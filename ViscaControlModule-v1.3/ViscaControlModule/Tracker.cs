using System;

using OpenCvSharp;

namespace ViscaControlModule
{
    class Tracker
    {
        public CvRect BoundingBox;
        private const int Hist_Threshold = 2;

        private double Adap_Threshold_value = 50;
        public IplImage dstImage;

        private IplImage imghistogram_X = new IplImage(Cv.Size(640, 500), BitDepth.U8, 3);
        private IplImage imghistogram_Y = new IplImage(Cv.Size(400, 480), BitDepth.U8, 3);
        private IplImage imgHistogram = new IplImage(Cv.Size(640, 480), BitDepth.U8, 1);

        private const int interval = 2;
        public CvPoint histoCenter;
        const int alphaThreshValue = 50;

        private CvPoint Center;
        private const int DetectionThresholdWidth = 80;
        private const int DetectionThresholdHeight = 120;
        public bool isTrackSuccess = false;

        private CvRect tmpRect;

        private struct histPoint
        {
            public CvPoint point;
            public int maxValue;


        }

        public void AdaptiveDifferentialImage(IplImage pre_Image, IplImage current_Image)
        {

            dstImage = Cv.CreateImage(Cv.GetSize(pre_Image), pre_Image.Depth, 1);

            int[] Histogram_x = new int[dstImage.Width];
            int[] Histogram_y = new int[dstImage.Height];

            imghistogram_X.SetZero();
            imghistogram_Y.SetZero();

            dstImage.SetZero();
            imgHistogram.SetZero();

            for (int i = 0; i < dstImage.Height; i++)
            {
                Histogram_y.SetValue(0, i);
            }
            for (int i = 0; i < dstImage.Width; i++)
            {
                Histogram_x.SetValue(0, i);
            }

            int[][] histogram = new int[dstImage.Height][];
            for (int i = 0; i < dstImage.Height; i++)
            {
                histogram[i] = new int[dstImage.Width];
                for (int j = 0; j < dstImage.Width; j++)
                {
                    histogram[i][j] = 0;
                }
            }


            int temp, accumulation = 0;
            unsafe
            {
                for (int i = 0; i < dstImage.Height; i++)
                {
                    for (int j = 0; j < dstImage.Width; j++)
                    {
                        temp = Math.Abs((int)pre_Image.ImageDataPtr[i * pre_Image.WidthStep + j]
                            - (int)current_Image.ImageDataPtr[i * current_Image.WidthStep + j]);
                        //Math.Abs((int)pre_Image[i, j] - (int)current_Image[i, j]);
                        accumulation += temp;
                    }
                }
            }
            Adap_Threshold_value = (double)accumulation / (double)(dstImage.Height * dstImage.Width);

            unsafe
            {
                for (int i = 0; i < dstImage.Height; i++)
                {
                    for (int j = 0; j < dstImage.Width; j++)
                    {
                        temp = Math.Abs((int)pre_Image.ImageDataPtr[i * pre_Image.WidthStep + j]
                            - (int)current_Image.ImageDataPtr[i * current_Image.WidthStep + j]);
                        //Math.Abs((int)pre_Image[i, j] - (int)current_Image[i, j]);

                        if (temp >= Adap_Threshold_value + alphaThreshValue)
                        {
                            //Cv.Set2D(dstImage, i, j, Cv.ScalarAll(temp));
                            Cv.Set2D(dstImage, i, j, Cv.ScalarAll(255));
                            Histogram_x[j]++;
                            Histogram_y[i]++;
                            histogram[i][j]++;
                        }
                        else
                            Cv.Set2D(dstImage, i, j, Cv.ScalarAll(0));

                    }
                }
            }


            for (int i = 0; i < dstImage.Height; i++)
            {
                for (int j = 0; j < dstImage.Width; j++)
                {
                    temp = histogram[i][j];
                    Cv.Set2D(imgHistogram, i, j, Cv.ScalarAll(temp * 100));
                }
            }
            /// <라벨링>

            // Histogram의 최고점 구하기

            histPoint histPointX, histPointY;

            histPointX.maxValue = Histogram_x[0];
            histPointX.point = new CvPoint(0, 0);
            for (int i = 1; i < dstImage.Width; i++)
            {
                temp = Histogram_x[i];
                if (histPointX.maxValue <= temp)
                {
                    histPointX.maxValue = temp;
                    histPointX.point.X = i;
                    histPointX.point.Y = imghistogram_X.Height - temp * interval;
                }
                Cv.Line(imghistogram_X, Cv.Point(i, imghistogram_X.Height), Cv.Point(i, imghistogram_X.Height - temp * interval), Cv.RGB(0, 255, 0));
            }
            Cv.Circle(imghistogram_X, histPointX.point, 1, Cv.RGB(255, 0, 0), 8);



            histPointY.maxValue = Histogram_y[0];
            histPointY.point = new CvPoint(0, 0);
            for (int i = 1; i < dstImage.Height; i++)
            {
                temp = Histogram_y[i];
                if (histPointY.maxValue <= temp)
                {
                    histPointY.maxValue = temp;
                    histPointY.point.X = temp * interval;
                    histPointY.point.Y = i;
                }
                Cv.Line(imghistogram_Y, Cv.Point(0, i), Cv.Point(temp * interval, i), Cv.RGB(0, 255, 0));

            }
            Cv.Circle(imghistogram_Y, histPointY.point, 1, Cv.RGB(255, 0, 0), 8);

            histoCenter.X = histPointX.point.X;
            histoCenter.Y = histPointY.point.Y;

            // 최고점을 기준으로 영역 검색하기 

            // X histogram에 대한 영역 검사 
            bool histFlag = false;
            int width = 0, height = 0;
            for (int i = histoCenter.X; i < dstImage.Width - 1; i++) // 오른쪽편  검사
            {

                //if ( Math.Abs( Histogram_x[i + 1] - Histogram_x[i]) >= Hist_Threshold && histFlag == false)
                if (Histogram_x[i] <= Hist_Threshold && histFlag == false)
                {
                    histFlag = true;
                }
                else
                {
                    width++;
                }

                if (histFlag)
                    break;

            }
            histFlag = false;
            for (int i = histoCenter.X; i > 1; i--) // 왼쪽 편 검사
            {
                //if ( Math.Abs(Histogram_x[i] -  Histogram_x[i - 1] )  >= Hist_Threshold && histFlag == false)
                if (Histogram_x[i] <= Hist_Threshold && histFlag == false)
                {
                    histFlag = true;
                }
                else
                {
                    histoCenter.X--;
                    width++;
                }
                if (histFlag)
                    break;

            }

            Cv.Line(imghistogram_X, Cv.Point(histoCenter.X, dstImage.Height - 2), Cv.Point(histoCenter.X + width, dstImage.Height - 2), Cv.RGB(0, 0, 255), 8);
            // Y histogram에 대한 영역 검사 
            histFlag = false;
            for (int i = histoCenter.Y; i < dstImage.Height - 1; i++)
            {
                if (Histogram_y[i] <= Hist_Threshold && histFlag == false)
                {
                    histFlag = true;
                }
                else
                    height++;

                if (histFlag)
                    break;

            }
            histFlag = false;
            for (int i = histoCenter.Y; i > 1; i--)
            {
                if (Histogram_y[i] <= Hist_Threshold && histFlag == false)
                {
                    histFlag = true;
                }
                else
                {
                    histoCenter.Y--;
                    height++;
                }
                if (histFlag)
                    break;
            }
            Cv.Line(imghistogram_Y, Cv.Point(3, histoCenter.Y), Cv.Point(3, histoCenter.Y + height), Cv.RGB(0, 0, 255), 8);

            BoundingBox.X = histoCenter.X;
            BoundingBox.Y = histoCenter.Y;
            BoundingBox.Width = width;
            BoundingBox.Height = height;

            if (width >= DetectionThresholdWidth && height >= DetectionThresholdHeight && histPointX.maxValue >= 30 && histPointY.maxValue >= 30)
            {
                Console.WriteLine("물체 검출 - 위치 (" + BoundingBox.X.ToString() + "," + BoundingBox.Y.ToString() + ")");

                isTrackSuccess = true;
                Cv.Rectangle(dstImage, BoundingBox, Cv.ScalarAll(255), 8);

            }
            else
                isTrackSuccess = false;

            //Cv.ShowImage("dstImage", dstImage);

            //Cv.ShowImage("X-Histogram", imghistogram_X);
            //Cv.ShowImage("Y-Histogram", imghistogram_Y);
            //Cv.ShowImage("Histogram", imgHistogram);



        }



