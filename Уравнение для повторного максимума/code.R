library(tidyverse)
library(magrittr)


data=read_tsv("data.tsv",
              skip=1,col_names = F,na="",
              col_types = "fddnfffnnnff"
              ) %>% tbl_df()

colnames(data)=c("Date","SM","Val","Count","Type","Sex","Experience","Age","Weight","High","Body","Mail")
data %<>% mutate(CountGroup=cut(Count,breaks = c(1,4,8,12,15,25)))
allrows=1:11


summary(data)

Error=function(target,weight) (target-weight)^2 %>% sum()
Show=function(vals,rows){
  cbind(value=vals,Target=data$SM[rows],ERROR=abs(data$SM[rows]-vals),data[rows,c(3:11,13)]) %>% tbl_df() %>% print()
  cat("---------------------> Error is",Error(vals,data$SM[rows]),"\n")
}

Show(data$Val*(1+0.0333*data$Count),allrows)


md=lm(SM~Val:Count+Val-1,data)
summary(md)
Show(predict(md,data[c(3:4,9,13)]),allrows)


md=nls(SM~sqrt(Val)*(est+coef*Count),
       data=data,
       start = list(est=1.0,coef=0.0333))
summary(md)
Show(predict(md,data[c(3:4,9)]),allrows)


md=nls(SM~Val*(1+coef*Count),
       data=data,
       start = list(coef=0.0333))
summary(md)

Show(predict(md,data[3:4]),allrows)

















