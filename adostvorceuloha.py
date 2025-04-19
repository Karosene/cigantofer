import random
import tkinter

c = tkinter.Canvas(width = 500 ,height = 500)
c.pack()

def stv():
    stvx = random.randint(100,300)
    stvy = random.randint(100,300)
    stvorec = c.create_rectangle(stvx, stvy, stvx + 100, stvy + 100)
    stvorec1 = c.create_rectangle(225,225,275,275, fill="red")
    a1, b1, a2, b2 = c.coords(stvorec)
    x1, y1, x2, y2 = c.coords(stvorec1)
    if x1 < a2 and x2 > a1 and y1 < b2 and y2 > b1:
        print("j")
    else:
        print("n")
stv()

c.mainloop()

# if a1 + a2 > x1 and y1 + y2 > b1 and x1 + x2 > a1 and b1 + b2 > y1
speky bucek
speky bucek
speky bucek
speky bucek