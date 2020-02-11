library(tidyverse)
library(magrittr)
library(ggformula)


mx=function(vals,count){
  t=vals*(1+0.0333*count)
  t[count==1]=vals[count==1]
  return(t)
}


x=c(100,120,115,105,130,120,125)
y=c(10,4,6,10,3,6,5)

vc=c(mx(x,y),mx(x+5,y))


f=spline(vc,n=40)
tb=tibble(d=f$x,val=f$y,day=ifelse(d %in% 1:length(vc),1,0))

ggplot(tb)+
  geom_line(aes(x=d,y=val),col="green",size=1.3)+
  geom_point(aes(x=d,y=val,size=factor(day/2),col=factor(ifelse(day==1,"red","green"))))











