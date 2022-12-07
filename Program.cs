
using ComputerVisionDemo.Convolution;
using Emgu.CV;
using Emgu.CV.Structure;

const string IMG_DIR = "D:\\r\\repos\\ComputerVisionDemo\\sample-images\\";

Image<Gray, byte> src = new(IMG_DIR + "src.jpg");
Image<Gray, byte> des = SobelOperator.Apply(src, SobelOperator.X_KERNEL);
CvInvoke.Imwrite(IMG_DIR + "sobel.png", des);
