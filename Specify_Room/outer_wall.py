# 이 코드는 ROOMINFO의 외벽을 만들기 위한 코드이다.
import numpy as np
import cv2
import copy
import json
import copy
black = (0,0,0)
def build_outer_wall(newinfo):
    
    print("build outer wall")
    # 기존에 만든 점들 또는 초기화된 점 가져오기
    temp_points = copy.deepcopy(newinfo.outer_points)
    # 외벽을 만드는 window를 만들고, outerpoint를 반환한다.
    pd = PolygonDrawer("BUILD OUTER WALL", 10, newinfo.outer_points)
    newinfo.outer_points = pd.run()



class PolygonDrawer(object):
    '''
    격자 위에 마우스 클릭을 이용하여, 점을 찍는다. 
    점들이 연결되어, 집의 외벽을 형성한다.
    이 과정이 다각형을 만드는 과정와 유사하다. 점을 이은 선분이 외벽에 해당한다.
    '''
    def __init__(self, window_name, size, outerpoints):
        self.window_name = window_name
        self.outerpoints = outerpoints
        self.size = size
        self.current = None
        self.done = False # Flag signalling we're done
        self.current = (0, 0) # Current position, so we can draw the line-in-progress

    def on_mouse(self, event, x, y, buttons, user_param):
        '''
        callback함수로 마우스 움직임에 따라 작동한다.
        1. 커서의 현재 위치를 포착 
        2. 왼쪽 마우스 클릭을 통해 외벽을 생성
        3. 오른쪽 마우스 클릭을 통해 가장 최근에 생성한 point를 제거한다. 
        '''

        if self.done:
            # self.done은 ESC버튼이 누르면 True값이 되고, 이때는 프로그램을 종료한다.
            return
        if event == cv2.EVENT_MOUSEMOVE:
            # 마우스의 현재의 위치를 self.current에 저장한다.
            self.current = (x, y)

        elif event == cv2.EVENT_LBUTTONDOWN:
            # 마우스의 왼쪽 버튼을 누르면 현재 마우스 커서의 위치에 점을 찍는다.
            if len(self.outerpoints) > 0:
                x_before = self.outerpoints[-1][0]
                y_before = self.outerpoints[-1][1]
                if x < x_before + 10 and x > x_before - 10:
                    self.outerpoints.append((x_before, y))
                elif y < y_before + 10 and y > y_before - 10:
                    self.outerpoints.append((x, y_before))
                else:
                    self.outerpoints.append((x, y))
            else:
                self.outerpoints.append((x,y))

        elif event == cv2.EVENT_RBUTTONDOWN:
            # 마우스의 오른쪽 버튼을 누르면 마지막으로 생성한 점을 제거한다.
            if len(self.outerpoints) > 0:
                del self.outerpoints[-1]


    def run(self):
        # 마우스를 이용하여 격자를 만들고 외벽을 생성하기 위한 창을 만든다.
        CANVAS_SIZE = ((self.size+3)*100, (self.size+3) * 100)
        cv2.imshow(self.window_name, np.zeros(CANVAS_SIZE, np.uint8))
        cv2.waitKey(1)
        # 마우스 콜백 실행.
        cv2.setMouseCallback(self.window_name, self.on_mouse)
        canvas = np.ones(CANVAS_SIZE, np.uint8)*255
        # 한 단위가 1m에 해당하는 격자를 생성한다.
        for i in range(1,self.size + 1):
            cv2.line(canvas, (i*100,0), (i*100, self.size * 100), black)
            cv2.line(canvas, (0, i*100), (self.size * 100, i*100), black)
        lattice_board = copy.deepcopy(canvas)

        # ESC 버튼을 누르기 전까지 작동하는 window를 생성한다.
        # 마우스를 통해서 받은 입력을 통해 생성된 벽을 display한다.
        while(not self.done):
            canvas = copy.deepcopy(lattice_board)
            # 현재 마우스 커서 위치를 출력.
            cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
            if (len(self.outerpoints) > 0):
                # 점들을 연결하여 외벽을 display
                cv2.polylines(canvas, np.array([self.outerpoints]), False, black, 2)
                # 현재 마우스 커서의 위치와 마지막에 입력한 점을 잇는 선분을 그린다.
                cv2.line(canvas, self.outerpoints[-1], self.current, black, 2)
                
                # 현재 마우스 커서의 위치와 마지막에 입력한 점의 거리를 계산하고, display한다.
                temp1 = float(self.outerpoints[-1][0]-self.current[0])
                temp2 = float(self.outerpoints[-1][1]-self.current[1])
                x = np.array([temp1, temp2])
                dist = round(np.linalg.norm(x),1)
                cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "length : {}".format(dist), (1000, 100) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "dif : ({}, {})".format(abs(temp1), abs(temp2)), (1000,200) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                
                # 외벽을 구성하는 점의 위치와 번호를 display한다.
                for i in range(len(self.outerpoints)):
                    cv2.putText(canvas, "{} : {}".format(i+1, self.outerpoints[i]), self.outerpoints[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                
            # 생성한 외벽을 display한다.
            cv2.imshow(self.window_name, canvas)
            # 50ms 마다 입력을 가져온다.
            inp = cv2.waitKey(50)
            # ESC키가 입력되면 loop를 종료한다.
            if inp == 27: # ESC hit
                self.done = True
        
        # 입력을 마친 후에 확인을 위해 최종적으로 생성한 외벽을 display한다.
        canvas = copy.deepcopy(lattice_board)
        
        # 외벽과 외벽을 구성하는 점들의 번호와 좌표를 출력한다.
        if (len(self.outerpoints) > 0):
            self.outerpoints.append(self.outerpoints[0])
            for i in range(len(self.outerpoints)-1):
                cv2.putText(canvas, "{} : {}".format(i+1, self.outerpoints[i]), self.outerpoints[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, black, 2)
            cv2.polylines(canvas, np.array([self.outerpoints]), False, black, 3)
        # display
        cv2.imshow(self.window_name, canvas)
        
        # 마지막으로 확인을 위해 최종 입력을 받는다.
        cv2.waitKey()
        # 창을 종료한다.
        cv2.destroyWindow(self.window_name)
        return self.outerpoints