#Загрузка данных####
library(tidyverse)
library(magrittr)
library(ggformula)
library(ggthemes)
library(tidyquant)
library(ggvis)
library(plotrix)

data=read_tsv("data.tsv",
              skip=1,col_names = F,na="",
              col_types = "fddnfffnnnff"
              ) %>% tbl_df()

colnames(data)=c("Date","SM","Val","Count","Type","Sex","Experience","Age","Weight","High","Body","Mail")
data %<>% mutate(
  CountGroup=cut(Count,breaks = c(1,3,7,12,20,50))
  )%>% select(-Date,-Mail) %>% filter(Count>1,Val<SM)
levels(data$CountGroup)=c("2-3","4-7","8-12","13-20",">20")
allrows=1:nrow(data)
maxerror=2



#Разведочный анализ и описание выборки####

psych::describe(data)
summary(data)



getparam=function(vec){
  ln=length(levels(vec))
  x=numeric(ln)
  ns=character(ln)
  for(i in seq(ln)){
    x[i]=sum(vec==levels(vec)[i])/length(vec)
    ns[i]=paste0(levels(vec)[i]," (",round(x[i]*100,2),"%)")
  }
  return(list(x=x,ns=ns))
}

getPIE=function(vec,main=""){
  lst=getparam(vec)
  pie(x=lst$x,labels=lst$ns,main=main)
}
getFan=function(vec,main=""){
  pr=getparam(vec)
  fan.plot(pr$x,labels=pr$ns,main=main)
}


par(mfrow=c(2,2),mai=rep(0.1,4))
getPIE(data$Sex,main = "Пол испытуемых")
getPIE(data$Body,main = "Телосложение испытуемых")
getPIE(data %>% filter(Sex=="Мужчина") %$% Body,main = "Телосложение: мужчины")
getPIE(data%>% filter(Sex=="Женщина") %$% Body,main = "Телосложение: женщины")

chisq.test(data%>% filter(Sex=="Мужчина") %>%select(Body) %>% table())


par(mfrow=c(3,1),mai=rep(0.1,4))
getPIE(data$CountGroup,main = "Диапазон повторений")
getPIE(data$Experience,main = "Опыт тренировок")
getPIE(data$Type,main = "Движение")
par(mfrow=c(1,1),mai=rep(0.1,4))


getFan(cut(data$Age,breaks=c(0,20,30,40,100)))


data %<>%select(-Age)
pairs(data %>% select(-Count))
GGally::ggpairs(data%>% select(-Count),title="Диаграмы взаимодействий между переменными в выборке",
                lower = list(continuous = "smooth_loess", combo = "box"))

GGally::ggcorr(data,label=T,label_round = 2)
data %$% cor(SM,Val*Count)

byCountGroup=ggplot(data,aes(y=SM/Val,col=Sex))+facet_wrap(~CountGroup)+theme_bw()
byCountGroup+geom_point(aes(x=Age))
byCountGroup+geom_point(aes(x=Weight))

ggplot(data,aes(x=Experience,y=SM/Val))+geom_boxplot()+facet_wrap(~CountGroup)+theme_bw()
#есть ли значимые различия в разных возрастных группах для фиксированного диапазона
aov(SM/Val~Experience,data %>% filter(CountGroup=="2-3")) %>% summary()



obj=ggplot(data %>% select(-Count))+theme_bw()
obj+geom_bar(aes(x=CountGroup))
obj+geom_bar(aes(x=Body))
obj+geom_point(aes(x=Weight,y=High,col=Body,shape=Experience),size=5)
obj+geom_boxplot(aes(x=Type,y=SM))

bx=obj+geom_boxplot(aes(x=CountGroup,y=SM/Val))+
  labs(title = "Отношение повторного максимума к многоповторному в зависимости от числа повторений",
       x="Диапазон повторений",y="Отношение повторного максимума к многоповторному",
       caption = "Каждый диапазон повторений действует по своим законам \n и нуждается в собственной версии модели")+
  theme_tq()

bx+facet_grid(~Body)+theme_bw()+labs(caption="caption")
bx+facet_grid(~Sex)+theme_bw()+labs(caption="caption")
bx+facet_grid(~Type)+theme_bw()+labs(caption="caption")



