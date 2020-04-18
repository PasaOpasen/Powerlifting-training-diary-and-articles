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

data = read_tsv(
  "data(rus).tsv",#"data(eng).tsv",
  skip = 1,
  col_names = F,
  na = "",
  col_types = "fddnfffnnnff",
  comment = "#"
) %>% tbl_df()

colnames(data) = c(
  "Date",
  "RM",
  "MRM",
  "Count",
  "Action",
  "Sex",
  "Experience",
  "Age",
  "Weight",
  "Height",
  "BodyType",
  "Mail"
)
data %<>% #filter(Count<=20) %>%
  arrange(MRM, Count, Weight) %>%  mutate(
    CountGroup = cut(Count, breaks = c(1, 3, 6, 10, 20, 40)),
    AgeGroup = cut(Age, breaks = c(1, 19, 27, 35, 70)),
    Experience = factor(
      Experience,
      levels = c(
        "До двух лет",
        "2-3 года",
        "4-5 лет",
        "6-10 лет",
        "11-15 лет" ,
        "больше 15 лет"
      ),
      ordered = T
    ),
    Index = Weight / (0.01 * Height) ^ 2,
    IndexGroup = cut(Index, breaks = c(0, 16, 18.5, 24.99, 30, 35, 40, 60))
  ) %>% select(-Date)#,-Mail) %>% filter(Count>1,MRM<RM)
levels(data$CountGroup) = c("2-3", "4-6", "7-10", "11-20", ">20")
levels(data$AgeGroup) = c("<20", "20-27", "28-35", ">35")
levels(data$IndexGroup) = c(
  "выраженный дефицит",
  "дефицит",
  "норма",
  "избыток",
  "ожирение1",
  "ожирение2",
  "ожирение3"
)

ex = data$Experience %>% as.numeric()
ex[ex == 6] = 5
ex %<>% factor()
levels(ex) = c("До двух лет", "2-3 года", "4-5 лет", "6-10 лет", "больше 10 лет")
data %<>% mutate(Experience = factor(ex, ordered = T))

allrows = 1:nrow(data)
maxerror = 5

#уникальные записи (где один от каждого человека берётся только одна запись)
#объяснить, по каким признакам людей считать одинаковыми
data.unique = data %>% select(AgeGroup, Height, BodyType, Experience, Sex, IndexGroup) %>% unique()

#функции
getparam = function(vec) {
  ln = length(levels(vec))
  x = numeric(ln)
  ns = character(ln)
  for (i in seq(ln)) {
    x[i] = sum(vec == levels(vec)[i]) / length(vec)
    ns[i] = paste0(levels(vec)[i], " (", round(x[i] * 100, 2), "%)")
  }
  return(list(x = x, ns = ns))
}

getPIE = function(vec, main = "") {
  lst = getparam(vec)
  pie(x = lst$x,
      labels = lst$ns,
      main = main)
}
getFan = function(vec, main = "") {
  pr = getparam(vec)
  fan.plot(pr$x, labels = pr$ns, main = main)
}


data.backup = data




save(data, file = "data.rdata")

dt.save=data %>% select(-Mail,-Experience,-IndexGroup) %>% filter(Count<11) %>% mutate(Mult=MRM*Count)

write_csv(dt.save,'./MLtests/data.csv')

#Разведочный анализ и описание выборки####


psych::describe(data)
summary(data %>% select(-Mail))


getPIE(data.unique$Sex, main = "Пол испытуемых")

getPIE(data.unique$BodyType, main = "Тип телосложения испытуемых")
chisq.test(data.unique %>% filter(Sex == "Мужчина") %>% select(BodyType) %>% table())

getPIE(data$Action, main = "Движение")

par(mfrow = c(1, 1), mai = rep(0.1, 4))
getPIE(data$CountGroup, main = "Диапазон повторений")
getPIE(data$Experience, main = "Опыт тренировок")
getPIE(data$AgeGroup, main = "Возрастная группа")
par(mfrow = c(1, 1))

(
  ggplot(data, aes(x = CountGroup, fill = Action)) + geom_bar() + theme_classic() +
    labs(title = "Количество наблюдений в каждом диапазоне повторений", x =
           "Диапазон повторений", y = "Количество")
) %>% ggplotly()
ggplot(data, aes(x = Experience)) + geom_bar() + theme_classic() + labs(title =
                                                                          "Распределение по опыту тренировок", x = "Опыт тренировок", y = "Количество")
ggplot(data, aes(x = AgeGroup)) + geom_bar() + theme_classic() + labs(title =
                                                                        "Распределение по возрасту", x = "Возрастная группа", y = "Количество")



#среди эндоморфов избыток веса встречается почаще
data.unique %$%  table(BodyType, IndexGroup)

ggplot(data.unique, aes(x = BodyType, fill = IndexGroup)) + geom_bar(position =
                                                                       position_dodge2()) +
  labs(x = "Тип телосложения", y = "Количество ответивших", fill = "Индекс массы тела") +
  coord_flip() + theme_bw() + theme(legend.position = "bottom")

#ggplot(data,aes(x=Age,y=Index))+geom_point()
data %$% cor(Age, Index)
#chisq.test(data.unique%>% filter(IndexGroup=="ожирение1") %>%select(BodyType) %>% table())

tb = data.unique %>% mutate(Obees = ifelse(IndexGroup == "ожирение1", "да", "нет")) %>%
  select(Obees, BodyType) %>% table()
#tb %>% chisq.test()
tb %>% t() %>% prop.test()#для всех
data.unique %>%
  mutate(
    Obees = ifelse(IndexGroup == "ожирение1", "да", "нет"),
    Bd = ifelse(BodyType == 'Эндоморф', 1, 0)
  ) %>%
  select(Obees, Bd) %>% table() %>% t() %>%
  prop.test() %$% conf.int


#tb %>% chisq.test(p=c(1,1,20),rescale.p = T)
if (F) {
  par(mfrow = c(2, 2), mai = rep(0.1, 4))
  
  getPIE(data.unique$Sex, main = "Пол испытуемых")
  getPIE(data.unique$BodyType, main = "Телосложение испытуемых")
  getPIE(data.unique %>% filter(Sex == "Мужчина") %$% BodyType, main = "Телосложение: мужчины")
  getPIE(data.unique %>% filter(Sex == "Женщина") %$% BodyType, main = "Телосложение: женщины")
  
  
  
  par(mfrow = c(1, 1), mai = rep(0.1, 4))
  
  
  getFan(cut(data$Age, breaks = c(0, 20, 30, 40, 100)))
  
  
  
  
  
  data %<>% select(-Age)
  pairs(data %>% select(-Count))
}



GGally::ggpairs(
  data %>% select(-Count, -Mail, -Age, -Sex),
  title = "Диаграммы взаимодействий между переменными в выборке",
  lower = list(combo = "box")
) #%>% ggplotly()


GGally::ggpairs(
  data %>% select(RM, MRM, Action, BodyType),
  title = "Диаграммы взаимодействий между переменными в выборке",
  lower = list(combo = "box")
) #%>% ggplotly()

GGally::ggpairs(
  data %>% select(RM, Action, IndexGroup),
  title = "Диаграммы взаимодействий между переменными в выборке",
  lower = list(combo = "box")
) #%>% ggplotly()


ggplot(data, aes(x = IndexGroup, y = RM)) + geom_boxplot() + facet_wrap(vars(Action)) +
  coord_flip() +
  labs(
    x = "Группа по индексу массы",
    y = "Повторный максимум",
    title = "Зависимость повторного максимума от индекса массы тела",
    subtitle = "Из графика видно, что жим лёжа имеет тенденцию увеличиваться с ростом индекса массы тела. \nОднако для приседа и тяги это верно лишь до некоторого порога"
  ) +
  theme_bw()

ggplot(data, aes(x = IndexGroup, y = RM)) + geom_boxplot() + facet_grid(Action ~
                                                                          BodyType)

(
  ggplot(data, aes(
    x = Index, y = RM, col = BodyType
  )) + geom_point() +
    facet_wrap( ~ Action) + geom_smooth(method = "lm", se = F) +
    theme_bw() + theme(legend.position = "bottom") +
    labs(
      x = "Индекс массы тела",
      y = "Повторный максимум",
      col = "Телосложение",
      title = "Зависимость повторного максимума от индекса массы тела"
    )
) %>% ggplotly()


GGally::ggpairs(
  data %>% select(
    -Count,
    -Mail,
    -Age,
    -Sex,
    -Index,
    -Height,
    -CountGroup,
    -AgeGroup,
    -Weight
  ),
  title = "Диаграммы взаимодействий между переменными в выборке",
  lower = list(combo = "box")
) #%>% ggplotly()





