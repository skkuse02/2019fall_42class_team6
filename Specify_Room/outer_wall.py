# 2019.12.01 작성
# 이 코드는 ROOMINFO의 외벽을 만들기 위한 코드이다.
import numpy as np
import cv2
import copy
import json
import copy
black = (0,0,0)
def build_outer_wall(newinfo):
    print("build outer wall")
    # print("before : {} ".format(newinfo.ceiling))
    # temp = copy.deepcopy(newinfo.ceiling)
    # newinfo.ceiling = 99999
    # print("before : {}".format(newinfo.ceiling))
    temp_points = copy.deepcopy(newinfo.outer_points)
    pd = PolygonDrawer("BUILD OUTER WALL", 10, newinfo.outer_points)
    outerpoints = pd.run()
    print("here : {} ".format(outerpoints))
    newinfo.outer_points = outerpoints



class PolygonDrawer(object):
    def __init__(self, window_name, size, outerpoints):
        self.window_name = window_name
        self.outerpoints = outerpoints
        self.size = size
        self.current = None
        self.done = False # Flag signalling we're done
        self.current = (0, 0) # Current position, so we can draw the line-in-progress
        print("first outerpoints : {}".format(self.outerpoints))

    def on_mouse(self, event, x, y, buttons, user_param):
        # Mouse callback that gets called for every mouse event (i.e. moving, clicking, etc.)
        if self.done: # Nothing more to do
            return
        if event == cv2.EVENT_MOUSEMOVE:
            # We want to be able to draw the line-in-progress, so update current mouse position            
            self.current = (x, y)


        elif event == cv2.EVENT_LBUTTONDOWN:
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
            # Right click means delete one vertex
            if len(self.outerpoints) > 0:
                del self.outerpoints[-1]
            # print("Completing polygon with %d points." % len(self.points))
            # self.done = True

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
        lattice_board = copy.deepcopy(canvas)
        # cv2.imshow(self.window_name, lattice_board)



        while(not self.done):
            # This is our drawing loop, we just continuously draw new images
            # and show them in the named window
            # canvas = np.zeros(CANVAS_SIZE, np.uint8)
            canvas = copy.deepcopy(lattice_board)
            cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
            if (len(self.outerpoints) > 0):
                # Draw all the current polygon segments
                cv2.polylines(canvas, np.array([self.outerpoints]), False, black, 2)
                # And  also show what the current segment would look like
                cv2.line(canvas, self.outerpoints[-1], self.current, black, 2)

                temp1 = float(self.outerpoints[-1][0]-self.current[0])
                temp2 = float(self.outerpoints[-1][1]-self.current[1])

                x = np.array([temp1, temp2])
                # dist = round(np.linalg.norm(x)/100,2)
                dist = round(np.linalg.norm(x),1)
                cv2.putText(canvas, "{}".format(self.current), self.current ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "length : {}".format(dist), (1000, 100) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                cv2.putText(canvas, "dif : ({}, {})".format(abs(temp1), abs(temp2)), (1000,200) ,cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                for i in range(len(self.outerpoints)):
                    cv2.putText(canvas, "{} : {}".format(i+1, self.outerpoints[i]), self.outerpoints[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)
                
            # Update the window
            cv2.imshow(self.window_name, canvas)
            # And wait 50ms before next iteration (this will pump window messages meanwhile)
            inp = cv2.waitKey(50)
            if inp == 27: # ESC hit
                self.done = True
                # self.outerpoints.append(self.outerpoints[0])
                # mydict = {}
                # mydict["point"] = self.outerpoints
                # with open("test.json", "w") as f:
                #     json.dump(mydict, f, indent='\t')
           
                



        # User finised entering the polygon points, so let's make the final drawing
        canvas = copy.deepcopy(lattice_board)
        # for i in range(len(self.points)):
        #     cv2.putText(canvas, "{} : {}".format(i, self.points[i]), self.points[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, (0,0,0), 2)


        # of a filled polygon
        if (len(self.outerpoints) > 0):
            self.outerpoints.append(self.outerpoints[0])
            for i in range(len(self.outerpoints)-1):
                cv2.putText(canvas, "{} : {}".format(i+1, self.outerpoints[i]), self.outerpoints[i], cv2.FONT_HERSHEY_SIMPLEX, 0.8, black, 2)
            # cv2.fillPoly(canvas, np.array([self.points]), FINAL_LINE_COLOR)
            cv2.polylines(canvas, np.array([self.outerpoints]), False, black, 3)
        # And show it
        cv2.imshow(self.window_name, canvas)
        # Waiting for the user to press any key
        cv2.waitKey()

        cv2.destroyWindow(self.window_name)
        return self.outerpoints

# if __name__ == "__main__":
#     pd = PolygonDrawer("Make_ROOM_DESIGN", 10)
#     image = pd.run()
#     # pd = MakeDoor(image)
#     # image = pd.run()
#     cv2.imwrite("polygon.png", image)
#     print("Polygon = %s" % pd.points)