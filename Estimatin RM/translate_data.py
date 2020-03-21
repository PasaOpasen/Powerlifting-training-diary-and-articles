# -*- coding: utf-8 -*-

word_pairs = [
    ('Присед','Squat'),
    ('Жим','Bench press'),
    ('Тяга','Deadlift'),
    ('Мужчина','Male'),
    ('Женщина','Female'),
    ('До двух лет','less than 2 years'),
    ('2-3 года','2-3 years'),
    ('4-5 лет','4-5 years'),
    ('6-10 лет','6-10 years'),
    ('11-15 лет','11-15 years'),
    ('больше 15 лет','more than 15 years'),
    ('Эктоморф','Ectomorph'),
    ('Мезоморф','Mesomorph'),
    ('Эндоморф','Endomorph')
]

dt=open("data.tsv","r")
dt_ru=open("data(rus).tsv","w")
dt_en=open("data(eng).tsv","w")

for line in dt:
    ru=line
    en=line
    for rus,eng in word_pairs:
        ru=ru.replace(eng,rus)
        en=en.replace(rus,eng)
    dt_ru.write(ru)
    dt_en.write(en)



dt.close()
dt_ru.close()
dt_en.close()

