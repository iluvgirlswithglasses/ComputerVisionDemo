using Emgu.CV;
using Emgu.CV.Structure;
using ComputerVisionDemo.Tool;

namespace ComputerVisionDemo.Convolution {

    public class SobelOperator {

        public static Image<Gray, byte> Apply(Image<Gray, byte> src, double[,] kernel) {
            Image<Gray, byte> res = new(src.Size);
            int k = kernel.GetLength(0) >> 1;
            ImgTool<Gray, byte>.Forward(src, k, k, src.Height - k, src.Width - k, (y, x) => {
                res[y, x] = new Gray(ConvolutionCalc.Calc(src, kernel, y, x));
                return true;
            });
            return res;
        }

        // negative cells are displayed as red
        // while positive cells are displayed as blue
        public static Image<Bgr, byte> VisualApply(Image<Gray, byte> src, double[,] kernel) {
            Image<Bgr, byte> res = new(src.Size);
            int k = kernel.GetLength(0) >> 1;
            ImgTool<Gray, byte>.Forward(src, k, k, src.Height - k, src.Width - k, (y, x) => {
                double w = ConvolutionCalc.Calc(src, kernel, y, x);
                if (w < 0)
                    res[y, x] = new Bgr(0, 0, -w);
                else
                    res[y, x] = new Bgr(w, 0, 0);
                return true;
            });
            return res;
        }
    }
}
