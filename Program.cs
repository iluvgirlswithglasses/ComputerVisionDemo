
using ComputerVisionDemo.Convolution;
using ComputerVisionDemo.Tool;
using Emgu.CV;
using Emgu.CV.Structure;

const string IMG_DIR = "D:\\r\\repos\\ComputerVisionDemo\\sample-images\\";

Image<Gray, byte> src = new(IMG_DIR + "sobel-src-2.jpg");
Image<Gray, byte> des = SobelOperator.Apply(src, SobelOperator.Y_KERNEL);

ImgTool<Gray, byte>.Forward(des, (y, x) => {
    if (des[y, x].Intensity < 200) des[y, x] = new Gray(0);
    return true;
});

CvInvoke.Imwrite(IMG_DIR + "sobel-pattern.png", des);