data %>% ggplot(aes(x = factor(Count), y = RM / MRM - 1)) + geom_boxplot() +
  theme_bw()
#data %>% ggplot(aes(x=CountGroup,y=RM/MRM))+geom_boxplot()+theme_bw()
data %>% ggplot(aes(x = factor(Count), y = (RM / MRM - 1) / Count)) +
  geom_boxplot() + theme_bw() +
  geom_hline(yintercept = 0.0333,
             size = 1.3,
             col = "red") +
  labs(x = "Число повторений",
       title = "Оценка параметра для разного числа повторений",
       subtitle = "Красным цветом обозначен параметр из книги Лилли. Как видно, он может быть верен для числа повторений от 2 до 5")


data.ct = data %>% filter(Count <= 10 | Count == 12)

#По этому соотношению надо бы выбросы удалить
prc = data.ct %>% ggplot(aes(x = factor(Count), y = 100 * MRM / RM)) + geom_boxplot() +
  coord_flip() + theme_bw() +
  labs(x = "Число повторений", y = "Какой процент составляет МПМ от ПМ")
prc %>% ggplotly()

#(prc+facet_grid(vars(Action))) %>% ggplotly()

cors = sapply(2:10, function(x)
  data %>% filter(Count == x) %$% cor(RM, MRM))
names(cors) = paste(2:10, "repeats")
cors




#есть ли разница в проценте в зависимости от чего-то
cat("p-значения для телосложений:\n")
pvalues = sapply(2:10, function(x)
  data %>% filter(Count == x) %$% aov(MRM / RM ~ BodyType, .) %>% summary() %$% .[[1]][["Pr(>F)"]][1])
names(pvalues) = names(cors)
pvalues
cat("p-значения для типа движения:\n")
pvalues = sapply(2:6, function(x)
  data %>% filter(Count == x) %$% aov(MRM / RM ~ Action, .) %>% summary() %$% .[[1]][["Pr(>F)"]][1])
names(pvalues) = paste(2:6, "repeats")
pvalues
cat("p-значения для групп по индексу массы:\n")
pvalues = sapply(2:10, function(x)
  data %>% filter(Count == x) %$% aov(MRM / RM ~ IndexGroup, .) %>% summary() %$% .[[1]][["Pr(>F)"]][1])
names(pvalues) = paste(2:10, "repeats")
pvalues




df = data %>% mutate(perc = 100 * MRM / RM) %>% filter(Count <= 10 |
                                                         Count == 12 | Count == 15 | Count == 20) %>%
  group_by(factor(Count)) %>%
  summarise(
    mean = t.test(perc, conf.level = 0.99)$estimate,
    down = t.test(perc, conf.level = 0.99)$conf.int[1],
    up = t.test(perc, conf.level = 0.99)$conf.int[2]
  )
names(df) = c("Число повторений",
              "Ожидаемый %",
              "Нижняя граница",
              "Верхняя граница")
df



#как насчёт какой-то такой модели?####
lm(MRM / RM ~ Count + I(Count ^ 2) + I(MRM / Weight), data) %>% summary()
lm(MRM / RM ~ Count:CountGroup, data) %>% summary()
lm(MRM / RM ~ Count + I(Count ^ 2) + I(MRM / Weight), data) %>% plot()

gr = data$CountGroup %>% as.numeric()
cors = function(inds)
  cor(data$RM[inds], data$MRM[inds])

cors(gr < 2)
cors(gr == 2)
cors(gr == 3)
cors(gr == 4)
cors(gr == 5)

m = lm(RM ~ MRM:factor(Count) - 1, data)
m %>% summary()
cf = coefficients(m)[1:7]
cf - cf[4]
coefficients(m)[1:7] %>% plot(x = 2:8, type = "b")

#по этому графику видно, что повторения делятся на линейные группы, то есть в пределах группы они дают одинаковый прирост
plot(seq(-10, 10, length.out = 50) %>% map_dbl(function(x)
  1 / (1 + exp(-x))))

# а что если там квадратичная зависимость от 2 до 6?
m = lm(RM ~ MRM:I((Count - 1) ^ 2) - 1, data %>% filter(Count < 7))


sigma = nls(
  v ~ 1 + b / (1 + exp(-k * n)),
  data = data.frame(v = coefficients(m)[1:7], n = 1:7),
  start = list(b = 0.3, k = 1)
)





GGally::ggcorr(data, label = T, label_round = 2)
data %$% cor(RM, MRM * Count)

plt = data %>% ggplot(aes(x = MRM, y = RM)) + geom_smooth() +
  geom_point(aes(col = CountGroup), size = 3) + theme_bw() +
  #facet_grid(vars(CountGroup))+
  labs(x = "Многоповторный максимум",
       y = "Повторный максимум",
       col = "Диапазон повторений",
       title = "Зависимость повторного максимума от многоповторного")
plt + theme(legend.position = c(0.9, 0.2))

plt + facet_grid(vars(CountGroup))

plt + facet_grid(vars(CountGroup),scales="free")+theme(legend.position = "bottom")


byCountGroup = ggplot(data, aes(y = (RM / MRM - 1) / Count, col = BodyType)) +
  facet_grid(vars(CountGroup)) + theme_bw()
byCountGroup + geom_point(aes(x = Age))
byCountGroup + geom_point(aes(x = Weight))


ggplot(data, aes(x = AgeGroup, y = RM / MRM)) + geom_boxplot() +
  facet_grid(vars(CountGroup)) + theme_bw() +
  labs(x = "Опыт тренировок")

#есть ли значимые различия в разных возрастных группах для фиксированного диапазона
cat('p-значения в зависимости от опыта: \n')
(sapply(levels(data$CountGroup), function(x)
  aov(RM / MRM ~ Experience, data %>% filter(CountGroup == x)) %>% summary() %$% .[[1]][["Pr(>F)"]][1]))

cat('p-значения в зависимости от возрастной группы: \n')
(sapply(levels(data$CountGroup), function(x)
  aov(RM / MRM ~ AgeGroup, data %>% filter(CountGroup == x)) %>% summary() %$% .[[1]][["Pr(>F)"]][1]))


cat('p-значения в зависимости от индекса массы тела: \n')
(sapply(levels(data$CountGroup), function(x)
  aov(RM / MRM ~ IndexGroup, data %>% filter(CountGroup == x)) %>% summary() %$% .[[1]][["Pr(>F)"]][1]))

cat('p-значения в зависимости от типа телосложения: \n')
(sapply(levels(data$CountGroup), function(x)
  aov(RM / MRM ~ BodyType, data %>% filter(CountGroup == x)) %>% summary() %$% .[[1]][["Pr(>F)"]][1]))


ggplot(data, aes(x = AgeGroup, y = RM / MRM)) + geom_boxplot() +
  facet_grid(vars(CountGroup)) + theme_bw() +
  labs(x = "Возраст")

ggplot(data, aes(x = IndexGroup, y = RM / MRM)) + geom_boxplot() +
  facet_grid(vars(CountGroup)) + theme_bw() +
  labs(x = "Возраст")


cat('p-значения в зависимости от типа телосложения: \n')
(sapply(levels(data$CountGroup), function(x)
  aov(RM / MRM ~ Action, data %>% filter(CountGroup == x)) %>% summary() %$% .[[1]][["Pr(>F)"]][1]))

ggplot(data %>% filter(Count <= 20), aes(x = Action, y = RM / MRM)) + geom_boxplot() +
  facet_grid(vars(CountGroup)) + theme_bw() +
  labs(x = "Движение")



aov(cor(RM, MRM) ~ Experience, data %>% filter(CountGroup == "2-3")) %>% summary() %$% .[[1]][["Pr(>F)"]][1]

###

obj = ggplot(data %>% select(-Count)) + theme_bw()
obj + geom_bar(aes(x = CountGroup))
obj + geom_bar(aes(x = BodyType))
obj + geom_point(aes(
  x = Weight,
  y = Height,
  col = BodyType,
  shape = Experience
), size = 5) + facet_grid(vars(Action), vars(BodyType))
obj + geom_boxplot(aes(x = Action, y = RM))


bx = ggplot(data) + geom_boxplot(aes(x = CountGroup, y = RM / MRM)) +
  labs(
    title = "Отношение повторного максимума к многоповторному в зависимости от числа повторений",
    x = "Диапазон повторений",
    y = "Отношение повторного максимума к многоповторному",
    caption = "Каждый диапазон повторений действует по своим законам \n и нуждается в собственной версии модели"
  ) +
  theme_tq()
bx

