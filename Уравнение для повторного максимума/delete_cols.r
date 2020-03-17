library(tidyverse)
library(magrittr)

data=read_tsv("eng_survey.tsv",
              skip=1,col_names = F,na="",
              col_types = "fddnfffnnnffff",
              comment="#"
              ) %>% tbl_df()

sm=apply(data[-c(12,14)], 1, function(line) paste(line %>% as.character(),collapse='\t'))

write(sm,"eng_data.tsv")
