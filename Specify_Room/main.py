# 2019.12.11 최종 수정
# 외부모듈 : opencv, tkinter
# 내부모듈 : outer_wall, innter_wall, door

import os
from tkinter import *
from tkinter import ttk
import tkinter as tk
import cv2
from outer_wall import build_outer_wall
from inner_wall import build_inner_wall
from door import build_door
from door import saveroom
import requests
import json
import operator
import sys
import pathlib


black = (0,0,0)

class RoomInfo():
    '''
    집 구조 정보를 담기 위한 Class이고 5개의 변수를 가지고 있다.
    1.name은 집의 이름을 저장하기 위한 정보. str type 

    2.outer_points는 집의 외벽을 세우기 위한 정보. 
    (int, int) 형태의 tuple의 list (cm 단위)

    3.inner_points는 집의 내벽을 세우기 위한 정보 (cm, 단위)
    (int, int) 형태의 tuple의 list가 list안에 포함되어 있는 이중 리스트 
    
    4. door은 집의 문, 창문에 대한 정보를 저장한다. (cm 단위)
       {"point_1" = , "point_2"=, "height_1" = , "height_2" = } dictionary의 list
       point_1 : 평면도에서 창문의 한 좌표 (int, int) 형태의 tuple를 value로 갖는다. 
       point_2 : 평면도에서 창문의 한 좌표 (int, int) 형태의 tuple를 value로 갖는다.
       h1 : 창문의 아랫단과 바닥과의 거리 int type를 value로 갖는다.
       h2 : 창문의 윗단과 바닥과의 거리 int type를 value로 갖는다.
    
    5. ceiling 천장의 높이를 저장한다. float type (m 단위)

    6. web으로부터 입력받은 user_id
    '''    
    def __init__(self, ceiling = 2.5):
        self.name = None
        self.outer_points = []
        self.inner_points = []
        self.door = []
        self.ceiling = ceiling
        self.user_id = None
        
def newroom(user_id):
    '''
    main UI에서 new room 버튼을 누르면 실행된다.
    main UI를 종료하고, 새로운 방을 만들 때에 방의 이름과 천장의 높이를 입력받는다.
    Enter키를 누름과 동시에 종료된다.
    '''
    global main_ui
    global newinfo
    global roomname
    global ceil
    global sub_root
    # destory main UI
    main_ui.destroy()
    # initialize object for new room
    newinfo = RoomInfo()
    newinfo.user_id = user_id
    print("newroom : newinfo.user_id : {}".format(newinfo.user_id))

    # window for input(room name, ceiling) 
    sub_root = Tk()
    sub_root.title("New Room")
    sub_root.geometry("300x300")
    rname = Label(sub_root, text = "ROOM NAME")
    rname.pack()
    roomname = tk.Entry(sub_root, width = 20)
    roomname.pack()
    cl = Label(sub_root, text = "CEIL HEIGHT")
    cl.pack()
    ceil = tk.Entry(sub_root, width = 20)
    ceil.pack()
    suggestion = Label(sub_root, text = "위의 항목들을 작성하고 Enter를 눌러주세요")
    suggestion.pack()
    sub_root.bind('<Return>', step)
    sub_root.mainloop()
    
def step(event):
    '''
    newroom함수에서 실행되는 sub_root window에서 room name과 ceiling을 입력한 후에
    Enter를 선택하면 실행된다. 
    room name과 ceiling을 newinfo instance에 저장하고 이전의 sub_root window를 종료한다.
    방의 정보를 생성하기 위해 사용되는 put_roominfo를 호출한다. 
    '''
    global newinfo
    global sub_root
    newinfo.name = tk.Entry.get(roomname)
    newinfo.ceiling = tk.Entry.get(ceil)
    sub_root.destroy()
    put_roominfo()


def put_roominfo():
    '''
    Tkinter winodw의 버튼을 연결하여, 외벽을 세우는 build_outer_wall
    내벽을 세우는 build_inner_wall, 문과 창문을 만드는 build_door
    결과를 데이터베이스에 저장하는 save room을 호출한다.
    '''
    global newinfo
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