bx + facet_grid( ~ BodyType) + theme_bw() + labs(caption = "Для числа повторений до 10 тип телосложения не имеет значения. На высоком числе повторений эндоморфы менее выносливы")
#bx+facet_grid(~Sex)+theme_bw()+labs(caption="caption")
bx + facet_grid( ~ Action) + theme_bw() + labs(caption = "Присед имеет хорошую выносливость на большом числе повдторений, жим -- на маленьком",
                                               subtitle = "Чем отношение ниже, тем более 'выносливы' мышцы в том или ином диапазоне")













data %<>% filter(Count <= 20)

data %>% summary()

plt = ggplot(data) + theme_bw()

plt + geom_density(aes(x = RM), fill = "green") + geom_density(aes(x = MRM, fill =
                                                                     "red", alpha = 0.5)) +
  labs(x = "RM (зелёное), MRM (красное)", y = "ядерная плотность", title = 'Плотность распределения повторного максимума и многоповторного максимума') +
  theme(legend.position = 'none')

plt + geom_bar(aes(x = factor(Count)))

plt + geom_bar(aes(x = CountGroup, fill = BodyType), position = position_dodge2()) +
  theme(legend.position = c(.85, .9)) +
  labs(x = "Диапазон повторений",
       y = "Количество",
       title = "Количество наблюдений в каждом диапазоне повторений",
       fill = "Телосложение")

plt + geom_bar(aes(x = IndexGroup, fill = BodyType), position = position_dodge2()) +
  theme(legend.position = c(.85, .9)) +
  labs(x = "Категория по индексу массы тела",
       y = "Количество",
       title = "Количество наблюдений в каждой категории по индексу массы тела",
       fill = "Телосложение")

plt + geom_bar(aes(x = AgeGroup, fill = BodyType), position = position_dodge2()) +
  theme(legend.position = c(.85, .9)) +
  labs(x = "Категория по возрасту",
       y = "Количество",
       title = "Количество наблюдений в каждой категории по возрасту",
       fill = "Телосложение")

plt + geom_bar(aes(x = Action, fill = BodyType), position = position_dodge2()) +
  theme(legend.position = c(.85, .9)) +
  labs(x = "Движение",
       y = "Количество",
       title = "Количество наблюдений в каждом движении",
       fill = "Телосложение")

plt + geom_point(aes(
  x = Weight,
  y = Height,
  col = AgeGroup,
  shape = Sex
), size = 2.5) +
  facet_grid(vars(Action), vars(BodyType)) +
  labs(
    x = "Вес",
    y = "Рост",
    shape = "Пол",
    col = "Возраст",
    title = "Зависимость между ростом и весом"
  )


sapply(data[sapply(data, is.numeric)], function(x)
  shapiro.test(x)$p.value)



#Функция ошибок####

data %<>% filter(Count <= 20)
data %>% summary()

Error = function(target, weight)
  (target - weight) ^ 2 %>% mean() %>% sqrt()

Show = function(vals, df = data) {
  #vals=predict(model,df)
  err = df$RM - vals
  cbind(
    Fact = round(vals),
    Target = df$RM,
    Set = paste0(df$MRM, "*", df$Count),
    ERROR = abs(df$RM - vals),
    ErrorPercent = abs(err) / df$RM * 100,
    df[, c(3:15)]
  ) %>% tbl_df() %>% select(-Count, -Mail, -Experience, IndexGroup) %>% arrange(-ERROR, -ErrorPercent, Weight) %>%
    filter(ERROR > 1) %>% View()
  cat("\n")
  rg = range(err)#;print(err);print(rg)
  
  if (rg[1] < 0)
    cat("------------> Наибольшая ошибка в большую сторону:",
        -rg[1],
        "\n")
  if (rg[2] > 0)
    cat("------------> Наибольшая ошибка в меньшую сторону:", rg[2], "\n")
  
  s = sum(abs(err) / df$RM * 100 > maxerror)
  len = length(err)
  cat(
    "Модель ошиблась более чем на",
    maxerror,
    "% в",
    s,
    "случаях из",
    len,
    "(",
    s / len * 100,
    "%)\n"
  )
  s = sum(abs(err) > maxerror)
  cat(
    "Модель ошиблась более чем на",
    maxerror,
    "кг в",
    s,
    "случаях из",
    len,
    "(",
    s / len * 100,
    "%)\n"
  )
  
  cat("----------------> Статистика по ошибкам в процентах:\n")
  (abs(df$RM - vals) / df$RM * 100) %>% summary() %>% print()
  cat("-------------------> Среднеквадратичная ошибка:",
      Error(vals, df$RM),
      "\n")
}

ShowErrors = function(model,
                      power.coef = 1,
                      sum.coef = 0) {
  Show(predict(model, data) * power.coef + sum.coef)
  
  cat(
    "Оценка кросс-валидации для всего набора данных",
    boot::cv.glm(data, glm(
      formula = model$call$formula, data = data
    ), K = 10)$delta[1],
    "\n"
  )
  cat(
    "Оценка кросс-валидации для не более чем 10 повторений",
    boot::cv.glm(
      data %>% filter(Count < 11),
      glm(
        formula = model$call$formula,
        data = data %>% filter(Count < 11)
      ),
      K = 10
    )$delta[1],
    "\n"
  )
  cat(
    "Оценка кросс-валидации для не более чем 6 повторений",
    boot::cv.glm(
      data %>% filter(Count < 7),
      glm(
        formula = model$call$formula,
        data = data %>% filter(Count < 7)
      ),
      K = 10
    )$delta[1],
    "\n"
  )
}


ResAn = function(res) {
  p = ggplot(data %>% mutate(res = res), aes(x = CountGroup, y = res)) +
    geom_boxplot() + labs(x = "Группа повторений", y = "Остатки (цель - предсказание)", title =
                            "Распределения остатков в зависимости от группы повторений") + theme_bw()
  print(p)
  
  (p + facet_grid(vars(Action))) %>% print()
  
  (p + facet_grid(vars(BodyType), vars(Action))) %>% print()
  return(0)
}

#из этого графика можно сделать вывод, что модель неплохо работает на диапазоне 2-3, но на диапазоне 13-20 ошибка какая-то сильно отличающаяся от тенденции уменьшения ошибок, так что этот диапазон надо бы и вообще убрать, так как там уже играют роль свойства красных волокон, не говорящие о силе
ResAn(data$RM - data$MRM * (1 + 0.0333 * data$Count))

ResVal = function(vals)
  ResAn(data$RM - vals)

#ResGraf=function(model)ResVal(predict(model,data))

mysummary = function(mdl) {
  cat("-----> ОБЩАЯ ИНФОРМАЦИЯ О МОДЕЛИ:\n")
  cat("\n")
  gvlma::gvlma(mdl) %>% summary()
  cat("\n")
  
  
  cat("-----> БАЗОВЫЕ ГРАФИКИ:\n")
  cat("\n")
  par(mfrow = c(2, 2))
  plot(mdl)
  par(mfrow = c(1, 1))
  cat("\n")
  
  
  cat("-----> ТЕСТ НА НОРМАЛЬНОСТЬ РАСПРЕДЕЛЕНИЯ ОСТАТКОВ\n")
  cat("\n")
  shapiro.test(mdl$residuals) %>% print()
  cat("\n")
  
  qqPlot(mdl, main = "Q-Q plot")
  
  
  cat("-----> ФАКТОР ИНФЛЯЦИИ ДИСПЕРСИЙ:\n")
  cat("\n")
  vif(mdl) %>% print()
  cat("\n")
  
  cat("-----> ТЕСТ НА АВТОКОРРЕЛЯЦИЮ:\n")
  cat("\n")
  durbinWatsonTest(mdl) %>% print()
  cat("\n")   #тест на автокорреляцию
  
  # cat("-----> ТЕСТ НА ОДНОРОДНОСТЬ ДИСПЕРСИИ:\n");cat("\n")
  # ncvTest(mdl)%>% print();cat("\n")    #однородность дисперсии
  
  cat("-----> ТЕСТ НА ВЫБРОСЫ И ВЛИЯТЕЛЬНЫЕ НАБЛЮДЕНИЯ:\n")
  cat("\n")
  
  outs = outlierTest(mdl)
  outs %>% print()
  
  
  influ = influencePlot(mdl, main = "Диаграмма влияния", sub = "Размеры кругов пропорциональны расстояниям Кука")
  influ %>% print()
  
  cat("-----> ВЫБРОСЫ И ВЛИЯТЕЛЬНЫЕ НАБЛЮДЕНИЯ:\n")
  cat("\n")
  data[c(outs$p %>% names(), influ %>% rownames()) %>% as.numeric(), ] %>% unique() %>%
    select(-Mail) %>% arrange(-RM, -Count) %>% print()
  cat("\n")
  
}

