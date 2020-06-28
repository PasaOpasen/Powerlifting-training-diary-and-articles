


library(shiny)
library(magrittr)
library(dplyr)
#load("entire_data.rdata")



# Define UI for application that draws a histogram
ui <- fluidPage(

  
  
    # Application title
    titlePanel("Estimating repeated maximim by multi-repeated maximum"),

    # Sidebar with a slider input for number of bins 
    sidebarLayout(
           sidebarPanel(     
               
        numericInput(inputId = "MRM",
                     label = "Your repeated maximum(kg):",
                     value = 100,min=20,max=450,
                     step =2.5,
                     width = "30%"),
        
         selectInput(inputId = "Count",
                label = "Repeats:",
                choices = 2:10 %>% as.character(),
                width = "30%"),
    
        selectInput(inputId = "Action",
                    label = "Choose an action:",
                    choices = c("Bench press", "Squat", "Deadlift"),
                    width = "40%"),
    

            sliderInput("Weight",
                        "Choose your weight(kg):",
                        min = 40,
                        max = 160,
                        value = 80),
            sliderInput("Height",
                        "Choose your height(cm):",
                        min = 150,
                        max = 210,
                        value = 180)
        ),
      
        # Show a plot of the generated distribution
        mainPanel(
          # plotOutput("distPlot")
           h3(textOutput("cap1", container = span)),
            tableOutput("vals"),
           h3(textOutput("cap2", container = span)),
            tableOutput("vals2"),
           textOutput("sorry"),
            #verbatimTextOutput("vals"),
           h3(textOutput("cap3", container = span)),
           tableOutput("perc")
        )
    )
)



 e = new.env()
 name <- load("entire_data.rdata", envir = e)
 
 f=function(MRM,Count,Action='Жим',Weight=70,Height=170){
   
   act=factor(Action,levels =e$action.levels)
   
   up=c(4,8,11)
   
   lv=e$count.levels
   
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
   
   predict(e$b5,
           df,
           se.fit = T,
           interval = "confidence",
           level=0.999)[[1]] %>% return()
   
 }
 f2=function(MRM,Count,Action='Жим',Weight=70,Height=170){
    
    act=factor(Action,levels =e$action.levels)
    
    up=c(4,8,11)
    
    lv=e$count.levels
    
    cg=lv[Count<up] %>% first() %>% factor(levels=lv)

    
    df=data.frame(MRM=MRM,
                  Count=Count,
                  Action=act,
                  CountGroup=cg,
                  Index=Weight/(0.01*Height)^2)
    #df %>% print()
    
    predict(e$n14, df) %>% return()
    
 }
 mrm3=function(RM,count,Action='Присед',Weight=70,Height=170){
   
   cf=e$cf
   
   ctg=3
   if(count<7){ctg=2}
   if(count<4){ctg=1}
   
   act=0
   if(Action=="Тяга"){
     act=cf[5]}else if(Action=="Присед"){act=cf[6]}
   
   
   polyroot(c(-RM,cf[1+ctg]+count*cf[6+ctg]+act,0,0,0,0,cf[1]*((0.01*Height)^2/Weight)^6))[1] %>% Re()
   
 } 
  
 
 
# Define server logic required to draw a histogram
server <- function(input, output) {

   output$cap1 <- renderText({
      'Predictions by linear model:'
   })
   output$cap2 <- renderText({
      'Predicted multi-reps:'
   })
   output$cap3 <- renderText({
      'Percentiles:'
   })
   
   output$sorry<-renderText({
     'The present model can give outliers if RM or Repeats are very large or the body mass index is too small. It`s because of features of the dataset, sorry'
   })
   
   
    output$vals <- renderTable({

        v=f(input$MRM,
            input$Count %>% as.numeric(),
          Action=switch (input$Action,
                         "Bench press" = "Жим",
                         "Squat" = "Присед",
                         "Deadlift" = "Тяга",
          ),
        Height=input$Height,
        Weight=input$Weight) %>% round(1)
        
        colnames(v)=c("RM Prediction","Lower prediction","Upper prediction")
        
        tbl_df(v)
    })
    
    output$vals2 <- renderTable({
      
      MRM=input$MRM
      Count=input$Count %>% as.numeric()
      Action=switch (input$Action,
                     "Bench press" = "Жим",
                     "Squat" = "Присед",
                     "Deadlift" = "Тяга",
      )
      Height=input$Height
      Weight=input$Weight      
      
      
      v=f(MRM,
          Count,
          Action=Action,
          Height=Height,
          Weight=Weight)[1] %>% round(1)
      
      v2=sapply(2:10, function(xx) mrm3(v,xx,Action = Action,Weight = Weight,Height = Height))
      
      v2=c(v,v2 %>% round(1))
      nas=v2>v
      v2[nas]=NA
      
      if(length(nas)>0){
        for(i in 2:9){
          if(is.na(v2[i])){
            v2[i]=(v2[i-1]+v2[i+1])/2
          }
        }
      }
      
      inds=nas&!is.na(v2)
      
      v2[inds]=paste('~',v2[inds])
      
      df=data.frame(1:10,v2)
      names(df)=c("Count of repeats","Predicted weight")
      
      df
    })
    
    output$perc <- renderTable({
       
       v=f(input$MRM,
           input$Count %>% as.numeric(),
           Action=switch (input$Action,
                          "Bench press" = "Жим",
                          "Squat" = "Присед",
                          "Deadlift" = "Тяга",
           ),
           Height=input$Height,
           Weight=input$Weight)[1]
       
       v2=f2(input$MRM,
       input$Count %>% as.numeric(),
       Action=switch (input$Action,
                      "Bench press" = "Жим",
                      "Squat" = "Присед",
                      "Deadlift" = "Тяга",
       ),
       Height=input$Height,
       Weight=input$Weight)
       
       vc=seq(100,50,by=-5)
       g=tibble('Percentage of RM'=paste0(vc,'%'),'Linear model'=round(v*vc/100,1),'Nonlinear model'=round(v2*vc/100,1))
       g
    })
}



# Run the application 
shinyApp(ui = ui, server = server)
