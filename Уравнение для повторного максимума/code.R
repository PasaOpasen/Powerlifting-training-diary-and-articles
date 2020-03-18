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
library(plotly)

data=read_tsv("data(rus).tsv",
              skip=1,col_names = F,na="",
              col_types = "fddnfffnnnff",
              comment="#"
              ) %>% tbl_df()

colnames(data)=c("Date","RM","MRM","Count","Action","Sex","Experience","Age","Weight","Height","BodyType","Mail")
data %<>%#filter(Count<=20) %>% 
  arrange(MRM,Count,Weight) %>%  mutate(
  CountGroup=cut(Count,breaks = c(1,3,6,10,20,40)),
  AgeGroup=cut(Age,breaks = c(1,19,27,35,70)),
  Experience=factor(Experience,levels = c("До двух лет","2-3 года","4-5 лет","6-10 лет","11-15 лет" ,"больше 15 лет"),ordered = T),
  Index=Weight/(0.01*Height)^2,
  IndexGroup=cut(Index,breaks = c(0,16,18.5,24.99,30,35,40,60))
  )%>% select(-Date)#,-Mail) %>% filter(Count>1,MRM<RM)
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

#уникальные записи (где один от каждого человека берётся только одна запись)
#объяснить, по каким признакам людей считать одинаковыми
data.unique=data %>% select(AgeGroup,Height,BodyType,Experience,Sex,IndexGroup) %>% unique()

#функции
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



#Разведочный анализ и описание выборки####


psych::describe(data)
summary(data %>% select(-Mail))


getPIE(data.unique$Sex,main = "Пол испытуемых")

getPIE(data.unique$BodyType,main="Тип телосложения испытуемых")
chisq.test(data.unique%>% filter(Sex=="Мужчина") %>%select(BodyType) %>% table())

getPIE(data$Action, main="Движение")

par(mfrow=c(1,1),mai=rep(0.1,4))
getPIE(data$CountGroup,main = "Диапазон повторений")
getPIE(data$Experience,main = "Опыт тренировок")
getPIE(data$AgeGroup,main = "Возрастная группа")
par(mfrow=c(1,1))

(ggplot(data,aes(x=CountGroup,fill=Action))+geom_bar()+theme_classic()+labs(title="Количество наблюдений в каждом диапазоне повторений",x="Диапазон повторений",y="Количество")) %>% ggplotly()
ggplot(data,aes(x=Experience))+geom_bar()+theme_classic()+labs(title="Распределение по опыту тренировок",x="Опыт тренировок",y="Количество")
ggplot(data,aes(x=AgeGroup))+geom_bar()+theme_classic()+labs(title="Распределение по возрасту",x="Возрастная группа",y="Количество")



#среди эндоморфов избыток веса встречается почаще
data.unique %$%  table(BodyType,IndexGroup)

ggplot(data.unique,aes(x=BodyType,fill=IndexGroup))+geom_bar(position=position_dodge2())+
  labs(x="Тип телосложения",y="Количество ответивших",fill="Индекс массы тела")+
  coord_flip()+theme_bw()+theme(legend.position = "bottom")

#ggplot(data,aes(x=Age,y=Index))+geom_point()
data %$%cor(Age,Index) 
#chisq.test(data.unique%>% filter(IndexGroup=="ожирение1") %>%select(BodyType) %>% table())

tb=data.unique%>% mutate(Obees=ifelse(IndexGroup=="ожирение1","да","нет")) %>%select(Obees,BodyType) %>% table()
#tb %>% chisq.test()
tb %>% t() %>% prop.test()#для всех
data.unique%>% 
  mutate(Obees=ifelse(IndexGroup=="ожирение1","да","нет"),Bd=ifelse(BodyType=='Эндоморф',1,0)) %>%
  select(Obees,Bd) %>% table() %>% t() %>% 
  prop.test() %$% conf.int


#tb %>% chisq.test(p=c(1,1,20),rescale.p = T)
if(F){
 par(mfrow=c(2,2),mai=rep(0.1,4))

getPIE(data.unique$Sex,main = "Пол испытуемых")
getPIE(data.unique$BodyType,main = "Телосложение испытуемых")
getPIE(data.unique %>% filter(Sex=="Мужчина") %$% BodyType,main = "Телосложение: мужчины")
getPIE(data.unique%>% filter(Sex=="Женщина") %$% BodyType,main = "Телосложение: женщины") 



par(mfrow=c(1,1),mai=rep(0.1,4))


getFan(cut(data$Age,breaks=c(0,20,30,40,100)))





data %<>%select(-Age)
pairs(data %>% select(-Count))
}



