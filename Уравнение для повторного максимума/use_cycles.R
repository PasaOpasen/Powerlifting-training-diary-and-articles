library(tidyverse)
library(magrittr)
library(ggformula)
library(ggalt)


#простой цикл####

mx=function(vals,count){
  t=vals*(1+0.0333*count)
  t[count==1]=vals[count==1]
  s=t[length(t)]*(1+runif(1,0.,0.005))
  return(c(t,s))
}


x=c(100,120,115,105,130,120,125)
y=c(10,4,6,10,3,6,5)

vc=c(mx(x,y),mx(x+4,y),mx(x+9,y))



f=spline(vc,n=40)
tb=tibble(d=f$x,val=f$y,day=ifelse(d %in% 1:length(vc),1,0))

ggplot(tb)+
  geom_line(aes(x=d,y=val),col="green",size=1.3)+
  geom_point(aes(x=d,y=val,size=factor(day/2),col=factor(ifelse(day==1,"red","green"))))




tb=tibble(d=1:length(vc),val=vc) %>% mutate(day=factor(ifelse(d%%(length(x)+1)==0,"проходка","тренировка")))

ggplot(tb,aes(x=d,y=val))+
  geom_hline(yintercept = 141,size=1.1,linetype="dashed")+
  geom_hline(yintercept = vc[length(vc)],size=1.1,linetype="dashed")+
  geom_xspline(size=1,spline_shape = -0.3,linetype="dotdash",col="green",alpha=0.9)+
  geom_point(aes(col=day),size=4)+theme_bw()
  
  #geom_line(,col="green",size=1.3)+
  #geom_point(aes(x=d,y=val,size=factor(day/2),col=factor(ifelse(day==1,"red","green"))))









mx2=function(vals,count){
  t=vals*(1+0.0333*count)
  t[count==1]=vals[count==1]
  return(t)
}


#155 верхошанский
x=c(100,125,107.5,132.5,120,140,127.5,147.5,117.5,132.5,125,162.5)
y=c(6,5,5,4,3,3,3,2,5,2,2,1)
vc=c(mx2(x,y),mx2(x+8,y),mx2(x+15,y))
plot(vc,type = "b")


#160 Том МакКаллоу

x=c(112,112,117,122,126,131,136,141,145,150,155,160,166,177,122.5,122.5,128,133,138,143,149,154,159,165,170,175,182,195,136,136,142,148,154,160,165,171,177,183,190,195,203,216)
y=rep(c(10,10,8,8,5,5,5,5,3,3,2,2,1,1),3)
vc=c(mx2(x,y))
plot(vc,type = "b")