all = function(modelka) {
  modelka %>% ShowErrors()
  
  modelka %>% predict(data) %>% ResVal()
  
  modelka %>% mysummary()
}

data %<>% mutate(
  Body2 = ifelse(BodyType == "Эндоморф", "Endo", "NonEndo") %>% factor(),
  Action2 = ifelse(Action == "Жим", "Up", "Down") %>% factor()
)



md %>% mysummary()

m1 = lm(RM ~ MRM + MRM:Count - 1, data)

m1 %>% ShowErrors()

m1 %>% predict(data) %>% ResVal()

m1 %>% mysummary()


b1 = lm(RM ~ MRM + MRM:Count - 1, data)

#lm(RM~MRM:Count+MRM:CountGroup-1,data) %>% ShowErrors()

lm(RM ~ MRM:Count:CountGroup + MRM - 1, data) %>% ShowErrors()

lm(RM ~ MRM:Count:CountGroup + MRM:I(Count ^ 2) + MRM - 1, data) %>% ShowErrors()

lm(RM ~ MRM:Count:CountGroup + MRM:Action - 1, data) %>% ShowErrors()

#lm(RM~MRM:Count:CountGroup+MRM:Action2-1,data) %>% ShowErrors()

lm(RM ~ MRM:Count:CountGroup + MRM:Action + MRM:Index:Experience - 1,
   data) %>% ShowErrors()

lm(RM ~ MRM:Count:CountGroup + MRM:Action + Index - 1, data) %>% ShowErrors()


lm(RM ~ MRM:Count:CountGroup + MRM:Action + I(MRM / Weight * Index) - 1,
   data) %>% ShowErrors()

lm(
  RM ~ MRM:Count:CountGroup + MRM:Action + I(MRM / Weight * Index) + sqrt(Count):CountGroup:MRM -
    1,
  data
) %>% ShowErrors()


lm(RM ~ MRM:Count:CountGroup + MRM:CountGroup - 1, data) %>% all()
lm(RM ~ MRM:Count:CountGroup + MRM:Action - 1, data) %>% all()


lm(RM ~ MRM:Count:CountGroup + MRM:Action + MRM:Body2 - 1, data) %>% all()

lm(RM ~ MRM:Count:CountGroup + MRM:Action + I(Count ^ 2):MRM - 1, data) %>% all()


#надо удалить диапазон выше 10
data$CountGroup %>% table()
data %<>% filter(Count < 11)

b2 = lm(RM ~ MRM:Count:CountGroup + MRM:CountGroup - 1, data)
b2 %>% all()
b3 = lm(RM ~ MRM:Count:CountGroup + MRM:Action - 1, data) #%>% all()

best = lm(RM ~ MRM:Count:CountGroup + MRM:Action + I(Count ^ 2):MRM - 1, data)


stp = lm(
  RM ~ MRM:Count:CountGroup +
    MRM:CountGroup +
    # MRM:CountGroup:Action+
    #MRM:CountGroup:Action2+
    # MRM:CountGroup:BodyType+
    # MRM:CountGroup:Body2+
    MRM:Action +
    MRM:Action2 +
    I(Count ^ 2):MRM - 1 +
    #I(MRM/Weight):Action+
    MRM:BodyType:Count +
    MRM:Body2:Count +
    I(MRM / Weight):Action2 +
    MRM:Body2:Count +
    I(MRM ^ 2) +
    sqrt(MRM) +
    I(Index / MRM) +
    AgeGroup:Experience +
    I(MRM / Weight):AgeGroup:Experience +
    I(MRM / Index) +
    I((MRM / Index) ^ 2) +
    I((MRM / Index) ^ 3) +
    sqrt(MRM / Index) +
    log(MRM / Index) +
    #poly(MRM/Index,3)+
    poly(Index / MRM, 3)
  ,
  data #%>% filter(Count<11)
) %>% step(
  direction = "both",
  scope = (
    ~ . +
      I(Count ^ 2):CountGroup:MRM +
      MRM:BodyType +
      MRM:Count:BodyType +
      MRM:Experience +
      MRM:AgeGroup:Experience +
      MRM:Count:Experience +
      MRM:Count:CountGroup:BodyType +
      MRM:CountGroup:BodyType +
      I(MRM * Count / Weight):CountGroup +
      I(MRM * Weight / (Height - 100)) +
      I((MRM - 100) / MRM) +
      poly(MRM / Weight, 2) +
      MRM + MRM:Count +
      MRM:Weight:BodyType
  ),
  steps = 5000
)


stp %>% all()



b4 = lm(RM ~ MRM:Action + MRM:I(Count ^ 2) + Action2:I(MRM / Weight) + MRM:Count:CountGroup - 1,
        data)
b4 %>% all()

#b5=lm(RM ~ I(MRM/Index) + MRM:CountGroup + MRM:Action + MRM:CountGroup:Count - 1,data)
#b5 %>% all()


b5 = lm(RM ~ I((MRM / Index) ^ 6) + MRM:CountGroup + MRM:Action + MRM:CountGroup:Count - 1,
        data)
b5 %>% all()


stp = b6 %>% step(
  direction = "both",
  scope = (
    ~ . +
      I(Count ^ 2):CountGroup:MRM +
      MRM:BodyType +
      MRM:Body2 +
      MRM:I(Count ^ 2) +
      MRM:Count:BodyType +
      MRM:Experience +
      MRM:AgeGroup:Experience +
      MRM:Count:Experience +
      MRM:Count:CountGroup:BodyType +
      MRM:CountGroup:BodyType +
      MRM:Count:CountGroup:Body2 +
      MRM:CountGroup:Body2 +
      I(MRM * Count / Weight):CountGroup +
      I(MRM * Weight / (Height - 100)) +
      I((MRM - 100) / MRM) +
      poly(MRM / Weight, 2) +
      MRM +
      MRM:Count +
      MRM:Body2 +
      MRM:Action2 +
      MRM:Weight:BodyType +
      I((MRM / Index) ^ 2) +
      I((MRM / Index) ^ 3) +
      I((MRM / Index) ^ 4) +
      I((MRM / Index) ^ 5) +
      I((MRM / Index) ^ 6) +
      I((MRM / Index) ^ 7) +
      I((MRM / Index) ^ 8) +
      I((MRM / Index) ^ 9) +
      I((MRM / Index) ^ 0.5) +
      log(MRM / Index) +
      I(log(MRM / Index) ^ 2) +
      I((MRM / Index) ^ 2):Body2 +
      I((MRM / Index) ^ 2):Action2 +
      I((MRM / Weight * ((
        Height - 100
      ) / 100) ^ 2))
  ),
  steps = 5000
)

stp %>% all()

#data%$%plot(MRM/Index,RM)
#data%$%plot(MRM/Index,log(RM))
#data%$%plot(MRM*Index,RM)
#data%$%plot(Index/RM,RM)
#data%$%plot(Index/RM,log(RM))

#lm(RM~I(MRM/Index/Count)-1,data) %>% all()



#b2=lm(RM/MRM ~ I(1/Index) + CountGroup + Action + CountGroup:Count - 1,data)
#b2 %>% ShowErrors(power.coef = data$MRM)



#nb=rep(1:5,2)

kn = c(5, 6, 7, 8, 9, 10, 11, 12)

ct = c(8, 11)

gr = rep(c('2-10', '2-7'), length(kn)) %>% sort(decreasing = T)

m = matrix(nrow = length(kn) * length(ct), ncol = 5)

lst = list(b1, b2, b3, b4, b5)


for (i in 1:5) {
  model = lst[[i]]
  
  for (j in 1:length(ct)) {
    dt = data %>% filter(Count < ct[j])
    gl = glm(formula = model$call$formula, data = dt)
    
    getval = function(k) {
      boot::cv.glm(dt, gl, K = k)$delta[1] %>% return()
    }
    
    getval.mean = function(k, count) {
      map_dbl(1:count, function(x)
        getval(k)) %>% mean() %>% return()
    }
    
    beg = (j - 1) * length(kn)
    
    for (s in 1:length(kn)) {
      m[beg + s, i] = getval.mean(kn[s], 30)
    }
    #print(m)
  }
  
}

colnames(m) = paste0('b', 1:5)
kp = rep(kn, length(ct))

vals = data.frame(
  kp = rep(kp, 5),
  b = as.numeric(m),
  gr = factor(rep(gr, 5)),
  n = factor(rep(colnames(m), length(kn) * length(ct)) %>% sort())
) %>%
  tbl_df()