GGally::ggpairs(data%>% select(-Count,-Mail,-Age,-Sex),
                title="Диаграммы взаимодействий между переменными в выборке",
                lower = list(combo = "box")) #%>% ggplotly()


GGally::ggpairs(data%>% select(RM,MRM,Action,BodyType),
                title="Диаграммы взаимодействий между переменными в выборке",
                lower = list(combo = "box")) #%>% ggplotly()

GGally::ggpairs(data%>% select(RM,Action,IndexGroup),
                title="Диаграммы взаимодействий между переменными в выборке",
                lower = list(combo = "box")) #%>% ggplotly()


ggplot(data,aes(x=IndexGroup,y=RM))+geom_boxplot()+facet_wrap(vars(Action))
ggplot(data,aes(x=IndexGroup,y=RM))+geom_boxplot()+facet_grid(Action~BodyType)
ggplot(data,aes(x=Index,y=RM))+geom_point()+facet_wrap(~Action)


GGally::ggpairs(data%>% select(-Count,-Mail,-Age,-Sex,-Index,-Height,-CountGroup,-AgeGroup,-Weight),
                title="Диаграммы взаимодействий между переменными в выборке",
                lower = list(combo = "box")) #%>% ggplotly()





data %>% ggplot(aes(x=factor(Count),y=RM/MRM-1))+geom_boxplot()+theme_bw()
#data %>% ggplot(aes(x=CountGroup,y=RM/MRM))+geom_boxplot()+theme_bw()
data %>% ggplot(aes(x=factor(Count),y=(RM/MRM-1)/Count))+geom_boxplot()+theme_bw()

#По этому соотношению надо бы выбросы удалить
data %>% ggplot(aes(x=factor(Count),y=100*MRM/RM))+geom_boxplot()+coord_flip()+theme_bw()#+
  #annotate("text", x=10, y = mean(100*MRM/RM), label = "Начальные результаты")

data %>% mutate(perc=100*MRM/RM) %>% filter(Count<=15) %>% 
  group_by(factor(Count)) %>% 
  summarise(mean=t.test(perc,conf.level = 0.99)$estimate,
            down=t.test(perc,conf.level = 0.99)$conf.int[1],
            up=t.test(perc,conf.level = 0.99)$conf.int[2])




#как насчёт какой-то такой модели?
lm(MRM/RM~Count+I(Count^2)+I(MRM/Weight),data) %>% summary()
lm(MRM/RM~Count:CountGroup,data) %>% summary()
lm(MRM/RM~Count+I(Count^2)+I(MRM/Weight),data) %>% plot()

gr=data$CountGroup %>% as.numeric()
cors=function(inds)cor(data$RM[inds],data$MRM[inds])

cors(gr<2)
cors(gr==2)
cors(gr==3)
cors(gr==4)
cors(gr==5)

m=lm(RM~MRM:factor(Count)-1,data) 
m%>% summary()
cf=coefficients(m)[1:7]
cf-cf[4]
coefficients(m)[1:7] %>% plot(x=2:8,type="b")

#по этому графику видно, что повторения делятся на линейные группы, то есть в пределах группы они дают одинаковый прирост
plot(seq(-10,10,length.out=50) %>% map_dbl(function(x)1/(1+exp(-x))) )

# а что если там квадратичная зависимость от 2 до 6?
m=lm(RM~MRM:I((Count-1)^2)-1,data %>% filter(Count<7)) 


sigma=nls(v~1+b/(1+exp(-k*n)),
          data=data.frame(v=coefficients(m)[1:7],n=1:7),
          start = list(b=0.3,k=1))


GGally::ggcorr(data,label=T,label_round = 2)
data %$% cor(RM,MRM*Count)

data %>% ggplot(aes(x=MRM,y=RM))+geom_RMooth()+geom_point(aes(col=CountGroup),size=3)+theme_bw()

byCountGroup=ggplot(data,aes(y=RM/MRM,col=Sex))+facet_wrap(~CountGroup)+theme_bw()
byCountGroup+geom_point(aes(x=Age))
byCountGroup+geom_point(aes(x=Weight))

