
using ComputerVisionDemo.Convolution;
using Emgu.CV;
using Emgu.CV.Structure;

const string IMG_DIR = "D:\\r\\repos\\ComputerVisionDemo\\sample-images\\";

Image<Gray, byte> src = new(IMG_DIR + "sobel-src-2.jpg");
Image<Bgr, byte> des = SobelOperator.VisualApply(src, SobelOperator.X_KERNEL);
CvInvoke.Imwrite(IMG_DIR + "sobel-visual-2.png", des);