ggplot(vals, aes(x = kp, y = b, col = n)) + theme_bw() + facet_grid(vars(gr), scales="free_y") +
  geom_point(size = 4) + geom_line(size = 1.) +
  labs(
    x = "Количество блоков при перекрёстной проверке",
    y = "Усреднённые значения ошибок после 30 повторных проверок",
    col = "Модель",
    title = "Оценки качества моделей при перекрёстной проверке",
    subtitle = "Оценка производилась на разных подмножествах данных (диапазонах повторений)",
    caption = "Очевидно, что пятая модель превосходит остальные по точности"
  ) +
  scale_x_continuous(breaks = kn) +
  theme(legend.position = "bottom")


ggplot(vals, aes(x = kp, y = b, col = n)) + theme_bw() + facet_grid(vars(gr), scales="free_y") +
  geom_point(aes(shape=n),size = 5) + geom_line(aes(linetype=n),size = 1.) +
  labs(
    x = "Количество блоков при перекрёстной проверке",
    y = "Усреднённые значения ошибок после 30 повторных проверок",
    shape = "Модель",
    title = "Оценки качества моделей при перекрёстной проверке",
    subtitle = "Оценка производилась на разных подмножествах данных (диапазонах повторений)",
    caption = "Очевидно, что пятая модель превосходит остальные по точности"
  ) +guides(col=FALSE,linetype=F)+
  scale_x_continuous(breaks = kn) +
  theme(legend.position = "bottom")



save(vals, file = "CVvals.rdata")
save(vals, file = "CVvals(more_points).rdata")
save(b5, file = "b5_fit.rdata")















kn = c(5, 6, 7, 8, 9, 10, 11, 12)

ct = c(21, 50)

gr = rep(c('11-20', '>20'), length(kn)) %>% sort(decreasing = T)

m = matrix(nrow = length(kn) * length(ct), ncol = 5)

lst = list(b1, b2, b3, b4, b5)


for (i in 1:5) {
  model = lst[[i]]
  
  for (j in 1:length(ct)) {
    dt = data.backup %>% filter(Count < ct[j])
    gl = glm(formula = model$call$formula, data = dt)
    
    getval = function(k) {
      boot::cv.glm(dt, gl, K = k)$delta[1] %>% return()
    }
    
    getval.mean = function(k, count) {
      map_dbl(1:count, function(x)
        getval(k)) %>% mean() %>% return()
    }
    
    beg = (j - 1) * length(kn)
    
    for (s in 1:length(kn)) {
      m[beg + s, i] = getval.mean(kn[s], 30)
    }
    #print(m)
  }
  
}

colnames(m) = paste0('b', 1:5)
kp = rep(kn, length(ct))

vals = data.frame(
  kp = rep(kp, 5),
  b = as.numeric(m),
  gr = factor(rep(gr, 5)),
  n = factor(rep(colnames(m), length(kn) * length(ct)) %>% sort())
) %>%
  tbl_df()



ggplot(vals, aes(x = kp, y = b, col = n)) + theme_bw() + facet_grid(vars(gr)) +
  geom_point(size = 4) + geom_line(size = 1.) +
  labs(
    x = "Количество блоков при перекрёстной проверке",
    y = "Усреднённые значения ошибок после 30 повторных проверок",
    col = "Модель",
    title = "Оценки качества моделей при перекрёстной проверке",
    subtitle = "Оценка производилась на разных подмножествах данных",
    caption = "Очевидно, что пятая модель превосходит остальные по точности"
  ) +
  scale_x_continuous(breaks = kn) +
  theme(legend.position = "bottom")





#caret####

library(caret)


tr = trainControl(
  method = "repeatedcv",
  number = 10,
  #p = 0.75,
  repeats = 10,
  verboseIter = F,
  returnResamp = "all",
  savePredictions = T,
  summaryFunction = defaultSummary
)


d2=data %>% select(MRM,Count,Weight,Height,Index,Action,CountGroup,IndexGroup) %>% 
  mutate_all(as.numeric) %>% 
  mutate(pow=MRM*Count,add=(MRM/Index)^6)

d3=d2 %>% mutate(b5=predict(b5,data),n14=predict(n14,data))


mths=c("ridge","lasso","blassoAveraged","enet","monmlp")

tbm=c('bstTree','rpart','rpart1SE','rpart2','ctree','xgbDART','xgbTree','M5','nodeHarvest')

mars=c('bagEarth','bagEarthGCV','earth','gcvEarth','brnn')

rf=c('cforest','parRF','qrf','ranger','rf','extraTrees','RRF','RRFglobal')


cvs.print=function(method.array,n=10,reps=10){
  
  tr = trainControl(
    method = "repeatedcv",
    number = n,
    repeats = reps,
    verboseIter = T,
    returnResamp = "all",
    savePredictions = T,
    summaryFunction = defaultSummary
  )
  
  cvs=sapply(method.array, function(ft){
  
  t=Sys.time()
  tt= train(
    y=data$RM,
    x = d3,
    method = ft,
    metric =  "RMSE",
    maximize = FALSE,
    trControl = tr
  )
  
  t=Sys.time()-t
  
  return(list(value=tt$control$seeds[[1+n*reps]],time=unclass(t)[[1]]))
})
  
  rs=c(cvs[[1]],cvs[[2]])
dm=dim(cvs)
for(i in seq(3,dm[1]*dm[2],by=2)){
  rs=rbind(rs,c(cvs[[i]],cvs[[i+1]]))
}
dm=dimnames(cvs)
colnames(rs)=dm[[1]]
rownames(rs)=dm[[2]]

rs %<>% as.data.frame()
rs[order(rs$value,rs$time),] %>% print()

}

cvs.print(rf,10,1)

cvs.print2=function(method.array,n=10,reps=10){
  
  tr = trainControl(
    method = "repeatedcv",
    number = n,
    repeats = reps,
    verboseIter = F,
    returnResamp = "final",
    savePredictions = T,
    summaryFunction = defaultSummary
  )
  aa=list()
  
  cvs=for(ft in method.array){
    
   aa[[ft]]= train(
      y=data$RM,
      x = d3,
      method = ft,
      metric =  "RMSE",
      maximize = FALSE,
      trControl = tr
    )
  }
  
  results=resamples(aa)
  
  # summarize the distributions
  summary(results) %>% print()
  # boxplots of results
  bwplot(results)%>% print()
  # dot plots of results
  dotplot(results)%>% print()
  
}

cvs.print2(rf,10,1)

trtr=trainControl(
    method = "repeatedcv",
    number = 10,
    #p = 0.75,
    repeats = 2,
    verboseIter = F,
    returnResamp = "final",
    savePredictions = T,
    summaryFunction = defaultSummary
  )

ft1 = train(
  y=data$RM,
  x = d3,
  method = "M5",
  metric =  "RMSE",
  maximize = FALSE,
  trControl = trtr,
  tuneLength = 50
)


save(ft,file="xgb.rdata")

modelLookup("M5")

# 4.90
ft2 = train(
  y=data$RM,
  x = d3,
  method = "ridge",
  metric =  "RMSE",
  maximize = FALSE,
  trControl = trtr
)


ft %>% predict(d3) %>% Show()



#4.89
ft3 = train(
  y=data$RM,
  x = d3,
  method = "lasso",
  metric =  "RMSE",
  maximize = FALSE,
  trControl = trtr
)
ft %>% summary()
ft$control$seeds[[1+10*10]]
ft1 %>% predict(d3) %>% Show()

modelLookup("M5")


results <- resamples(list('LVQ'=ft1, 'GBM'=ft2, 'SVM'=ft))
# summarize the distributions
summary(results)
# boxplots of results
bwplot(results)
# dot plots of results
dotplot(results)




#4.75
ft = train(
  RM ~ I((MRM / Index) ^ 6) + MRM:CountGroup + MRM:Action + MRM:CountGroup:Count - 1,
  
  data = data %>% select(-Mail,-Sex,-Age,-Height,-Weight),
  method = "lmStepAIC",
  scope = list(upper =  ~ . ^ 3,
               lower =  ~ 1),
  direction = "both",
  
  metric =  "RMSE",
  maximize = FALSE,
  trControl = trtr
)