ggplot(data,aes(x=Experience,y=RM/MRM))+geom_boxplot()+facet_wrap(~CountGroup)+theme_bw()
#есть ли значимые различия в разных возрастных группах для фиксированного диапазона
aov(RM/MRM~Experience,data %>% filter(CountGroup=="2-3")) %>% summary()



obj=ggplot(data %>% select(-Count))+theme_bw()
obj+geom_bar(aes(x=CountGroup))
obj+geom_bar(aes(x=BodyType))
obj+geom_point(aes(x=Weight,y=Height,col=BodyType,shape=Experience),size=5)
obj+geom_boxplot(aes(x=Action,y=RM))

bx=obj+geom_boxplot(aes(x=CountGroup,y=RM/MRM))+
  labs(title = "Отношение повторного максимума к многоповторному в зависимости от числа повторений",
       x="Диапазон повторений",y="Отношение повторного максимума к многоповторному",
       caption = "Каждый диапазон повторений действует по своим законам \n и нуждается в собственной версии модели")+
  theme_tq()

bx+facet_grid(~BodyType)+theme_bw()+labs(caption="caption")
bx+facet_grid(~Sex)+theme_bw()+labs(caption="caption")
bx+facet_grid(~Action)+theme_bw()+labs(caption="caption")



#Функция ошибок####
Error=function(target,weight) (target-weight)^2 %>% mean() %>% sqrt()

Show=function(vals,df=data){
  #vals=predict(model,df)
  err=df$RM-vals
  cbind(value=vals,Target=df$RM,Set=paste0(df$MRM,"*",df$Count),
        ERROR=abs(df$RM-vals),
        ErrorPercent=abs(err)/df$RM*100,
        df[,c(3:15)]) %>% tbl_df() %>% select(-Count, IndexGroup)%>% arrange(-ERROR,-ErrorPercent,Weight) %>% 
        filter(ERROR>1)%>% print()
  cat("\n")
  rg=range(err)#;print(err);print(rg)
  
  if(rg[1]<0)cat("------------> Наибольшая ошибка в большую сторону:",-rg[1],"\n")
  if(rg[2]>0)cat("------------> Наибольшая ошибка в меньшую сторону:",rg[2],"\n")
  
  s=sum(abs(err)/df$RM*100>maxerror)
  len=length(err)
  cat("Модель ошиблась более чем на",maxerror,"% в",s,"случаях из",len,"(",s/len*100,"%)\n")
  s=sum(abs(err)>maxerror)
  cat("Модель ошиблась более чем на",maxerror,"кг в",s,"случаях из",len,"(",s/len*100,"%)\n")
  cat("-------------------> Среднеквадратичная ошибка:",Error(vals,df$RM),"\n")
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
  
  (p+facet_grid(vars(Action))) %>% print()
  
  p+facet_grid(vars(BodyType),vars(Action))
}

#из этого графика можно сделать вывод, что модель неплохо работает на диапазоне 2-3, но на диапазоне 13-20 ошибка какая-то сильно отличающаяся от тенденции уменьшения ошибок, так что этот диапазон надо бы и вообще убрать, так как там уже играют роль свойства красных волокон, не говорящие о силе
ResAn(data$RM-data$MRM*(1+0.0333*data$Count))

ResVal=function(vals)ResAn(data$RM-vals)



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

shapiro.test(data$RM)
hist(data$RM)

#начальная
Show(data$MRM*(1+0.0333*data$Count))

#оптимизация чисто коэффициента
md=nls(RM~MRM*(s+coef*Count)^t+k*Weight/MRM,
       data=data,
       start = list(s=1,coef=0.0333,t=1,k=0))
summary(md)
Show(predict(md,data))
ResVal(predict(md,data[3:4]))

md=nls(RM~MRM*(1+coef*Count),
       data=data,
       start = list(coef=0.0333))
summary(md)
Show(predict(md,data))


md=nls(RM~MRM^vk*(1+coef*Count)^kk,
       data=data,
       start = list(coef=0.0333,vk=1.,kk=1.))
summary(md)
Show(predict(md,data))


md=nls(RM~MRM*(1+coef*Count)^(kk+Count*t),
       data=data,
       start = list(coef=0.033,kk=0.01,t=0))
summary(md)
Show(predict(md,data))

