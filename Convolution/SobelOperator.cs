using Emgu.CV;
using Emgu.CV.Structure;
using ComputerVisionDemo.Tool;

namespace ComputerVisionDemo.Convolution {

    public class SobelOperator {

        public static readonly double[,] X_KERNEL = {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

        public static readonly double[,] Y_KERNEL = {
            { -1, -2, -1 },
            {  0,  0,  0 },
            { +1, +2, +1 }
        };

        public static Image<Gray, byte> Apply(Image<Gray, byte> src, double[,] kernel) {
            Image<Gray, byte> res = new(src.Size);
            Forward(src, kernel, (y, x, w) => {
                res[y, x] = new Gray(w);
                return true;
            });
            return res;
        }

        public static Image<Gray, byte> AbsApply(Image<Gray, byte> src, double[,] kernel) { 
            Image<Gray, byte> res = new(src.Size);
            Forward(src, kernel, (y, x, w) => {
                res[y, x] = new Gray(Math.Abs(w));
                return true;
            });
            return res;
        }

        // negative cells are displayed as red
        // while positive cells are displayed as blue
        public static Image<Bgr, byte> VisualApply(Image<Gray, byte> src, double[,] kernel) {
            Image<Bgr, byte> res = new(src.Size);
            Forward(src, kernel, (y, x, w) => {
                if (w < 0)
                    res[y, x] = new Bgr(-w, 0, -w);
                else
                    res[y, x] = new Bgr(w, w, 0);
                return true;
            });
            return res;
        }

        private static void Forward(Image<Gray, byte> src, double[,] kernel, Func<int, int, double, bool> f) {
            int k = kernel.GetLength(0) >> 1;
            ImgTool<Gray, byte>.Forward(src, k, k, src.Height - k, src.Width - k, (y, x) => {
                return f(y, x, ConvolutionCalc.Calc(src, kernel, y, x));
            });
        }
    }
}