cv.caret=function(df = d3,
                           fit,
                           k = 10,
                           repets = 1) {
  sm = 0
  r = 0
  RM = data$RM
  
  repeat {
    b = T
    while (b) {
      val = tryCatch({
        b = F
        
        blocks = sample.int(k, nrow(df), replace = T)
        
        future.apply::future_sapply(seq(k), function(i) {
          ft = train(
            y=data$RM,
            x = d3,
            method = fit,
            metric =  "RMSE",
            maximize = FALSE,
            trControl = tr
          )
          sum((RM[blocks == i] - predict(ft, df[blocks == i,])) ^ 2) %>% return()
        }) %>% sum()
        
      },
      error = function(cond) {
        # print(cond)
        b = T
      })
    }
    
    
    
    sm = sm + val / k
    r = r + 1
    
    if (r == repets) {
      break
    }
    
  }
  
  return(sm / repets)
}






#Модели####


#начальная
Show(data$MRM * (1 + 0.0333 * data$Count))

#оптимизация чисто коэффициента
md = nls(
  RM ~ MRM * (s + coef * Count) ^ t + k * Weight / MRM,
  data = data,
  start = list(
    s = 1,
    coef = 0.0333,
    t = 1,
    k = 0
  )
)
summary(md)
Show(predict(md, data))
ResVal(predict(md, data[3:4]))

md = nls(RM ~ MRM * (1 + coef * Count),
         data = data,
         start = list(coef = 0.0333))
summary(md)
Show(predict(md, data))


md = nls(
  RM ~ MRM ^ vk * (1 + coef * Count) ^ kk,
  data = data,
  start = list(coef = 0.0333, vk = 1., kk = 1.)
)
summary(md)
Show(predict(md, data))


md = nls(
  RM ~ MRM * (1 + coef * Count) ^ (kk + Count * t),
  data = data,
  start = list(coef = 0.033, kk = 0.01, t = 0)
)
summary(md)
Show(predict(md, data))

#оптимизация чисто коэффициента c поправкой на его группу
md = lm(I(RM / MRM - 1) ~ MRM:Count:CountGroup - 1, data)
summary(md)
Show((predict(md, data %>% select(
  MRM, Count, CountGroup
)) + 1) * data$MRM)
ResVal((predict(md, data %>% select(
  MRM, Count, CountGroup
)) + 1) * data$MRM)


#надо глянуть это:
md = lm(I(RM / MRM - 1) ~ Count:CountGroup - 1, data)
ShowErrors(md, data$MRM, data$MRM)
md = lm(I(RM - MRM) ~ MRM:Count:CountGroup - 1, data)
ShowErrors(md, sum.coef = data$MRM)
md = lm(RM ~ MRM + MRM:Count:CountGroup - 1, data)
md = lm(RM ~ MRM:CountGroup + MRM:Count:CountGroup + I((Height - 120) /
                                                         Weight):Count:CountGroup - 1,
        data)

md = lm(I(RM ^ 2) ~ MRM + MRM:Count - 1, data)
Show(predict(md, data) %>% sqrt())

md = lm(sqrt(RM) ~ MRM + MRM:Count - 1, data)
Show(predict(md, data) ^ 2)


md = lm(RM ~ MRM:Count + MRM:CountGroup - 1, data)

Show(predict(md, data)[data$Count < 11], data %>% filter(Count < 11))
############################################################################################################
#MRM+MRM*Count с поправкой на группу####
md = lm(RM ~ MRM:Count:CountGroup + MRM:CountGroup - 1, data)
summary(md)
Show(predict(md, data))
ShowErrors(md)
ResVal(predict(md, data %>% select(MRM, Count, CountGroup)))

md = lm(RM ~ MRM:Count:CountGroup + MRM:CountGroup + MRM + MRM:Count - 1,
        data)
md = lm(RM ~ MRM:Count:CountGroup + MRM - 1, data)
md = lm(RM ~ MRM:Count:CountGroup + MRM:BodyType - 1, data)

md = lm(RM ~ MRM:CountGroup:Count + MRM:Action:BodyType - 1, data)

#md=lm(RM~MRM:Count+MRM-1,data,subset = data$Count<4)
md = step(
  md,
  direction = "both",
  scope = (
    ~ . +
      MRM:BodyType +
      MRM:Count:BodyType +
      MRM:Experience +
      MRM:AgeGroup:Experience +
      MRM:Count:Experience +
      MRM:Count:CountGroup:BodyType +
      MRM:CountGroup:BodyType +
      I(MRM * Count / Weight):CountGroup +
      I(MRM * Weight / (Height - 100)) +
      I((MRM - 100) / MRM) +
      poly(MRM / Weight, 2) +
      MRM + MRM:Count +
      MRM:Weight:BodyType
  ),
  steps = 5000
)

#md=regsubsets(md$call$formula,data=data,nbest = 10)


summary(md)
Show(predict(md, data))
ResVal(predict(md, data %>% select(MRM, Count, CountGroup)))


md = lm(
  RM ~ MRM + MRM:Count - 1 + MRM:BodyType:Count + MRM:Experience:Count +
    MRM:BodyType + MRM:Count:CountGroup + MRM:CountGroup + #MRM:Sex
    MRM:Experience +
    I(MRM * Weight / (Height - 100)) +
    I((MRM - 100) / MRM) + poly(MRM / Weight, 2)
  ,
  data
)
summary(md)
Show(predict(md, data))


md = step(md, direction = "both")
summary(md)
Show(predict(md, data))

anova(md)



md = b5
obj = ggplot(data %>% select(-Count)) + theme_bw()

pr = predict(md, data, interval = "prediction", level = 0.95)
obj +
  #geom_ribbon(aes(x=MRM,ymin = pr[,2], ymax = pr[,3]), fill = "grey70") +
  geom_line(aes(x = MRM, y = pr[, 1]),
            size = 1,
            col = "grey70",
            linetype = "dotted") +
  geom_point(aes(x = MRM, y = pr[, 1]), size = 3) +
  geom_point(aes(
    x = MRM,
    y = RM,
    col = BodyType,
    shape = Action
  ), size = 4) +
  facet_grid(vars(CountGroup), vars(Action))#+
theme(legend.position = c(0.85, 0.25))


#Рассмотрение остатков####
d2 = data %>% mutate(res = RM - predict(md, data))

ob = ggplot(d2, aes(y = res))

ob + geom_boxplot(aes(x = Action))
ob + geom_boxplot(aes(x = BodyType))
ob + geom_point(aes(
  x = allrows,
  shape = BodyType,
  col = factor(ifelse(abs(res) > 2, "red", "green"))
), size = 4)











































#Многоповторный через многоповторный####

mrm = function(RM, count) {
  s = function(MRM)
    abs(RM - f(
      MRM = MRM,
      Count = count,
      Action = 'Присед'
    )[1]) %>% return()
  
  optim(
    par = c(0.8 * RM),
    fn = s,
    lower = 0.7 * RM,
    upper = RM,
    method = "Brent"
  )$par %>% return()
}



Rprof(tmp <- tempfile())
mrm(150, 3)
Rprof(NULL)
summaryRprof(tmp)



mrm2 = function(RM,
                count,
                Action = 'Присед',
                Weight = 70,
                Height = 170) {
  s = function(MRM)
    RM - f(MRM = MRM,
           Count = count,
           Action = Action)[1] %>% return()
  uniroot(s, c(0.5, 0.99) * RM)$root %>% return()
}
Rprof(tmp <- tempfile())
mrm2(150, 3)
Rprof(NULL)
summaryRprof(tmp)



mrm3 = function(RM,
                count,
                Action = 'Присед',
                Weight = 70,
                Height = 170) {
  ctg = 3
  if (count < 7) {
    ctg = 2
  }
  if (count < 4) {
    ctg = 1
  }
  
  act = 0
  if (Action == "Тяга") {
    act = cf[5]
  } else if (Action == "Присед") {
    act = cf[6]
  }
  
  
  polyroot(c(-RM, cf[1 + ctg] + count * cf[6 + ctg] + act, 0, 0, 0, 0, cf[1] *
               ((0.01 * Height) ^ 2 / Weight) ^ 6))[1] %>% Re()
  
}
Rprof(tmp <- tempfile())
mrm3(150, 3)
Rprof(NULL)
summaryRprof(tmp)


t = Sys.time()

vec = seq(100, 300, length.out = 81)

m = matrix(nrow = length(vec), ncol = 10)
m[, 1] = vec
for (i in 2:10) {
  for (j in 1:length(vec))
    m[j, i] = mrm3(m[j, 1], i)
}

colnames(m) = paste(1:10, 'reps') %>% as.character()

m %>% round(2) %>% tbl_df()


Sys.time() - t




save(f, count.levels, action.levels, cf, mrm3, file = "Functions.rdata")

save(f, count.levels, action.levels, cf, mrm3, b5, n14,file = "./RMbyMRMestimating/entire_data.rdata")


v = sapply(2:10, function(x)
  mrm3(100, x))
