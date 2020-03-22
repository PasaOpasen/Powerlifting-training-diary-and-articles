library(tidyverse)
library(magrittr)
library(ggformula)
library(ggalt)






#Функция модели####

action.levels=levels(data$Action)
count.levels=levels(data$CountGroup)

f=function(MRM,Count,Action='Жим',Weight=70,Height=170){
  
  if(Count<2){
    Count=2
  } else if(Count>10){
    Count=10
  }
  
  act=factor(Action,levels =globalenv()$action.levels)
  
  up=c(4,8,11)
  
  lv=globalenv()$count.levels
  
  cg=lv[Count<up] %>% first() %>% factor(levels=lv)
  
  #print(environment()$action.levels)
  
  #print(MRM)
  #print(Count)
  #print(act)
  #print(cg)
  #print(Weight/(0.01*Height)^2)
  
  df=data.frame(MRM=MRM,
                Count=Count,
                Action=act,
                CountGroup=cg,
                Index=Weight/(0.01*Height)^2)
  #df %>% print()
  
  predict(globalenv()$b5,
          df,
          se.fit = T,
          interval = "confidence",
          level=0.999)[[1]] %>% return()
  
}






#простой цикл####

mx=function(vals,count, withcomp=T){
  
  t=1:length(vals) %>% map_dbl(function(i) f(MRM=vals[i],Count=count[i],Action='Жим')[1])
  
  #t=vals*(1+0.0333*count)
  t[count==1]=vals[count==1]
  if(withcomp){
      s=t[length(t)]*(1+runif(1,0.,0.005))
  return(c(t,s))
  }
  return(t)
}


x=c(100,120,115,105,130,120,125)
y=c(10,4,6,10,3,6,5)

vc=c(mx(x,y),mx(x+4,y),mx(x+9,y))
lb=paste0(c(x,vc[8],x+4,vc[16],x+9,vc[24]) %>% round(),"x",c(y,1,y,1,y,1))

############################
f=spline(vc,n=40)
tb=tibble(d=f$x,val=f$y,day=ifelse(d %in% 1:length(vc),1,0))

ggplot(tb)+
  geom_line(aes(x=d,y=val),col="green",size=1.3)+
  geom_point(aes(x=d,y=val,size=factor(day/2),col=factor(ifelse(day==1,"red","green"))))
#############################



tb=tibble(d=1:length(vc),val=vc) %>% mutate(day=factor(ifelse(d%%(length(x)+1)==0,"проходка","тренировка")))

ggplot(tb,aes(x=d,y=val))+
  geom_hline(yintercept = 141,size=1.,linetype="dashed",alpha=0.8)+
  geom_hline(yintercept = vc[length(vc)],size=1.,linetype="dashed",alpha=0.8)+
  geom_xspline(size=1,spline_shape = -0.3,linetype="dotdash",col="green",alpha=0.9)+
  geom_point(aes(col=day),size=4)+
  annotate("text", x = 22, y = 141.7, label = "Начальные результаты") +
  annotate("text", x = 17, y = 153.2, label = "Итоговые результаты") +
  annotate("text", x = tb$d+0.4, y = tb$val-ifelse(seq(tb$d)%%8!=0,0.8,0.3), label = lb) +
  labs(title='Динамика простого цикла (три прохода)',
       subtitle="Использование модели помогает отследить прогрессию нагрузок\nЗелёную кривую можно рассматривать как аппроксимацию уровня тренированности",
       x="Номер тренировки",
       y="Требуемое усилие",
       caption="Не используйте такой цикл больше 2-3 проходов подряд, он может привести к перетренированности") +
  guides(color=guide_legend(title="Тип дня"))+
  theme_bw()+theme(legend.position = c(0.85,0.15))#+ guides(color=FALSE)
  






#155 верхошанский
x=c(100,125,107.5,132.5,120,140,127.5,147.5,117.5,132.5,125,162.5)
y=c(6,5,5,4,3,3,3,2,5,2,2,1)
vc=c(mx(x,y,F),mx(x+8,y,F),mx(x+15,y,F))
lb=paste0(c(x,x+8,x+15) %>% round(),"x",c(y,y,y))

tb=tibble(d=1:length(vc),val=vc) %>% mutate(day=factor(ifelse(d%%length(x)==0,"проходка","тренировка")))

ggplot(tb,aes(x=d,y=val))+
  geom_hline(yintercept = 155,size=1.,linetype="dashed",alpha=0.7)+
  geom_hline(yintercept = vc[length(vc)],size=1.,linetype="dashed",alpha=0.7)+
  geom_xspline(size=1,spline_shape = -0.7,linetype="dotdash",col="green",alpha=0.9)+
  geom_point(aes(col=day),size=4)+
  annotate("text", x = 3.5, y = 159, label = "Начальные результаты") +
  annotate("text", x = 3, y = 176, label = "Итоговые результаты") +
  annotate("text", x = tb$d+0.4, y = tb$val-ifelse(seq(tb$d)%%length(x)!=0,0.8,0.3), label = lb) +
  labs(title='Пиковый цикл Верхошанского (три прохода)',
       subtitle="Медленный подъём чуть выше бывшего максимума, затем отдых (разгон) и проходка",
       x="Номер тренировки",
       y="Требуемое усилие",
       caption="Файл с циклом прилагается") +
  guides(color=guide_legend(title="Тип дня"))+
  theme_bw()+theme(legend.position = c(0.85,0.15))#+ guides(color=FALSE)




#160 Том МакКаллоу

x=c(110,112,117,122,126,131,136,141,145,150,155,160,166,177,120,122.5,128,133,138,143,149,154,159,165,170,175,182,195,134,136,142,148,154,160,165,171,177,183,190,195,203,216)
y=rep(c(10,10,8,8,5,5,5,5,3,3,2,2,1,1),3)
vc=c(mx(x,y,F))
ct=length(y)/3

plot(vc,type = "b")

lb=paste0(x ,"x", y)

tb=tibble(d=1:length(vc),val=vc) %>% mutate(day=factor(ifelse(d%%ct==0,"проходка","тренировка")))

ggplot(tb,aes(x=d,y=val))+
  geom_hline(yintercept = 160,size=1.,linetype="dashed",alpha=0.7)+
  geom_hline(yintercept = vc[length(vc)],size=1.,linetype="dashed",alpha=0.7)+
  geom_xspline(size=1,spline_shape = -0.7,linetype="dotdash",col="green",alpha=0.9)+
  geom_point(aes(col=day),size=4)+
  annotate("text", x = 35, y = 162, label = "Начальные результаты") +
  annotate("text", x = 35, y = 214, label = "Итоговые результаты") +
  annotate("text", x = tb$d+0.4, y = tb$val-ifelse(seq(tb$d)%%ct!=0,0.8,0.3), label = lb) +
  labs(title='Тягловый цикл Тома Маккелоу (три прохода)',
       subtitle="Колебание, подъём, колебание, проходка",
       x="Номер тренировки",
       y="Требуемое усилие",
       caption="Файл с циклом прилагается. Первый день цикла сдвинут вниз для удобства визуализации") +
  guides(color=guide_legend(title="Тип дня"))+
  theme_bw()+theme(legend.position = c(0.85,0.1))#+ guides(color=FALSE)







