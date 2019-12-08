# 2019.12.01 작성
# 이 코드는 ROOMINFO의 내벽을 만들기 위한 코드이다.

import numpy as np
import cv2
import copy
import json
import copy
import math
from tkinter import *
from tkinter import ttk
import tkinter as tk
from outer_wall import build_outer_wall
from inner_wall import build_inner_wall
import requests
import os
black = (0,0,0)
def build_door(room_input, newinfo):
    # global newinfo
    # newinfo = newinfo
    room_input.destroy()
    print("build door")
    pd = PolygonDrawer(newinfo.name, 10, newinfo.outer_points, newinfo.inner_points, newinfo.door, newinfo.ceiling)
    door = pd.run()
    pt_roominfo(newinfo)
    # print("here {} ".format(newinfo.door))
    return

def saveroom(newinfo):
    op_save = []
    ip_save = []
    door_save = []

    std_x = newinfo.outer_points[0][0]
    std_y = newinfo.outer_points[0][1]

    for outerpoint in newinfo.outer_points:
        temp_x = outerpoint[0]-std_x
        temp_y = outerpoint[1]-std_y
        op_save.append((temp_x, temp_y))
    
    for innerpoints in newinfo.inner_points:
        temp = []
        for innerpoint in innerpoints:
            temp_x = innerpoint[0]-std_x
            temp_y = innerpoint[1]-std_y
            temp.append((temp_x,temp_y))
        ip_save.append(temp)
    
    for door in newinfo.door:
        temp_1_x = door[0][0]-std_x
        temp_1_y = door[0][1]-std_y
        temp_2_x = door[1][0]-std_x
        temp_2_y = door[1][1]-std_y
        temp_dict = {}
        temp_dict["point_1"] = (temp_1_x, temp_1_y)
        temp_dict["point_2"] = (temp_2_x, temp_2_y)
        temp_dict["height_1"] = door[2]
        temp_dict["height_2"] = door[3]
        door_save.append(temp_dict)

    new_data = {}
    new_data["size"] = 1000
    new_data["outerpoints"] = op_save
    new_data["innerpoints"] = ip_save
    new_data["ceiling"] = float(newinfo.ceiling)
    new_data["roomname"] = newinfo.name
    new_data["door"] = door_save
    print("new_data : {}".format(new_data))
    
    URL = 'http://34.66.144.16:3000/model'
    headers = {
                'Content-Type': 'application/x-www-form-urlencoded',
                'Accept': 'application/json'
    }
    n_data = {
        "function" : "AddModel", 
        "user_id" : "user1",
        "roomname" : "myroom"
    }
    res = requests.post(URL, data=n_data, headers=headers)
    print(res.text)
    model_name = res.text

    now_path = os.getcwd()
    model_dir_path = os.path.join(now_path, "model")
    if not os.path.isdir(model_dir_path):
        os.makedirs(model_dir_path)
    
    model_path = os.path.join(model_dir_path, model_name)
    print(model_path)
    
    with open(model_path, 'w', encoding='utf-8') as f:
        json.dump(new_data, f)

    upload_URL = 'http://34.66.144.16:3000/upload'
    if os.path.isfile(model_path):
        print("there is file")
        files = {'file':open(model_path,'rb')}
        r = requests.post(upload_URL, files=files)
        print(r.text)






def pt_roominfo(newinfo):
    print("put_roominfo")
    room_input = Tk()
    room_input.title("Make Room")
    room_input.geometry("300x300")
    out_btn = ttk.Button(room_input, text="build outer wall", command=lambda:build_outer_wall(newinfo))
    out_btn.pack()
    in_btn = ttk.Button(room_input, text="build inner wall", command=lambda:build_inner_wall(newinfo))
    in_btn.pack()
    door_btn = ttk.Button(room_input, text="build door", command=lambda:build_door(room_input, newinfo))
    door_btn.pack() 
    save_btn = ttk.Button(room_input, text="save room", command=lambda:saveroom(newinfo))
    save_btn.pack()
    room_input.mainloop()


