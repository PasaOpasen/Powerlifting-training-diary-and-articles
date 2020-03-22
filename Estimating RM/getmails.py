
import re

ls=[]

dt=open("data.tsv","r")
rt=open("mails.txt","w")

for line in dt:
    s=re.search(r"\t[^\s]*\@.*\.[^\s]*[\s$]",line)
    if s!=None:
        st=s.group(0)[1:-1]
        rt.write(st+" ")
        ls.append(st)



dt.close()
rt.close()

print(ls)