        CvPoint FindMoment(IplImage dstImage, CvPoint min, CvPoint max)
        {
            double Sum_Oth = 0;
            double Sum_x = 0;
            double Sum_y = 0;
            CvPoint Center;
            unsafe
            {

                for (int i = min.Y; i < max.Y; i++)
                {
                    for (int j = min.X; j < max.X; j++)
                    {
                        Sum_Oth += dstImage.ImageDataPtr[i + dstImage.WidthStep + j];
                        Sum_x += j * dstImage.ImageDataPtr[i + dstImage.WidthStep + j];
                        Sum_y += i * dstImage.ImageDataPtr[i + dstImage.WidthStep + j];
                    }
                }

            }
            Center.X = (int)(Sum_x / Sum_Oth);
            Center.Y = (int)(Sum_y / Sum_Oth);

            return Center;
        }
        int Search_Window_Moment(IplImage dstImage, CvPoint min, CvPoint max)
        {

            double Sum_Oth = 0;
            double Sum_x = 0;
            //	double Sum_y =0;

            for (int i = min.Y; i < max.Y; i++)
            {
                for (int j = min.X; j < max.X; j++)
                {
                    Sum_Oth += dstImage[i, j];
                    Sum_x += j * dstImage[i, j];
                    //		Sum_y += i * data[i*dstImage->height + j];
                }
            }

            int Center = (int)(Sum_x / Sum_Oth);

            if (Center > 0) //&& Center < Dist_Range)
            {
                return Center;
            }
            else
            {

                return 0;
            }

        }

    }
}
