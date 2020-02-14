#Загрузка данных####
library(tidyverse)
library(magrittr)
library(ggformula)

data=read_tsv("data.tsv",
              skip=1,col_names = F,na="",
              col_types = "fddnfffnnnff"
              ) %>% tbl_df()

colnames(data)=c("Date","SM","Val","Count","Type","Sex","Experience","Age","Weight","High","Body","Mail")
data %<>% mutate(CountGroup=cut(Count,breaks = c(1,3,6,9,12,20,30)),ValCoef=Val/Weight,SMcoef=SM/Weight)
allrows=1:nrow(data)



#Разведочный анализ и описание выборки####

summary(data)
pairs(data %>% select(-Date,-Mail))
GGally::ggpairs(data %>% select(-Date,-Mail))
data[sapply(data, is.numeric)]%>% cor()%>% corrplot::corrplot(method = "number")


obj=ggplot(data %>% select(-Date,-Mail))+theme_bw()
obj+geom_bar(aes(x=CountGroup))
obj+geom_bar(aes(x=Body))
obj+geom_point(aes(x=Weight,y=High,col=Body,shape=Experience),size=5)
obj+geom_boxplot(aes(x=Type,y=SM))


getPIE=function(vec,main=""){
  ln=length(levels(vec))
  x=numeric(ln)
  ns=character(ln)
  for(i in seq(ln)){
    x[i]=sum(vec==levels(vec)[i])/length(vec)
    ns[i]=paste0(levels(vec)[i]," (",round(x[i]*100,2),"%)")
  }
  pie(x=x,labels=ns,main=main)
}

getPIE(data$Body)
getPIE(data$CountGroup)
getPIE(data$Experience)
getPIE(factor(data$Count))


Error=function(target,weight)
  {
    s=(target-weight)^2 %>% sum()
    return(sqrt(s/length(weight)))
  } 
Show=function(vals,rows){
  cbind(value=vals,Target=data$SM[rows],
        ERROR=abs(data$SM[rows]-vals),ErrorPercent=abs(data$SM[rows]-vals)/data$SM[rows]*100,
        data[rows,c(3:11,13)]) %>% tbl_df() %>% print()
  cat("---------------------> Error is",Error(vals,data$SM[rows]),"\n")
}

Show(data$Val*(1+0.0333*data$Count),allrows)

md=nls(SM~Val*(est+coef*Count),
       data=data,
       start = list(est=1.0,coef=0.0333))
summary(md)
Show(predict(md,data[c(3:4,9)]),allrows)

md=nls(SM~Val*(1+coef*Count),
       data=data,
       start = list(coef=0.0333))
summary(md)
Show(predict(md,data[3:4]),allrows)


md=lm(SM~Val:Count:CountGroup+Val:CountGroup-1,data)
summary(md)
Show(predict(md,data %>% select(Val,Count,CountGroup)),allrows)


md=lm(SMcoef~ValCoef:Count:CountGroup+ValCoef:CountGroup-1,data)
summary(md)
Show(predict(md,data %>% select(ValCoef,Count,CountGroup))*data$Weight,allrows)

car::vif(md)
plot(md)


md=lm(SM~Val+Val:Count-1+Val:Body:Count+Val:Experience:Count+
        Val:Body+#Val:Sex+
        Val:Experience+
        I(Val*Weight/(High-100))+
        I((Val-100)/Val)+poly(Val/Weight,2)
      ,data)
summary(md)
Show(predict(md,data%>% select(Val,Count,Weight,High,Body,Sex,Experience
                               )),allrows)


md=step(md,direction = "both")
summary(md)
Show(predict(md,data%>% select(Val,Count,Weight,High,Body,Sex,Experience)),allrows)

anova(md)



pr=predict(md,data%>% select(Val,Count,Weight,High,Body,Sex,Experience,CountGroup), interval = "prediction", level = 0.95)
obj+
  #geom_ribbon(aes(x=Val,ymin = pr[,2], ymax = pr[,3]), fill = "grey70") +
  geom_line(aes(x=Val,y=pr[,1]),size=1,col="grey70",linetype="dotted")+
  geom_point(aes(x=Val,y=pr[,1]),size=3)+
  geom_point(aes(x=Val,y=SM,col=Body,shape=Type),size=4)+theme(legend.position = c(0.85,0.25))


#Рассмотрение остатков####
d2=data %>% mutate(res=SM-predict(md,data%>% select(Val,Count,Weight,High,Body,Sex,Experience))) %>% select(-Date,-Mail)

ob=ggplot(d2,aes(y=res))

ob+geom_boxplot(aes(x=Type))
ob+geom_boxplot(aes(x=Body))
ob+geom_point(aes(x=allrows,shape=Body,col=factor(ifelse(abs(res)>2,"red","green"))),size=4)

















