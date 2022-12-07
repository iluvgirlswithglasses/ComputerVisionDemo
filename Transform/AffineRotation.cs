using Emgu.CV;
using Emgu.CV.Structure;
using static System.Math;

namespace ComputerVisionDemo.Transform {

    public class AffineRotation<TColor, TDepth> where TColor : struct, IColor where TDepth : new() {

        static public Image<TColor, TDepth> Create(Image<TColor, TDepth> src, double rad, TColor background) {
            rad = -rad;
            int h = src.Height, w = src.Width;
            Image<TColor, TDepth> res = new(
                (int)Ceiling(w * Abs(Sin(rad)) + h * Abs(Cos(rad))),    // w
                (int)Ceiling(w * Abs(Cos(rad)) + h * Abs(Sin(rad)))     // h
            );

            int srcy = h >> 1, srcx = w >> 1;
            int resy = res.Height >> 1, resx = res.Width >> 1;
            //
            for (int y = 0; y < res.Height; y++) {
                for (int x = 0; x < res.Width; x++) {
                    // given P', calculate P
                    int py = y - resy, px = x - resx;
                    int dy = srcy + (int)Round(-px * Sin(rad) + py * Cos(rad)),
                        dx = srcx + (int)Round(+px * Cos(rad) + py * Sin(rad));

                    if (0 <= dy && dy < h && 0 <= dx && dx < w)
                        res[y, x] = src[dy, dx];
                    else
                        res[y, x] = background;
                }
            }
            //
            return res;
        }
    }
}
