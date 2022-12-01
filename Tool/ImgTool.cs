using Emgu.CV;

namespace ComputerVisionDemo.Tool {

    public class ImgTool<TColor, TDepth> where TColor : struct, IColor where TDepth : new() {

        /** @ the core */
        static public void Forward(Image<TColor, TDepth> src, Func<int, int, bool> f) {
            Forward(src, 0, 0, src.Height, src.Width, f);
        }

        static public void Forward(Image<TColor, TDepth> src, int t, int l, int d, int r, Func<int, int, bool> f) {
            t = Math.Max(0, t);
            l = Math.Max(0, l);
            d = Math.Min(src.Height, d);
            r = Math.Min(src.Width, r);
            for (int y = t; y < d; y++)
                for (int x = l; x < r; x++)
                    f(y, x);
        }
    }
}
