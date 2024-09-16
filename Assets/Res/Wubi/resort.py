import os

with open("inputMethodCode.csv","r",encoding='utf-8') as f:
    alllines = f.readlines()
    for i in range(0,10):
        for line in alllines:
            if(len(line.split(',')[1]) == i):
                print(line)
        