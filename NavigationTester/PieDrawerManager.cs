using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NavigationTester
{
    class PieDrawerManager
    {
        private Graphics mGraphic;
        private int mHeight, mWidth;
        private int MAX_PART_ANGLE = 36;
        private bool[] mAngles = new bool[36]; // 360/36 = 10, each 10 deg a range 
        Color bcolor;

        public PieDrawerManager(Graphics graphics, int width, int height, Color col)
        {
            mGraphic = graphics;
            mHeight = height;
            mWidth = width;
            bcolor = col;
            //clear();
        }
        public void clear()
        {
            for( int i=0 ; i< MAX_PART_ANGLE; i++)
                mAngles[i] = false;

            updateValue(-1);
        }
        public int getPersen()
        {
            float ok=0;
            for (int i = 0; i < MAX_PART_ANGLE; i++){
                if (mAngles[i]) ok++;
            }
            return (int)ok*100 / MAX_PART_ANGLE;
        }


        int pointerX = 0;
        int pointerY = 0;
        bool pointerDrawed = false;
        public void updateValue(float value)
        {
            int height = mHeight;
            int width = mWidth;
            int pen_size = 2;
            Pen myPen = new Pen(Color.Red, pen_size);//画笔 
            SolidBrush myBrush = new SolidBrush(System.Drawing.Color.Blue);//画刷
            Graphics formGraphics = mGraphic;

            int x0 = mWidth/2;
            int y0 = mHeight/2;
            width = (width - pen_size);
            height = height - pen_size;
            int raid = width > height ? height / 2 : width / 2;
            raid = raid*7/8;

            
            //空圆 background
            myPen.Color = Color.DarkCyan;
            myPen.Width = 4;
            //myBrush.Color = Color.Cyan;
            formGraphics.DrawEllipse(myPen, new Rectangle(0, 0, width, height));




            //draw persen
            if (value >= 360) value = 359;
            if (value >= 0)
            {
                //recovery the pointer 
                if (pointerDrawed)
                {
                    myPen.Width = 4;
                    myPen.Color = bcolor;
                    formGraphics.DrawLine(myPen, x0, y0, x0 + pointerX, y0 - pointerY);
                }


                //draw persen
                int index = (int)value / 10;
                mAngles[index] = true;
                for (index = 0; index < MAX_PART_ANGLE; index++)
                {
                    if (mAngles[index])
                    {
                        myBrush.Color = Color.Blue;
                    }
                    else
                    {
                        myBrush.Color = bcolor;
                    }
                    formGraphics.FillPie(myBrush, new Rectangle(pen_size / 2, pen_size / 2, width - pen_size, height - pen_size),  index * 10, -10);


                }

                //画指针
                myPen.Width = 2;
                myPen.Color = Color.Black;
                pointerX = (int)(raid * System.Math.Cos(Math.PI * (360-value) / 180));
                pointerY = (int)(raid * System.Math.Sin(Math.PI * (360-value) / 180));
                formGraphics.DrawLine(myPen, x0, y0, x0 + pointerX, y0 - pointerY);
                pointerDrawed = true;
            }
            else
            {
                //call clear
                pointerDrawed = false;
            }

            //画中间0度线
            myPen.Width = 2;
            myPen.Color = Color.Red;
            formGraphics.DrawLine(myPen, x0, y0, width, y0);


            myPen.Dispose();
            myBrush.Dispose();;


            
            /*
            if (rem == 0)
            {
                //formGraphics.DrawEllipse(myPen, new Rectangle(pen_size / 2, pen_size / 2, width - pen_size, height - pen_size));//空心圆
                // formGraphics.DrawPie(myPen, 0, 0, width / 2, height / 2, 0, 90);
                formGraphics.FillPie(myBrush, new Rectangle(pen_size / 2, pen_size / 2, width - pen_size, height - pen_size), -1 * value, -1 * (value+1));
                //formGraphics.DrawPie(myPen, new Rectangle(pen_size / 2, pen_size / 2, width - pen_size, height - pen_size), 0, -90);
                //formGraphics.DrawLine(myPen, 0, 0, height / 2, width / 2);//画线 
                rem = 1;
                
                formGraphics.FillEllipse(myBrush, new Rectangle(0, 0, 100, 200));//画实心椭圆  
                formGraphics.DrawEllipse(myPen, new Rectangle(0,0,100,200));//空心圆 
                formGraphics.FillRectangle(myBrush, new Rectangle(0,0,100,200));//画实心方  
                formGraphics.DrawRectangle(myPen, new Rectangle(0,0,100,200));//空心矩形  
                formGraphics.DrawLine(myPen, 0, 0, 200, 200);//画线  
                formGraphics.DrawPie(myPen,90,80,140,40,120,100); //画馅饼图形 //画多边形  
                formGraphics.DrawPolygon(myPen,new Point[]{ new Point(30,140), new Point(270,250), new Point(110,240), new Point (200,170), new Point(70,350), new Point(50,200)}); //清理使用的资源 
                 
            }
            else
            {
                //formGraphics.DrawLine(myPen, height / 2, width / 2, height, width);//画线 
                rem = 0;
            }
             * */
            
        }
    }
}
