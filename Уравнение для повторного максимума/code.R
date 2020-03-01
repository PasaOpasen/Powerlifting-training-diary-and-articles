#Загрузка данных####
library(tidyverse)
library(magrittr)
library(ggformula)
library(ggthemes)
library(tidyquant)
library(ggvis)
library(plotrix)
library(car)
#library(DAAG)
library(leaps)

data=read_tsv("data.tsv",
              skip=1,col_names = F,na="",
              col_types = "fddnfffnnnff",
              comment="#"
              ) %>% tbl_df()

colnames(data)=c("Date","SM","Val","Count","Type","Sex","Experience","Age","Weight","High","Body","Mail")
data %<>%#filter(Count<=20) %>% 
  arrange(Val,Count,Weight) %>%  mutate(
  CountGroup=cut(Count,breaks = c(1,3,6,10,20,40)),
  AgeGroup=cut(Age,breaks = c(1,19,27,35,70)),
  Experience=factor(Experience,levels = c("До двух лет","2-3 года","4-5 лет","6-10 лет","11-15 лет" ,"больше 15 лет"),ordered = T),
  Index=Weight/(0.01*High)^2,
  IndexGroup=cut(Index,breaks = c(0,16,18.5,24.99,30,35,40,60))
  )%>% select(-Date)#,-Mail) %>% filter(Count>1,Val<SM)
levels(data$CountGroup)=c("2-3","4-6","7-10","11-20",">20")
levels(data$AgeGroup)=c("<20","20-27","28-35",">35")
levels(data$IndexGroup)=c("выраженный дефицит","дефицит","норма","избыток","ожирение1","ожирение2","ожирение3")

ex=data$Experience %>% as.numeric()
ex[ex==6]=5
ex %<>%factor() 
levels(ex)=c("До двух лет","2-3 года","4-5 лет","6-10 лет","больше 10 лет")
data %<>%mutate(Experience=factor(ex,ordered = T)) 

allrows=1:nrow(data)
maxerror=2

data %$%  table(Body,IndexGroup)


#Разведочный анализ и описание выборки####

psych::describe(data)
summary(data)


data %>% ggplot(aes(x=factor(Count),y=SM/Val-1))+geom_boxplot()+theme_bw()
#data %>% ggplot(aes(x=CountGroup,y=SM/Val))+geom_boxplot()+theme_bw()
data %>% ggplot(aes(x=factor(Count),y=(SM/Val-1)/Count))+geom_boxplot()+theme_bw()


gr=data$CountGroup %>% as.numeric()
cors=function(inds)cor(data$SM[inds],data$Val[inds])

cors(gr<2)
cors(gr==2)
cors(gr==3)
cors(gr==4)
cors(gr==5)

m=lm(SM~Val:factor(Count)-1,data) 
m%>% summary()
cf=coefficients(m)[1:7]
cf-cf[4]
coefficients(m)[1:7] %>% plot(x=2:8,type="b")

#по этому графику видно, что повторения делятся на линейные группы, то есть в пределах группы они дают одинаковый прирост
plot(seq(-10,10,length.out=50) %>% map_dbl(function(x)1/(1+exp(-x))) )

# а что если там квадратичная зависимость от 2 до 6?
m=lm(SM~Val:I((Count-1)^2)-1,data %>% filter(Count<7)) 



sigma=nls(v~1+b/(1+exp(-k*n)),
          data=data.frame(v=coefficients(m)[1:7],n=1:7),
          start = list(b=0.3,k=1))


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
ggplot(data,aes(x=CountGroup))+geom_bar()+theme_classic()+labs(title="Количество человек в каждом диапазоне повторений",x="Диапазон повторений",y="Количество")
getPIE(data$Experience,main = "Опыт тренировок")
ggplot(data,aes(x=Experience))+geom_bar()+theme_classic()+labs(title="Распределение по опыту тренировок",x="Опыт тренировок",y="Количество")
ggplot(data,aes(x=AgeGroup))+geom_bar()+theme_classic()+labs(title="Распределение по возрасту",x="Возрастная группа",y="Количество")

getPIE(data$Type,main = "Движение")


par(mfrow=c(1,1),mai=rep(0.1,4))


getFan(cut(data$Age,breaks=c(0,20,30,40,100)))


data %<>%select(-Age)
pairs(data %>% select(-Count))
GGally::ggpairs(data%>% select(-Count,-Mail),title="Диаграмы взаимодействий между переменными в выборке",
                lower = list(continuous = "smooth_loess", combo = "box"))

GGally::ggcorr(data,label=T,label_round = 2)
data %$% cor(SM,Val*Count)

data %>% ggplot(aes(x=Val,y=SM))+geom_smooth()+geom_point(aes(col=CountGroup),size=3)+theme_bw()

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
        df[,c(3:11)]) %>% tbl_df() %>% select(-Count, IndexGroup)%>% arrange(-ERROR,-ErrorPercent,Weight) %>% 
        filter(ERROR>1)%>% print()
  cat("\n")
  rg=range(err)#;print(err);print(rg)
  
  if(rg[1]<0)cat("------------> Наибольшая ошибка в большую сторону:",-rg[1],"\n")
  if(rg[2]>0)cat("------------> Наибольшая ошибка в меньшую сторону:",rg[2],"\n")
  
  s=sum(abs(err)/df$SM*100>maxerror)
  len=length(err)
  cat("Модель ошиблась более чем на",maxerror,"% в",s,"случаях из",len,"(",s/len*100,"%)\n")
  s=sum(abs(err)>maxerror)
  cat("Модель ошиблась более чем на",maxerror,"кг в",s,"случаях из",len,"(",s/len*100,"%)\n")
  cat("-------------------> Среднеквадратичная ошибка:",Error(vals,df$SM),"\n")
}
ShowErrors=function(model,power.coef=1,sum.coef=0){
  Show(predict(model,data)*power.coef+sum.coef)
  cat("Оценка кросс-валидации для всего набора данных",
      boot::cv.glm(data,glm(formula = md$call$formula,data=data),K=10)$delta[1],"\n")
  cat("Оценка кросс-валидации для не более чем 10 повторений",
      boot::cv.glm(data %>% filter(Count<11),glm(formula = md$call$formula,data=data %>% filter(Count<11)),K=10)$delta[1],"\n") 
  cat("Оценка кросс-валидации для не более чем 6 повторений",
      boot::cv.glm(data %>% filter(Count<7),glm(formula = md$call$formula,data=data %>% filter(Count<7)),K=10)$delta[1],"\n") 
}