def door_input():
    global door_window
    global temp_info
    global d1, d2, h1, h2

    door_window = Tk()
    door_window.title("DOOR, WINDOW")
    door_window.geometry("300x300")
    
    point_1 = temp_info[0]
    point_2 = temp_info[1]
    indexes = temp_info[2]
    in_or_out = temp_info[3]
    ceiling = temp_info[4]
    
    d = math.sqrt((point_1[0]-point_2[0])*(point_1[0]-point_2[0]) 
    + (point_1[1]-point_2[1])*(point_1[1]-point_2[1]))


    if in_or_out:
        distance1 = Label(door_window, text = "Distance from i{}".format(indexes[0]))
        distance1.pack()
        d1 = tk.Entry(door_window, width = 20)
        d1.pack()
        distance2 = Label(door_window, text = "Distance from i{}".format(indexes[1]))
        distance2.pack()
        d2 = tk.Entry(door_window, width = 20)
        d2.pack()

    else:
        distance1 = Label(door_window, text = "Distance from {}".format(indexes[0]))
        distance1.pack()
        d1 = tk.Entry(door_window, width = 20)
        d1.pack()
        distance2 = Label(door_window, text = "Distance from {}".format(indexes[1]))
        distance2.pack()
        d2 = tk.Entry(door_window, width = 20)
        d2.pack()
    
    warning1 = Label(door_window, text = "sum of two distances should be lower than {}".format(int(d)))
    warning1.pack()
    height1 = Label(door_window, text = "distance between floor and bottom of window or door")
    height1.pack()
    h1 = tk.Entry(door_window, width = 20)
    h1.pack()
    height2 = Label(door_window, text = "distance between floor and top of window or door")
    height2.pack()
    h2 = tk.Entry(door_window, width = 20)
    h2.pack()
    warning2 = Label(door_window, text = "distances from floor should be lower thatn ceiling : {}".format(ceiling))
    warning2.pack()
    door_window.bind('<Down>', make_door)
    door_window.mainloop()
    print("return")




def make_door(event):
    # print("Enter event : {}".format(event))
    print("makedoor")
    global temp_door
    global door_window
    global d1,d2,h1,h2
    point_1 = temp_info[0]
    point_2 = temp_info[1]
    indexes = temp_info[2]
    in_or_out = temp_info[3]
    ceiling = float(temp_info[4])
    
    dis_1 = float(tk.Entry.get(d1))
    dis_2 = float(tk.Entry.get(d2))
    height_1 = float(tk.Entry.get(h1))
    height_2 = float(tk.Entry.get(h2))

    door_window.destroy()
    print("door_window destroy")

    # in_or out은 true이면 in, false이면 out이다.
    # d1은 point_1으로부터의 거리
    # d2는 point_2로부터의 거리
    # d1과 d2를 입력받은 후에 계산해서 좌표인 w1과 w2를 계산한다.
    # w1,w2,h1,h2를 반환
    # d1과 d2의 합이 point_1과 point_2와의 거리보다 작아야한다.
    # h1는 바닥으로부터 창문 아랫단과의 거리 (ceiling 보다 작아야 한다.)
    # h2는 바닥으로부터 창문 윗단과의 거리   (ceiling 보다 작아야 한다.)
    # h1과 h2는 ceiling(천장 높이)보다 작아야한다.
    # h1 < h2 인 부등식이 성립해야한다.
    # d는 point 1과 point 2 사이의 거리이다.
    

    d = math.sqrt((point_1[0]-point_2[0])*(point_1[0]-point_2[0]) + (point_1[1]-point_2[1])*(point_1[1]-point_2[1]))

    
    error = 0
    if d < dis_1 + dis_2:
        print("sum of two distances should be lower than {}".format(int(d)))
        
        error = 1
        
    elif height_1 > height_2:
        print("height1 should be lower than height 2")
        
        error = 2

    elif ceiling < height_1 or ceiling < height_2:
        print("height should be lower than ceiling {}".format(ceiling))
        error = 3

    if error > 0:
        print("error")
        warn = Tk()
        warn.title("WARNING")
        warn.geometry("300x300")
        if error == 1:
            warning1 = Label(warn, text = "sum of two distances should be lower than {}".format(int(d)))
        elif error == 2:
            warning1 = Label(warn, text = "height1 should be lower than height 2")
        elif error == 3:
            warning1 = Label(warn, text = "height should be lower than ceiling {}".format(ceiling))
        warning1.pack()
        warn.mainloop()
        door_input()
        error = 0
    # door_input(in_or_out)
    # d1, d2, h1, h2를 입력받는다.
    # d1, d2의 경우 d를 넘으면 안된다는 경고
    # h1, h2의 경우 ceiling을 넘으면 안된다는 경고

    # assert d >= d1 + d2, "창문이 폭이 0이상이어야 한다. d >= d1+d2"
    else:
        print("no error")
        unit_vect = (float(point_2[0]-point_1[0])/d, float(point_2[1]-point_1[1])/d)
        w1 = (int(point_1[0] + unit_vect[0]*dis_1), int(point_1[1] + unit_vect[1]*dis_1))
        w2 = (int(point_2[0] - unit_vect[0]*dis_2), int(point_2[1] - unit_vect[1]*dis_2))
        height_1 = int(float(height_1)*100)
        height_2 = int(float(height_2)*100)

        global door_sample
        door_sample = (w1,w2,height_1,height_2)
        print("door_sample : {}".format(door_sample))
        # door_sample = (111,112,113,114)
        # print(door_sample)
    
    