v







#Модель Мориса вообще не подходит####

# https://power-fitness.ru/metod-morisa-i-rajdina-ili-kak-uznat-svoj-maksimum-v-zhime-lezha.html

lm(log(RM / MRM) ~ Action2 + Count:Action2, data) %>% summary()


rlt = data$MRM * (
  0.969611 * exp(0.030583 * data$Count) * ifelse(data$Action == "Жим", 1, 0) +
    0.985993 * exp(0.015050 * data$Count) * ifelse(data$Action != "Жим", 1, 0)
)

Show(rlt)





data %<>% filter(Count < 11)

sq = c(1, 1.0475, 1.13, 1.1575, 1.2, 1.242, 1.284, 1.326, 1.368, 1.41)
pr = c(1, 1.035, 1.08, 1.115, 1.15, 1.18, 1.22, 1.255, 1.29, 1.325)
lf = c(1, 1.065, 1.13, 1.147, 1.164, 1.181, 1.198, 1.232, 1.236, 1.24)


rlt = data$MRM * (
  sq[data$Count] * ifelse(ddt$Action == "Жим", 1, 0) +
    pr[data$Count] * ifelse(ddt$Action == "Присед", 1, 0) +
    lf[data$Count] * ifelse(ddt$Action == "Тяга", 1, 0)
)

Show(rlt)


b3 = lm(RM ~ MRM:Count:CountGroup + MRM:Action - 1, data)
b3 %>% all()


cf = coefficients(b3)

#надо бы исследовать эти числа
count.vec = 2:10 * c(rep(cf[4], 2), rep(cf[5], 3), rep(cf[6], 4))


tibble(
  'Присед' = c(1, cf[3] + count.vec),
  'Жим' =  c(1, cf[1] + count.vec),
  'Тяга' =  c(1, cf[2] + count.vec)
)





















#Нелинейные модели####

vc=sapply(c(4,8,11,21,50), 
          function(p) nls(RM~MRM*(1+Count*coef),data=data.backup %>% 
                            filter(Count<p),start = list(coef=1/30)) %>% coef())

names(vc)=c(paste(rep("not more",4),levels(data$CountGroup)[1:4]) ,"all")



ShowSummary=function(model){
  model %>% summary() %>% print()
  
  Show(model %>% predict(data))
}
n1=nls(RM~MRM*(1+Count*coef),data=data.backup %>% filter(Count<4),start = list(coef=1/30))

ShowSummary(n1)


n2=nls(RM~MRM*a/(b-Count),data,start = list(a=36,b=37))
ShowSummary(n2)


n3=nls(RM~100*MRM/(a+b*exp(-c*Count)),data,start = list(a=52,b=42,c=0.055))
ShowSummary(n3)


n4=nls(RM~MRM*Count^a,data,start = list(a=0.1))
ShowSummary(n4)



n5 = nls(
  RM ~ (MRM^d)*(coef1)/(coef2-Count+c*Count^2),
  data = data,
  start = list(
    coef1 = 50,
    coef2=40,d=1,
    c=0
  )
)
ShowSummary(n5)

n6 = nls(
  RM ~ MRM ^ vk * (s + coef * Count^kk) ,
  data = data,
  start = list(coef = 0.0333, vk = 1., kk = 1.,s=1)
)
ShowSummary(n6)



n7 = nls(
  RM ~ (MRM^d)*(coef1[Action])/(coef2[CountGroup]-Count+c*Count^2),
  data = data,
  start = list(
    coef1 = rep(50,3),
    coef2=rep(40,3),
    d=1,
    c=0
  )
)


n8 = nls(
  RM ~ MRM ^ vk[CountGroup] * (s[Action] + coef * sqrt(Count)) ,
  data = data,
  start = list(coef = 0.0333, vk = rep(1,3),s=rep(1,3))
)


n9=nls(RM~MRM*(
  a[BodyType]/(b[Action]-Count)),data,
  start = list(a=rep(36,3),b=rep(37,3)))

n10=nls(RM~MRM*(
  a[BodyType]/(b[Action]-Count))+d*(MRM/Index)^6,data,
  start = list(a=rep(36,3),b=rep(37,3),d=0))


n11=nls(RM~MRM*(
  a[Action]/(b[CountGroup]-Count)),data,
  start = list(a=rep(36,3),b=rep(37,3)))

n12=nls(RM~MRM*(
  a[Action]/(b[CountGroup]-Count))+d*(MRM/Index)^6,data,
  start = list(a=rep(36,3),b=rep(37,3),d=0))


n13=nls(RM~MRM*(Count^a[CountGroup])+b[Action]*MRM*Count^2,data,start = list(a=rep(0.1,3),b=rep(0.1,3)))

n14=nls(RM~100*MRM/(a[Action]+b*exp(-c[CountGroup]*Count))+d*(MRM/Index)^6,data,start = list(a=rep(52,3),b=42,c=rep(0.0555,3),d=0))




# just cv for nls
cv.my = function(df = data,
                 fit,
                 start,
                 k = 10,
                 repets = 1) {
  sm = 0
  RM = df$RM
  
  for (r in seq(repets)) {
    blocks = sample.int(k, nrow(df), replace = T)
    
    
    val = future.apply::future_sapply(seq(k), function(i) {
      ft = nls(
        formula = fit$call$formula,
        data = df[blocks != i,],
        start = start
      )
      sum((RM[blocks == i] - predict(ft, df[blocks == i,])) ^ 2) %>% return()
    }) %>% sum()
    
    sm = sm + val / k
    
  }
  
  return(sm / repets)
}

#cv for nls including errors sample
cv.my2 = function(df = data,
                  fit,
                  start,
                  k = 10,
                  repets = 1) {
  sm = 0
  r = 0
  RM = df$RM
  
  repeat {
    b = T
    while (b) {
      val = tryCatch({
        b = F
        
        blocks = sample.int(k, nrow(df), replace = T)
        
        future.apply::future_sapply(seq(k), function(i) {
          ft = nls(
            formula = fit$call$formula,
            data = df[blocks != i,],
            start = start
          )
          sum((RM[blocks == i] - predict(ft, df[blocks == i,])) ^ 2) %>% return()
        }) %>% sum()
        
      },
      error = function(cond) {
       # print(cond)
        b = T
      })
    }
    
    
    
    sm = sm + val / k
    r = r + 1
    
    if (r == repets) {
      break
    }
    
  }
  
  return(sm / repets)
}


#cv for lm or nls including errors
cv.my3 = function(df = data,
                  fit,
                  start=NULL,
                  k = 10,
                  repets = 1) {
  sm = 0
  r = 0
  RM = df$RM
  
  repeat {
    b = T
    while (b) {
      val = tryCatch({
        b = F
        
        blocks = sample.int(k, nrow(df), replace = T)
        
        if(is.null(start)){
          
          future.apply::future_sapply(seq(k), function(i) {
            ft = lm(
              formula = fit$call$formula,
              data = df[blocks != i,]
            )
            sum((RM[blocks == i] - predict(ft, df[blocks == i,])) ^ 2) %>% return()
          }) %>% sum()
          
        }else{
          
          future.apply::future_sapply(seq(k), function(i) {
          ft = nls(
            formula = fit$call$formula,
            data = df[blocks != i,],
            start = start
          )
          sum((RM[blocks == i] - predict(ft, df[blocks == i,])) ^ 2) %>% return()
        }) %>% sum()        
        }

        
      },
      error = function(cond) {
        b = T
      })
    }
    
    
    
    sm = sm + val / k
    r = r + 1
    
    if (r == repets) {
      break
    }
    
  }
  
  return(sm / repets)
}



cv.my2(data, md, start, 10, 30)

cv.my3(data, lm(RM~MRM+MRM:Count,data), k= 10, repets=30)





start = list(s = 1,
             coef = 0.0333,
             t = 1,
             k = 0)
md = nls(RM ~ MRM * (s + coef * Count) ^ t + k * Weight / MRM,
         data = data,
         start = start)

system.time(cv.my2(data, md, start, 10, 30))

cv.my2(data %>% filter(Count<8), n14, list(a=rep(2,3),b=2,c=rep(-0.5,3),d=0), 10, 30)

Show2=function(model,df=data){
  fact=predict(model,df)
  Show(fact,df)
  
  #(
    ggplot(df %>% mutate(fact=fact,rnd=1:nrow(df)),
           aes(col=CountGroup,x=rnd))+
        geom_xspline(aes(y=fact),size=1,
                 spline_shape = -0.3,
                 linetype="dotdash",col="green",alpha=0.9)+
    geom_point(aes(y=RM,shape=BodyType),size=3)+
    geom_point(aes(y=fact,shape=BodyType),col="black",size=2)+
    facet_wrap(~CountGroup)+
    theme_bw()+theme(legend.position = "bottom")#) %>% ggplotly()
}

