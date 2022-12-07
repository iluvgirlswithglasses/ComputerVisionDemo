
using ComputerVisionDemo.Convolution;
using ComputerVisionDemo.Tool;
using ComputerVisionDemo.Transform;
using Emgu.CV;
using Emgu.CV.Structure;

const string IMG_DIR = "D:\\r\\repos\\ComputerVisionDemo\\sample-images\\";

Image<Gray, byte> src = new(IMG_DIR + "restruct-0.png");
Image<Gray, byte> imx = SobelOperator.AbsApply(src, SobelOperator.X_KERNEL), 
                  imy = SobelOperator.AbsApply(src, SobelOperator.Y_KERNEL);

ImgTool<Gray, byte>.Forward(imx, (y, x) => {
    imx[y, x] = new Gray(Math.Max(imx[y, x].Intensity, imy[y, x].Intensity));
    if (imx[y, x].Intensity < 175) imx[y, x] = new Gray(0);
    return true;
});

CvInvoke.Imwrite(IMG_DIR + "restruct-1.png", imx);
