
using ComputerVisionDemo.Convolution;
using ComputerVisionDemo.Skeletonization;
using ComputerVisionDemo.Tool;
using ComputerVisionDemo.Transform;
using Emgu.CV;
using Emgu.CV.Structure;

const string IMG_DIR = "D:\\r\\repos\\ComputerVisionDemo\\sample-images\\";

Image<Gray, byte> src = new(IMG_DIR + "restruct-2.png");
bool[,] ske = new bool[src.Height, src.Width];

MatTool<bool>.Forward(ske, (y, x) => {
    return ske[y, x] = src[y, x].Intensity > 200;
});

new Sket(ske).Apply();

Image<Gray, byte> des = new(src.Size);
MatTool<bool>.Forward(ske, (y, x) => {
    if (ske[y, x]) des[y, x] = new Gray(255);
    return true;
});

CvInvoke.Imwrite(IMG_DIR + "restruct-3.png", des);