class PolygonDrawer(object):
    def __init__(self, name, size, outerpoints, innerpoints, doors, ceiling):
        self.name = name
        self.outerpoints = outerpoints
        self.innerpoints = innerpoints
        self.size = size
        self.done = False # Flag signalling we're done
        self.current = (0, 0) # Current position, so we can draw the line-in-progress
        self.temppoints = []
        self.doors = doors
        self.outer_dps = []
        self.ceiling = ceiling

    def on_mouse(self, event, x, y, buttons, user_param):
        # Mouse callback that gets called for every mouse event (i.e. moving, clicking, etc.)
        if self.done: # Nothing more to do
            return
        if event == cv2.EVENT_MOUSEMOVE:
            # We want to be able to draw the line-in-progress, so update current mouse position            
            self.current = (x, y)
            
        elif event == cv2.EVENT_LBUTTONDOWN:
            # Left click means adding a point at current position to the list of points
            global temp_info
            temp_info = None
            for i in range(len(self.outer_dps)):
                x_before = self.outer_dps[i][0]
                y_before = self.outer_dps[i][1]
                if x < x_before + 10 and x > x_before - 10 and y < y_before + 10 and y > y_before - 10:
                    if i == (len(self.outer_dps)-1):
                        temp_info = (self.outerpoints[i], self.outerpoints[i+1], (i+1,1), False, self.ceiling, self.name)
                        door_input()

                        # make_door(self.outerpoints[i], self.outerpoints[i+1], (i+1,1), False, self.ceiling)
                        # print("{} {} outer inside rectangle".format(i+1, 1))
                    else:
                        temp_info = (self.outerpoints[i], self.outerpoints[i+1], (i+1,i+2), False, self.ceiling, self.name)
                        door_input()
                        # make_door(self.outerpoints[i], self.outerpoints[i+1], (i+1,i+2), False, self.ceiling)
                        # print("{} {} outer inside rectangle".format(i+1, i+2))

            now = 1
            for innerpoint in self.innerpoints:
                for j in range(len(innerpoint)-1):
                    dp_x = int((innerpoint[j][0] + innerpoint[j+1][0])/2)
                    dp_y = int((innerpoint[j][1] + innerpoint[j+1][1])/2)
                    if x < dp_x + 10 and x > dp_x - 10 and y < dp_y + 10 and y > dp_y - 10:
                        # print("i{} i{} inner inside rectangle".format(now, now+1))
                        temp_info = (innerpoint[j], innerpoint[j+1], (now,now+1), True, self.ceiling, self.name)
                        door_input()
                        # make_door(innerpoint[j], innerpoint[j+1], (now,now+1), True, self.ceiling)
                    now+=1
                now += 1

        elif event == cv2.EVENT_RBUTTONDOWN:
            # Right click means delete one vertex
            if len(self.doors) > 0:
                del self.doors[-1]

        # elif event == tk.Event.
        

    def run(self):
        # Let's create our working window and set a mouse callback to handle events
        # cv2.namedWindow(self.name, flags=cv2.CV_WINDOW_AUTOSIZE)
        CANVAS_SIZE = ((self.size+4)*100, (self.size+4) * 100)

        cv2.imshow(self.name, np.zeros(CANVAS_SIZE, np.uint8))

        cv2.waitKey(1)
        cv2.setMouseCallback(self.name, self.on_mouse)
        canvas = np.ones(CANVAS_SIZE, np.uint8)*255
        # make lattice board
        for i in range(1,self.size + 1):
            cv2.line(canvas, (i*100,0), (i*100, self.size * 100), black)
            cv2.line(canvas, (0, i*100), (self.size * 100, i*100), black)
        cv2.polylines(canvas, np.array([self.outerpoints]), False, black, 2)

        cnt = 0
        for i in range(len(self.outerpoints)-1):
            outerpoint = self.outerpoints[i]
            cnt += 1
            cv2.putText(canvas, "{} : {}".format(cnt, outerpoint), outerpoint ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
        
        for i in range(len(self.outerpoints)-1):
            temp_x = int((self.outerpoints[i][0] + self.outerpoints[i+1][0])/2)
            temp_y = int((self.outerpoints[i][1] + self.outerpoints[i+1][1])/2)
            cv2.rectangle(canvas, (temp_x-10, temp_y-10), (temp_x+10, temp_y+10), black, 2)
            self.outer_dps.append((temp_x, temp_y))

        cnt = 0

        for innerpoint in self.innerpoints:
            if (len(innerpoint) > 0):
                cv2.polylines(canvas, np.array([innerpoint]), False, black, 2)
                for i in range(len(innerpoint)):
                    cnt += 1
                    cv2.putText(canvas, "i{} : {}".format(cnt, innerpoint[i]), innerpoint[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
        
        for innerpoint in self.innerpoints:
            for j in range(len(innerpoint)-1):
                temp_x = int((innerpoint[j][0] + innerpoint[j+1][0])/2)
                temp_y = int((innerpoint[j][1] + innerpoint[j+1][1])/2)
                cv2.rectangle(canvas, (temp_x-10, temp_y-10), (temp_x+10, temp_y+10), black, 2)
                # self.inner_dps.append((temp_x, temp_y))

        lattice_board = copy.deepcopy(canvas)

        global door_sample
        door_sample = None

        while(not self.done):
            if door_sample != None:
                print("new door : {}".format(door_sample))
                self.doors.append(door_sample)
                door_sample = None
                print(self.doors)
            canvas = copy.deepcopy(lattice_board)
            cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
            for i in range(len(self.doors)):
                door = self.doors[i]
                cv2.putText(canvas, "DOOR {}".format(i+1), (self.size*100, 100*(i+1)) ,cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0,0,0), 2)
                cv2.putText(canvas, "points  :{} {}".format(door[0], door[1]), (self.size*100, 100*(i+1) + 30) ,cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0,0,0), 2)
                cv2.putText(canvas, "heights :{} {}".format(door[2], door[3]), (self.size*100, 100*(i+1) + 60) ,cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0,0,0), 2)
                # print()

            # This is our drawing loop, we just continuously draw new images
            # and show them in the named window
            cnt = 0
            # Update the window
            cv2.imshow(self.name, canvas)
            # And wait 50ms before next iteration (this will pump window messages meanwhile)
            inp = cv2.waitKey(50)
            if inp == 27: # ESC hit
                self.done = True
        # # User finised entering the polygon points, so let's make the final drawing
        # canvas = copy.deepcopy(lattice_board)
        # cv2.imshow(self.name, canvas)
        # # Waiting for the user to press any key
        # cv2.waitKey()
        cv2.destroyWindow(self.name)
        # print("inner points : {}".format(self.innerpoints))
        return self.doors

if __name__ == "__main__":
    pd = PolygonDrawer("myroom", 10, [(100,100),(100,600),(800,600),(800,100),(100,100)], [[(100,500),(200,500),(200,600)],[(500,100),(500,300),(800,300)]], [], 3.0)
    door = pd.run()
    print("finish")
    print(door)