#Функция ошибок####
Error=function(target,weight) (target-weight)^2 %>% mean() %>% sqrt()

Show=function(vals,df=data){
  #vals=predict(model,df)
  err=df$SM-vals
  cbind(value=vals,Target=df$SM,Set=paste0(df$Val,"*",df$Count),
        ERROR=abs(df$SM-vals),
        ErrorPercent=abs(err)/df$SM*100,
        df[,c(3:11)]) %>% tbl_df() %>% select(-Count)%>% arrange(-ERROR,-ErrorPercent,Count,Weight) %>% 
        filter(ERROR>1)%>% print()
  cat("\n")
  rg=range(err)
  
  if(rg[1]<0)cat("------------> Наибольшая ошибка в большую сторону:",-rg[1],"\n")
  if(rg[2]>0)cat("------------> Наибольшая ошибка в меньшую сторону:",rg[2],"\n")
  
  s=sum(abs(err)/df$SM*100>maxerror)
  len=length(err)
  cat("Модель ошиблась более чем на",maxerror,"% в",s,"случаях из",len,"(",s/len*100,"%)\n")
  s=sum(abs(err)>maxerror)
  cat("Модель ошиблась более чем на",maxerror,"кг в",s,"случаях из",len,"(",s/len*100,"%)\n")
  cat("-------------------> Среднеквадратичная ошибка:",Error(vals,df$SM),"\n")
}


ResAn=function(res){
  shapiro.test(res) %>% print()
  p=ggplot(data %>% mutate(res=res),aes(x=CountGroup,y=res))+
    geom_boxplot()+theme_bw()
  print(p)
  
  p=ggplot(data %>% mutate(res=res),aes(x=CountGroup,y=res))+
    geom_boxplot() +facet_grid(vars(Body),vars(Type))+theme_bw()
  p
}
ResAn(data$SM-data$Val*(1+0.0333*data$Count))

ResVal=function(vals)ResAn(data$SM-vals)

#Модели####

shapiro.test(data$SM)
hist(data$SM)

#начальная
Show(data$Val*(1+0.0333*data$Count))

#оптимизация чисто коэффициента
md=nls(SM~Val*(1+coef*Count),
       data=data,
       start = list(coef=0.0333))
summary(md)
Show(predict(md,data[3:4]))
ResVal(predict(md,data[3:4]))

#оптимизация чисто коэффициента c поправкой на его группу
md=lm(I(SM/Val-1)~Val:Count:CountGroup-1,data)
summary(md)
Show((predict(md,data %>% select(Val,Count,CountGroup))+1)*data$Val)
ResVal((predict(md,data %>% select(Val,Count,CountGroup))+1)*data$Val)
boot::cv.glm(data=data, glmfit=md,K=6)

#надо глянуть это:
md=lm(I(SM/Val-1)~Count:CountGroup-1,data)
md=lm(I(SM-Val)~Val:Count:CountGroup-1,data)
md=lm(SM~Val+Val:Count:CountGroup-1,data)


#Val+Val*Count с поправкой на группу
md=lm(SM~Val:Count:CountGroup+Val:CountGroup-1,data)
summary(md)
Show(predict(md,data %>% select(Val,Count,CountGroup)))
ResVal(predict(md,data %>% select(Val,Count,CountGroup)))

car::vif(md)
plot(md)


md=step(md,
        direction = "both",
        scope = (~
                   Val:Body+Val:Count:Body+
                   Val:Experience+Val:Count:Experience+
                   Val:Count:CountGroup:Body+Val:CountGroup:Body+
                   I(Val*Weight/(High-100))+
                   I((Val-100)/Val)+poly(Val/Weight,2)
                 ))
summary(md)
Show(predict(md,data))
ResVal(predict(md,data %>% select(Val,Count,CountGroup)))


md=lm(SM~Val+Val:Count-1+Val:Body:Count+Val:Experience:Count+
        Val:Body+Val:Count:CountGroup+Val:CountGroup+#Val:Sex
        Val:Experience+
        I(Val*Weight/(High-100))+
        I((Val-100)/Val)+poly(Val/Weight,2)
      ,data)
summary(md)
Show(predict(md,data))


md=step(md,direction = "both")
summary(md)
Show(predict(md,data))

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

