ResAn=function(res){
  shapiro.test(res) %>% print()
  
  p=ggplot(data %>% mutate(res=res),aes(x=CountGroup,y=res))+
    geom_boxplot()+labs(x="Группа повторений",y="Остатки (цель - предсказание)",title="Распределения остатков в зависимости от группы повторений")+theme_bw()
  print(p)
  
  (p+facet_grid(vars(Type))) %>% print()
  
  p+facet_grid(vars(Body),vars(Type))
}

#из этого графика можно сделать вывод, что модель неплохо работает на диапазоне 2-3, но на диапазоне 13-20 ошибка какая-то сильно отличающаяся от тенденции уменьшения ошибок, так что этот диапазон надо бы и вообще убрать, так как там уже играют роль свойства красных волокон, не говорящие о силе
ResAn(data$SM-data$Val*(1+0.0333*data$Count))

ResVal=function(vals)ResAn(data$SM-vals)



qqPlot(md,main="Q-Q plot")

durbinWatsonTest(md)#тест на автокорреляцию

ncvTest(md)#однородность дисперсии

gvlma::gvlma(md) %>% summary()

vif(md)
par(mfrow=c(2,2))
plot(md)
par(mfrow=c(1,1))

outlierTest(md)

influencePlot(md,main="Диаграмма влияния",sub="Размеры кругов пропорциональны расстояниям Кука")

#Модели####

shapiro.test(data$SM)
hist(data$SM)

#начальная
Show(data$Val*(1+0.0333*data$Count))

#оптимизация чисто коэффициента
md=nls(SM~Val*(s+coef*Count)^t+k*Weight/Val,
       data=data,
       start = list(s=1,coef=0.0333,t=1,k=0))
summary(md)
Show(predict(md,data))
ResVal(predict(md,data[3:4]))

md=nls(SM~Val*(1+coef*Count),
       data=data,
       start = list(coef=0.0333))
summary(md)
Show(predict(md,data))


md=nls(SM~Val^vk*(1+coef*Count)^kk,
       data=data,
       start = list(coef=0.0333,vk=1.,kk=1.))
summary(md)
Show(predict(md,data))


#оптимизация чисто коэффициента c поправкой на его группу
md=lm(I(SM/Val-1)~Val:Count:CountGroup-1,data)
summary(md)
Show((predict(md,data %>% select(Val,Count,CountGroup))+1)*data$Val)
ResVal((predict(md,data %>% select(Val,Count,CountGroup))+1)*data$Val)


#надо глянуть это:
md=lm(I(SM/Val-1)~Count:CountGroup-1,data);ShowErrors(md,data$Val,data$Val)
md=lm(I(SM-Val)~Val:Count:CountGroup-1,data);ShowErrors(md,sum.coef=data$Val)
md=lm(SM~Val+Val:Count:CountGroup-1,data)
md=lm(SM~Val:CountGroup+Val:Count:CountGroup+I((High-120)/Weight):Count:CountGroup-1,data)

md=lm(I(SM^2)~Val+Val:Count-1,data)
Show(predict(md,data) %>% sqrt())

md=lm(sqrt(SM)~Val+Val:Count-1,data)
Show(predict(md,data)^2)


md=lm(SM~Val:Count+Val:CountGroup-1,data)

Show(predict(md,data)[data$Count<11],data %>% filter(Count<11))
############################################################################################################
#Val+Val*Count с поправкой на группу
md=lm(SM~Val:Count:CountGroup+Val:CountGroup-1,data)
summary(md)
Show(predict(md,data))
ShowErrors(md)
ResVal(predict(md,data %>% select(Val,Count,CountGroup)))

md=lm(SM~Val:Count:CountGroup+Val:CountGroup+Val+Val:Count-1,data)
md=lm(SM~Val:Count:CountGroup+Val-1,data)
md=lm(SM~Val:Count:CountGroup+Val:Body-1,data)

md=lm(SM~Val:CountGroup:Count+Val:Type:Body-1,data)

#md=lm(SM~Val:Count+Val-1,data,subset = data$Count<4)
md=step(md,
        direction = "both",
        scope = (~.+
                   Val:Body+
                   Val:Count:Body+
                   Val:Experience+
                   Val:AgeGroup:Experience+
                   Val:Count:Experience+
                   Val:Count:CountGroup:Body+
                   Val:CountGroup:Body+
                   I(Val*Count/Weight):CountGroup+
                   I(Val*Weight/(High-100))+
                   I((Val-100)/Val)+
                   poly(Val/Weight,2)+
                   Val+Val:Count+
                   Val:Weight:Body
                 ),steps=5000)

#md=regsubsets(md$call$formula,data=data,nbest = 10)


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

