def loadroom(user_id):
    print("user_id : {}".format(user_id))
    """
    DB와 연동하여 가장 최근에 생성된 ROOM을 호출하고, Unity 프로그램을 실행한다.
    """
    # 유저가 만든 방의 정보를 호출
    URL = 'http://34.66.144.16:3000/model'
    headers = {
            'Content-Type': 'application/x-www-form-urlencoded',
            'Accept': 'application/json'
    }
    n_data = {
        "function" : "LoadModelList", 
        "user_id" : user_id
    }
    res = requests.post(URL, data=n_data, headers=headers)
    
    # 백엔드으로부터 받아온 모델들의 리스트
    room_model_info = json.loads(res.text)
    # print(room_model_info)

    roommodel_dict = {}
    for modelinfo in room_model_info:
        temp_model = modelinfo["model_id"]
        temp_roomname = modelinfo["roomname"]
        roommodel_dict[temp_model] = temp_roomname
    # print("roommodel_dict")
    # print(roommodel_dict)
    model_ids = list(roommodel_dict.keys())
    # print(model_ids)
    sorted_ids = sorted(model_ids, key = (lambda x: int(x.split("_")[1])), reverse=True)
    # print(sorted_ids)
    if len(sorted_ids) > 10:
        sorted_ids = sorted_ids[:10]
    elif len(sorted_ids) < 10:
        tempNone = ["None" for i in range(10-len(sorted_ids))]
        sorted_ids.extend(tempNone)
    roommodel_dict["None"] = "None"
    # print("sorted_ids : ")
    # print(sorted_ids)
    sorted_roomname= [roommodel_dict[sortid] for sortid in sorted_ids]
    # print(sorted_roomname)

    # 최근에 생성한 방 10개를 선택할 수 있다.
    # 해당하는 방 모델을 버튼을 선택할 수 있는 UI 창을 만든다.
    loadroom_ui = Tk()
    loadroom_ui.title("Load Room")
    loadroom_ui.geometry("300x300")
    btn1 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[0]], command=lambda:start_unity(user_id, sorted_ids[0]))
    btn1.pack()
    btn2 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[1]], command=lambda:start_unity(user_id, sorted_ids[1]))
    btn2.pack()
    btn3 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[2]], command=lambda:start_unity(user_id, sorted_ids[2]))
    btn3.pack()
    btn4 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[3]], command=lambda:start_unity(user_id, sorted_ids[3]))
    btn4.pack()
    btn5 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[4]], command=lambda:start_unity(user_id, sorted_ids[4]))
    btn5.pack()
    btn6 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[5]], command=lambda:start_unity(user_id, sorted_ids[5]))
    btn6.pack()
    btn7 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[6]], command=lambda:start_unity(user_id, sorted_ids[6]))
    btn7.pack()
    btn8 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[7]], command=lambda:start_unity(user_id, sorted_ids[7]))
    btn8.pack()
    btn9 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[8]], command=lambda:start_unity(user_id, sorted_ids[8]))
    btn9.pack()
    btn10 = ttk.Button(loadroom_ui, text=roommodel_dict[sorted_ids[9]], command=lambda:start_unity(user_id, sorted_ids[9]))
    btn10.pack()
    loadroom_ui.mainloop()    


def start_unity(user_id, model_id):
    '''
    user id와 선택된 방 모델의 정보를 가지고, 이를 temp.txt 파일을 만들어 저장하고
    unity program을 실행한다.
    '''
    if model_id == "None":
        print("None clicked")
    print("start unity with {}, {}".format(user_id, model_id))
    print("os.getcwd()")
    print(os.getcwd())
    now_path = os.getcwd()
    # temp_path = now_path.split("\\")[:-1]
    # print("temp path : {}".format(temp_path))
    # parent_path = "\\".join(temp_path)
    # temp_path = os.path.join(now_path, "temp")
    # if not os.path.isdir(temp_path):
    #     os.makedirs(temp_path)
    print_form = "{}\n"
    # print("pathlib.Path")
    # print("parent path : {}".format(parent_path))
    
    unity_intereal_path = os.path.join(now_path, "unity", "inteReal")
    print(unity_intereal_path)
    if os.path.isdir(unity_intereal_path):
        with open(os.path.join(unity_intereal_path,"temp.txt"), "w") as f:
            f.write(print_form.format(user_id))
            f.write(print_form.format(model_id))
    # unity_prog = os.path.join(now_path, "unity.exe")
    # if os.path.isfile(unity_prog):
    #     os.system(unity_prog)
    print("finish")

if __name__ == "__main__":
    '''
    main_ui를 실행한다.
    기존에 생성했던 방정보를 통해서 Unity Program을 실행하는 load room 기능과
    새롭게 공간을 구성하는 new room 두가지 기능을 가지고 있다.
    각각 loadroom, newroom 함수를 통해 구현되어 있으며, 버튼을 눌러서 실행할 수 있다.
    '''
    inp = sys.argv[1]
    # inp = "specifyroom://user1/"

    user_id = inp.split("://")[1].split("/")[0]
    print("sys.argv : {}\nuser_id(processed) : {}".format(inp, user_id))

    global main_ui
    main_ui = Tk()
    main_ui.title("Specify Room")
    main_ui.geometry("300x300")

    label = Label(main_ui, text = 'Specify Room')
    label.pack()

    load_button = ttk.Button(main_ui, text="load room", command=lambda:loadroom(user_id))
    load_button.pack()

    newroom_button = ttk.Button(main_ui, text="new room", command=lambda:newroom(user_id))
    newroom_button.pack()

    main_ui.mainloop()