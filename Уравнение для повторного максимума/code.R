library(tidyverse)
library(magrittr)


data=read_tsv("data.tsv",
              skip=1,col_names = F,na="",
              col_types = "fddnfffnnnff"
              ) %>% tbl_df()

colnames(data)=c("Date","SM","Val","Count","Type","Sex","Experience","Age","Weigth","High","Body","Mail")

cbind(target=data$SM,value=data$Val*(1+0.0333*data$Count),type=data$Type)













