
namespace ComputerVisionDemo.Tool {

    public class MatTool<T> where T : new() {

        /** @ the core */
        static public void Forward(T[,] src, Func<int, int, bool> f) {
            Forward(src, 0, 0, src.GetLength(0), src.GetLength(1), f);
        }

        static public void Forward(T[,] src, int t, int l, int d, int r, Func<int, int, bool> f) {
            t = Max(0, t);
            l = Max(0, l);
            d = Min(src.GetLength(0), d);
            r = Min(src.GetLength(1), r);
            for (int y = t; y < d; y++)
                for (int x = l; x < r; x++)
                    f(y, x);
        }

        /** @ i don't want to include System.Math */
        static private int Min(int a, int b) {
            return a < b ? a : b;
        }

        static private int Max(int a, int b) {
            return a > b ? a : b;
        }
    }
}
