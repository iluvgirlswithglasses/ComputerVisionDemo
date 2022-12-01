using ComputerVisionDemo.Tool;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ComputerVisionDemo.Convolution {

    public class ConvolutionCalc {

        // asser that kernel height and width are equal, and are odds
        static public double Calc(Image<Gray, byte> src, double[,] kernel, int y, int x) {
            double res = 0;
            int k = kernel.GetLength(0)>>1;
            for (int i = 0; i < kernel.GetLength(0); i++)
                for (int j = 0; j < kernel.GetLength(1); j++)
                    res += kernel[i, j] * src[y + i - k, x + j - k].Intensity;
            return res;
        }
    }
}