Show2(md)


#nonlinear####

kn = c(5, 6, 7, 8, 9, 10, 11, 12,13,14,15,16,17,18,19,20)

ct = c(8, 11)

gr = rep(c('2-10', '2-7'), length(kn)) %>% sort(decreasing = T)

m = matrix(nrow = length(kn) * length(ct), ncol = 14)

lst = list(n1,n2,n3,n4,n5,n6,n7,n8,n9,n10,n11,n12,n13,n14)
sts=list(
  list(coef=1/30),
  list(a=36,b=37),
  list(a=52,b=42,c=0.055),
  list(a=0.1),
  list(
    coef1 = 50,
    coef2=40,d=1,
    c=0
  ),
  list(coef = 0.0333, vk = 1., kk = 1.,s=1),
  list(
    coef1 = rep(50,3),
    coef2=rep(40,3),
    d=1,
    c=0
  ),
  list(coef = 0.0333, vk = rep(1,3),s=rep(1,3)),
  list(a=rep(36,3),b=rep(37,3)),
  list(a=rep(36,3),b=rep(37,3),d=0),
  list(a=rep(36,3),b=rep(37,3)),
  list(a=rep(36,3),b=rep(37,3),d=0),
  list(a=rep(0.1,3),b=rep(0.1,3)),
  list(a=rep(52,3),b=42,c=rep(0.0555,3),d=0)
)

for (i in 1:length(lst)) {
  model = lst[[i]]
  
  for (j in 1:length(ct)) {
    dt = data %>% filter(Count < ct[j])
    
    getval.mean = function(k, count) {
      cv.my2(dt, model, sts[[i]], k,count)
    }
    
    beg = (j - 1) * length(kn)
    
    for (s in 1:length(kn)) {
      m[beg + s, i] = getval.mean(kn[s], 30)
    }
    #print(m)
  }
  
}

colnames(m) = paste0('n', 1:length(lst))
kp = rep(kn, length(ct))

m[m<100]=NA

vals = data.frame(
  kp = rep(kp, length(lst)),
  b = as.numeric(m),
  gr = factor(rep(gr, length(lst))),
  n = factor(paste0('n',rep(1:14, length(kn) * length(ct)) %>% sort()))
) %>%
  tbl_df()



(ggplot(vals %>% filter(gr=="2-10"), aes(x = kp, y = b, col = n)) + theme_bw() +
  geom_point(size = 4) + geom_line(size = 1.) +
  labs(
    x = "Количество блоков при перекрёстной проверке",
    y = "Усреднённые значения ошибок после 30 повторных проверок",
    col = "Модель",
    title = "Оценки качества моделей при перекрёстной проверке"
  ) +
  scale_x_continuous(breaks = kn) +
  theme(legend.position = "right")) %>% ggplotly()



ggplot(vals, aes(x = kp, y = b, col = n)) + theme_bw() + facet_wrap(~gr) +
  geom_point(size = 4) + geom_line(size = 1.) +
  labs(
    x = "Количество блоков при перекрёстной проверке",
    y = "Усреднённые значения ошибок после 30 повторных проверок",
    col = "Модель",
    title = "Оценки качества моделей при перекрёстной проверке",
    subtitle = "Оценка производилась на разных подмножествах данных",
    caption = "Очевидно, что пятая модель превосходит остальные по точности"
  ) +
  scale_x_continuous(breaks = kn) +
  theme(legend.position = "bottom")



#linear + nonlinear####

kn = c(5, 6, 7, 8, 9, 10, 11, 12,13,14,15,16,17,18,19,20)

ct = c(8, 11)

gr = rep(c('2-10', '2-7'), length(kn)) %>% sort(decreasing = T)

m = matrix(nrow = length(kn) * length(ct), ncol = 4)

lst = list(n8,n14,b3,b5)
sts=list(
  list(coef = 0.0333, vk = rep(1,3),s=rep(1,3)),
  list(a=rep(52,3),b=42,c=rep(0.0555,3),d=0),
  NULL,
  NULL
)

for (i in 1:length(lst)) {
  model = lst[[i]]
  
  for (j in 1:length(ct)) {
    dt = data %>% filter(Count < ct[j])
    
    getval.mean = function(k, count) {
      cv.my3(dt, model, sts[[i]], k,count)
    }
    
    beg = (j - 1) * length(kn)
    
    for (s in 1:length(kn)) {
      m[beg + s, i] = getval.mean(kn[s], 30)
    }
    #print(m)
  }
  
}

colnames(m) = c("n8","n14","b3","b5")
kp = rep(kn, length(ct))

#m[m<100]=NA

vals = data.frame(
  kp = rep(kp, length(lst)),
  b = as.numeric(m),
  gr = factor(rep(gr, length(lst))),
  n = factor(
    c("n8" %>% rep(length(ct)*length(kn)),
      "n14"%>% rep(length(ct)*length(kn)),
      "b3"%>% rep(length(ct)*length(kn)),
      "b5"%>% rep(length(ct)*length(kn)))
    )
) %>%
  tbl_df()



(ggplot(vals %>% filter(gr=="2-10"), aes(x = kp, y = b, col = n)) + theme_bw() +
    geom_point(size = 4) + geom_line(size = 1.) +
    labs(
      x = "Количество блоков при перекрёстной проверке",
      y = "Усреднённые значения ошибок после 30 повторных проверок",
      col = "Модель",
      title = "Оценки качества моделей при перекрёстной проверке"
    ) +
    scale_x_continuous(breaks = kn) +
    theme(legend.position = "right")) %>% ggplotly()


















#бустинг####

rs=b5$residuals

GGally::ggcorr(data %>% mutate(rs=rs))

ggplot(data,aes(x=MRM,y=rs))+geom_point()

lm(rs~Index:I(Count)^2,data %>% mutate(rs=rs)) %>% summary()


#комитет####

ans.predict=function(data,models,coefs){
  #coefs=coefs/sum(abs(coefs))
  r=coefs[1]*predict(models[[1]],data)
  for(i in 2:length(coefs)){
    r=r+coefs[i]*predict(models[[i]],data)
  }
  return(r)
}

ans.predict(data,list(n14,b5),c(0.3,0.7)) %>% Show()




cv.my.ans = function(df = data,
                  fits,
                  coefs,
                  starts,
                  k = 10,
                  repets = 1) {
  sm = 0
  r = 0
  RM = df$RM
  ft=list()
  
  repeat {
    b = T
    while (b) {
      val = tryCatch({
        b = F
        
        blocks = sample.int(k, nrow(df), replace = T)
          
          future.apply::future_sapply(seq(k), function(i) {
            
            for(m in seq(coefs)){
              if(is.null(starts[[m]])){
              ft[[m]]=lm(
                formula = fits[[m]]$call$formula,
                data = df[blocks != i,]
              )
              }else{
                ft[[m]] = nls(
                  formula = fits[[m]]$call$formula,
                  data = df[blocks != i,],
                  start = starts[[m]]
                )
              }
            }
            
            sum((RM[blocks == i] - ans.predict( df[blocks == i,],ft,coefs)) ^ 2) %>% return()
          }) %>% sum()
        
      },
      error = function(cond) {
        b = T
      })
    }
    
    
    
    sm = sm + val / k
    r = r + 1
    
    if (r == repets) {
      break
    }
    
  }
  
  return(sm / repets)
}


cv.my.ans(data,list(n14,b5),c(0.2,0.8),list(list(a=rep(52,3),b=42,c=rep(0.0555,3),d=0),NULL),10,30)



mds=list(n14,b5)


opt=function(cfs){
  return(cv.my.ans(data,mds,cfs,list(list(a=rep(52,3),b=42,c=rep(0.0555,3),d=0),NULL),10,30))
}


optim(par=c(0.5,0.5), fn=opt, gr = NULL,
      method = c("Nelder-Mead"))





optimize(f=function(q)opt(c(q,1-q)),interval=c(0,1))


optim(par=c(0.5,0.5), fn=function(v){Error(data$RM,ans.predict(data,mds,v))}, gr = NULL,
      method = c("Nelder-Mead"))









#кластеризация по остаткам####

ggplot(data %>% mutate(errors=cut(b5$residuals,breaks = c(-15,-10,-5,5,10,15))),
       aes(x=MRM/Index,y=Index,col=errors,shape=Action))+geom_point(size=3)+
  facet_grid(vars(CountGroup))+
  theme_bw()

# try svm






