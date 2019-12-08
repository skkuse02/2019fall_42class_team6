# 2019.12.01 작성
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
            # 현재 마우스 위치를 포착한다.
            self.current = (x, y)

        elif event == cv2.EVENT_LBUTTONDOWN:
            # 현재의 위치에서 내벽을 생성하기 위한 점을 생성한다.
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
            # 가장 마지막에 생성한 점을 제거한다.
            if len(self.temppoints) > 0:
                del self.temppoints[-1]
            else:
                if len(self.innerpoints) > 0:
                    self.temppoints =  self.innerpoints[-1]
                    del self.innerpoints[-1]
                    del self.temppoints[-1]
            
    def run(self):
        # Let's create our working window and set a mouse callback to handle events
        # cv2.namedWindow(self.window_name, flags=cv2.CV_WINDOW_AUTOSIZE)
        CANVAS_SIZE = ((self.size+3)*100, (self.size+3) * 100)

        cv2.imshow(self.window_name, np.zeros(CANVAS_SIZE, np.uint8))

        cv2.waitKey(1)
        cv2.setMouseCallback(self.window_name, self.on_mouse)
        canvas = np.ones(CANVAS_SIZE, np.uint8)*255
        # make lattice board
        for i in range(1,self.size + 1):
            cv2.line(canvas, (i*100,0), (i*100, self.size * 100), black)
            cv2.line(canvas, (0, i*100), (self.size * 100, i*100), black)
        cv2.polylines(canvas, np.array([self.outerpoints]), False, black, 2)
        for outerpoint in self.outerpoints:
            cv2.putText(canvas, "{}".format(outerpoint), outerpoint ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
        lattice_board = copy.deepcopy(canvas)



        while(not self.done):
            canvas = copy.deepcopy(lattice_board)
            cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
            # This is our drawing loop, we just continuously draw new images
            # and show them in the named window
            # canvas = np.zeros(CANVAS_SIZE, np.uint8)
            cnt = 0
            for innerpoint in self.innerpoints:
                if (len(innerpoint) > 0):
                    cv2.polylines(canvas, np.array([innerpoint]), False, black, 2)
                    for i in range(len(innerpoint)):
                        cnt += 1
                        cv2.putText(canvas, "i{} : {}".format(cnt, innerpoint[i]), innerpoint[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                        

            
            if (len(self.temppoints) > 0):
                # Draw all the current polygon segments
                cv2.polylines(canvas, np.array([self.temppoints]), False, black, 2)
                # And  also show what the current segment would look like
                cv2.line(canvas, self.temppoints[-1], self.current, black, 2)

                temp1 = float(self.temppoints[-1][0]-self.current[0])
                temp2 = float(self.temppoints[-1][1]-self.current[1])

                x = np.array([temp1, temp2])
                # dist = round(np.linalg.norm(x)/100,2)
                dist = round(np.linalg.norm(x),1)
                cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "length : {}".format(dist), (1000, 100) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "dif : ({}, {})".format(abs(temp1), abs(temp2)), (1000,200) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                for i in range(len(self.temppoints)):
                    cnt += 1
                    cv2.putText(canvas, "i{} : {}".format(cnt, self.temppoints[i]), self.temppoints[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
            else:
                cv2.polylines(canvas, np.array([self.temppoints]), False, black, 2)
                cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)

            # Update the window
            cv2.imshow(self.window_name, canvas)
            # And wait 50ms before next iteration (this will pump window messages meanwhile)
            inp = cv2.waitKey(50)
            if inp == 27: # ESC hit
                if not self.temppoints == []:
                    self.innerpoints.append(self.temppoints)
                self.done = True
            elif inp == 82 or inp == 114:
                print("type inp : {}".format(inp))
                if self.temppoints == []:
                    continue
                else:
                    self.innerpoints.append(self.temppoints)
                    self.temppoints = []

        # User finised entering the polygon points, so let's make the final drawing
        canvas = copy.deepcopy(lattice_board)

        # of a filled polygon
        cnt = 1
        if (len(self.innerpoints) > 0):
            for innerpoint in self.innerpoints:
                for i in range(len(innerpoint)):
                    cnt += 1
                    cv2.putText(canvas, "i{} : {}".format(cnt, innerpoint[i]), innerpoint[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, black, 2)
            # cv2.fillPoly(canvas, np.array([self.points]), FINAL_LINE_COLOR)
            for innerpoint in self.innerpoints:
                cv2.polylines(canvas, np.array([innerpoint]), False, black, 1)
        # And show it
        cv2.imshow(self.window_name, canvas)
        # Waiting for the user to press any key
        cv2.waitKey()

        cv2.destroyWindow(self.window_name)
        print("inner points : {}".format(self.innerpoints))
        return self.innerpoints