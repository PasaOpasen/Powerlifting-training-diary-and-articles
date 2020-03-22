


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
                     width = "40%"),
        
         selectInput(inputId = "Count",
                label = "Repeats:",
                choices = 2:10 %>% as.character(),
                width = "30%"),
    
        selectInput(inputId = "Action",
                    label = "Choose an action:",
                    choices = c("Bench press", "Squat", "Deadlift"),
                    width = "60%"),
    

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
            tableOutput("vals")
            #verbatimTextOutput("vals"),
        )
    )
)



  e = new.env()
  name <- load("entire_data.rdata", envir = e)

  
  
# Define server logic required to draw a histogram
server <- function(input, output) {


  

    output$vals <- renderTable({

        v=e$f(input$MRM,
          input$Count %>% as.numeric(),
          Action=switch (input$Action,
            "Bench press" = "Жим",
            "Squat" = "Присед",
            "Deadlift" = "Тяга",
        ),
        Height=input$Height,
        Weight=input$Weight) %>% round(1)
        
        colnames(v)=c("RM Prediction","Lower prediction","Upper prediction")
        
        data.frame(v)
    })
    
    
}



# Run the application 
shinyApp(ui = ui, server = server)
