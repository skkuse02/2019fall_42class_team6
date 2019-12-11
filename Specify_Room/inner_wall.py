# 2019.12.11 최종 수정
# 이 코드는 방의 내벽을 만들기 위한 코드이다.
import numpy as np
import cv2
import copy
import json
import copy
black = (0,0,0)

def build_inner_wall(newinfo):
    # 방의 내벽을 만들고, 이를 newinfo.inner_points에 저장한다.
    # innerpoints는 이중 list 형식이다.
    # 한 list안에 입력된 점들끼리 순서대로 연결된다.
    pd = PolygonDrawer("BUILD INNER WALL", 10, newinfo.outer_points, newinfo.inner_points)
    newinfo.inner_points = pd.run()

class PolygonDrawer(object):
    def __init__(self, window_name, size, outerpoints, innerpoints):
        self.window_name = window_name
        self.outerpoints = outerpoints
        self.innerpoints = innerpoints
        self.size = size
        self.done = False # Flag we're done
        self.current = (0, 0) # Current position
        self.temppoints = []

    def on_mouse(self, event, x, y, buttons, user_param):
        '''
        callback함수로 마우스 움직임에 따라 작동한다.
        1. 커서의 현재 위치를 포착 
        2. 왼쪽 마우스 클릭을 통해 내벽을 생성
        3. 오른쪽 마우스 클릭을 통해 가장 최근에 생성한 point를 제거한다. 
        '''
        # ESC키를 누르면 True가 되고 해당 method를 종료한다.
        if self.done:
            return
        if event == cv2.EVENT_MOUSEMOVE:
            # 현재 마우스 커서 위치를 포착한다.
            self.current = (x, y)

        elif event == cv2.EVENT_LBUTTONDOWN:
            # 마우스의 왼쪽 버튼을 누르면 현재의 위치에서 내벽을 생성하기 위한 점을 생성한다.
            if len(self.temppoints) > 0:
                x_before = self.temppoints[-1][0]
                y_before = self.temppoints[-1][1]
                if x < x_before + 10 and x > x_before - 10:
                    self.temppoints.append((x_before, y))
                elif y < y_before + 10 and y > y_before - 10:
                    self.temppoints.append((x, y_before))
                else:
                    self.temppoints.append((x, y))
            else:
                self.temppoints.append((x, y))
        
        elif event == cv2.EVENT_RBUTTONDOWN:
            # 마우스의 오른쪽 버튼을 누르면 가장 마지막에 생성한 점을 제거한다.
            if len(self.temppoints) > 0:
                del self.temppoints[-1]
            else:
                if len(self.innerpoints) > 0:
                    self.temppoints =  self.innerpoints[-1]
                    del self.innerpoints[-1]
                    del self.temppoints[-1]
            
    def run(self):
        # 마우스를 이용하여 격자를 만들고 내벽을 생성하기 위한 창을 만든다.
        CANVAS_SIZE = ((self.size+3)*100, (self.size+3) * 100)

        cv2.imshow(self.window_name, np.zeros(CANVAS_SIZE, np.uint8))

        cv2.waitKey(1)
        cv2.setMouseCallback(self.window_name, self.on_mouse)
        canvas = np.ones(CANVAS_SIZE, np.uint8)*255
        # 한 단위가 1m에 해당하는 격자를 생성한다.
        for i in range(1,self.size + 1):
            cv2.line(canvas, (i*100,0), (i*100, self.size * 100), black)
            cv2.line(canvas, (0, i*100), (self.size * 100, i*100), black)
        # 미리 저장된 외벽의 정보를 display할 수 있게 canvas에 저장한다. 
        cv2.polylines(canvas, np.array([self.outerpoints]), False, black, 2)

        # 외벽을 구성하는 점을 위해 좌표 표시.
        for outerpoint in self.outerpoints:
            cv2.putText(canvas, "{}".format(outerpoint), outerpoint ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
        lattice_board = copy.deepcopy(canvas)

        # ESC 버튼을 누르기 전까지 작동하는 window를 생성한다.
        # 마우스를 통해서 받은 입력을 통해 생성된 벽을 display한다.
        while(not self.done):
            canvas = copy.deepcopy(lattice_board)
            cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
            # cnt는 내벽을 구성하는 점들의 번호를 매기기 위한 변수이다.
            cnt = 0

            # innerpoints는 이중 list이다. 내부 list에 포함된 점들끼리 순서대로 연결된다.
            # 연결이 완료된 점들을 연결하고 각 점의 좌표와 번호를 출력할 수 있게 canvas를 수정한다.
            for innerpoint in self.innerpoints:
                if (len(innerpoint) > 0):
                    cv2.polylines(canvas, np.array([innerpoint]), False, black, 2)
                    for i in range(len(innerpoint)):
                        cnt += 1
                        cv2.putText(canvas, "i{} : {}".format(cnt, innerpoint[i]), innerpoint[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                        

            # 연결이 완료되지 않고 현재 생성중인 내벽을 display한다.
            if (len(self.temppoints) > 0):
                # 점들간의 연결.
                cv2.polylines(canvas, np.array([self.temppoints]), False, black, 2)
                cv2.line(canvas, self.temppoints[-1], self.current, black, 2)
                
                # 현재 마우스 커서의 위치와 마지막 점간의 거리를 구한다.
                temp1 = float(self.temppoints[-1][0]-self.current[0])
                temp2 = float(self.temppoints[-1][1]-self.current[1])
                x = np.array([temp1, temp2])
                dist = round(np.linalg.norm(x),1)
                cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "length : {}".format(dist), (1000, 100) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "dif : ({}, {})".format(abs(temp1), abs(temp2)), (1000,200) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                
                for i in range(len(self.temppoints)):
                    cnt += 1
                    cv2.putText(canvas, "i{} : {}".format(cnt, self.temppoints[i]), self.temppoints[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
            
            # 위에서 출력한 canvas를 화면에 display한다.
            cv2.imshow(self.window_name, canvas)
            # And wait 50ms before next iteration (this will pump window messages meanwhile)
            # 50milisecond마다 입력을 받는다. 입력이 ESC인 경우 loop를 종료한다.
            inp = cv2.waitKey(50)
            if inp == 27: # ESC hit
                if not self.temppoints == []:
                    self.innerpoints.append(self.temppoints)
                self.done = True
            # r 키를 누르면 현재의 연결을 종료하고, 새로운 연결을 할 수 있게 한다.
            elif inp == 82 or inp == 114:
                print("type inp : {}".format(inp))
                if self.temppoints == []:
                    continue
                else:
                    self.innerpoints.append(self.temppoints)
                    self.temppoints = []

        # 위에서 작성한 정보를 다시 display한다.
        canvas = copy.deepcopy(lattice_board)
        cnt = 1
        if (len(self.innerpoints) > 0):
            for innerpoint in self.innerpoints:
                for i in range(len(innerpoint)):
                    cnt += 1
                    cv2.putText(canvas, "i{} : {}".format(cnt, innerpoint[i]), innerpoint[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, black, 2)
            for innerpoint in self.innerpoints:
                cv2.polylines(canvas, np.array([innerpoint]), False, black, 1)
        # 마지막으로 화면을 display한다.
        cv2.imshow(self.window_name, canvas)
        # 최종으로 마지막 입력을 받는다.
        cv2.waitKey()
        cv2.destroyWindow(self.window_name)
        print("inner points : {}".format(self.innerpoints))
        return self.innerpoints