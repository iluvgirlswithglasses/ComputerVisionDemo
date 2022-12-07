
using ComputerVisionDemo.Convolution;
using ComputerVisionDemo.Tool;
using ComputerVisionDemo.Transform;
using Emgu.CV;
using Emgu.CV.Structure;

const string IMG_DIR = "D:\\r\\repos\\ComputerVisionDemo\\sample-images\\";

Image<Gray, byte> src = new(IMG_DIR + "src.private.jpg");
Image<Gray, byte> des = SobelOperator.Apply(src, SobelOperator.X_KERNEL);

ImgTool<Gray, byte>.Forward(des, (y, x) => {
    if (des[y, x].Intensity < 100) des[y, x] = new Gray(0);
    return true;
});

CvInvoke.Imwrite(IMG_DIR + "affine-3.png", des);