#оптимизация чисто коэффициента c поправкой на его группу
md=lm(I(RM/MRM-1)~MRM:Count:CountGroup-1,data)
summary(md)
Show((predict(md,data %>% select(MRM,Count,CountGroup))+1)*data$MRM)
ResVal((predict(md,data %>% select(MRM,Count,CountGroup))+1)*data$MRM)


#надо глянуть это:
md=lm(I(RM/MRM-1)~Count:CountGroup-1,data);ShowErrors(md,data$MRM,data$MRM)
md=lm(I(RM-MRM)~MRM:Count:CountGroup-1,data);ShowErrors(md,sum.coef=data$MRM)
md=lm(RM~MRM+MRM:Count:CountGroup-1,data)
md=lm(RM~MRM:CountGroup+MRM:Count:CountGroup+I((Height-120)/Weight):Count:CountGroup-1,data)

md=lm(I(RM^2)~MRM+MRM:Count-1,data)
Show(predict(md,data) %>% sqrt())

md=lm(sqrt(RM)~MRM+MRM:Count-1,data)
Show(predict(md,data)^2)


md=lm(RM~MRM:Count+MRM:CountGroup-1,data)

Show(predict(md,data)[data$Count<11],data %>% filter(Count<11))
############################################################################################################
#MRM+MRM*Count с поправкой на группу
md=lm(RM~MRM:Count:CountGroup+MRM:CountGroup-1,data)
summary(md)
Show(predict(md,data))
ShowErrors(md)
ResVal(predict(md,data %>% select(MRM,Count,CountGroup)))

md=lm(RM~MRM:Count:CountGroup+MRM:CountGroup+MRM+MRM:Count-1,data)
md=lm(RM~MRM:Count:CountGroup+MRM-1,data)
md=lm(RM~MRM:Count:CountGroup+MRM:BodyType-1,data)

md=lm(RM~MRM:CountGroup:Count+MRM:Action:BodyType-1,data)

#md=lm(RM~MRM:Count+MRM-1,data,subset = data$Count<4)
md=step(md,
        direction = "both",
        scope = (~.+
                   MRM:BodyType+
                   MRM:Count:BodyType+
                   MRM:Experience+
                   MRM:AgeGroup:Experience+
                   MRM:Count:Experience+
                   MRM:Count:CountGroup:BodyType+
                   MRM:CountGroup:BodyType+
                   I(MRM*Count/Weight):CountGroup+
                   I(MRM*Weight/(Height-100))+
                   I((MRM-100)/MRM)+
                   poly(MRM/Weight,2)+
                   MRM+MRM:Count+
                   MRM:Weight:BodyType
                 ),steps=5000)

#md=regsubsets(md$call$formula,data=data,nbest = 10)


summary(md)
Show(predict(md,data))
ResVal(predict(md,data %>% select(MRM,Count,CountGroup)))


md=lm(RM~MRM+MRM:Count-1+MRM:BodyType:Count+MRM:Experience:Count+
        MRM:BodyType+MRM:Count:CountGroup+MRM:CountGroup+#MRM:Sex
        MRM:Experience+
        I(MRM*Weight/(Height-100))+
        I((MRM-100)/MRM)+poly(MRM/Weight,2)
      ,data)
summary(md)
Show(predict(md,data))


md=step(md,direction = "both")
summary(md)
Show(predict(md,data))

anova(md)



pr=predict(md,data%>% select(MRM,Count,Weight,Height,BodyType,Sex,Experience,CountGroup), interval = "prediction", level = 0.95)
obj+
  #geom_ribbon(aes(x=MRM,ymin = pr[,2], ymax = pr[,3]), fill = "grey70") +
  geom_line(aes(x=MRM,y=pr[,1]),size=1,col="grey70",linetype="dotted")+
  geom_point(aes(x=MRM,y=pr[,1]),size=3)+
  geom_point(aes(x=MRM,y=RM,col=BodyType,shape=Action),size=4)+theme(legend.position = c(0.85,0.25))


#Рассмотрение остатков####
d2=data %>% mutate(res=RM-predict(md,data%>% select(MRM,Count,Weight,Height,BodyType,Sex,Experience))) %>% select(-Date,-Mail)

ob=ggplot(d2,aes(y=res))

ob+geom_boxplot(aes(x=Action))
ob+geom_boxplot(aes(x=BodyType))
ob+geom_point(aes(x=allrows,shape=BodyType,col=factor(ifelse(abs(res)>2,"red","green"))),size=4)

















