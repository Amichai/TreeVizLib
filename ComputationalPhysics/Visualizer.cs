using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalPhysics {
    public static class Visualizer {
        private static int r(double val) {
            return (int)Math.Round(val);
        }

        private static Color toColor(double val) {
            var rounded = r(val * 255);
            if (rounded < 0) {
                return Color.Black;
            }
            return Color.FromArgb(255, rounded, rounded / 2, 100);
        }

        private static double getScalingFactor(Func<double, double, double> generator, double x0, double xf, double dx, double y0, double yf, double dy) {
            double max = double.MinValue;
            for (double x = x0; x < xf; x += dx) {
                for (double y = y0; y < yf; y += dy) {
                    double eval = generator(x, y);
                    if (eval > max) {
                        max = eval;
                    }
                }
            }
            return max;
        }

        public static Bitmap Visualize(Func<double, double, double> generator, double x0, double xf, double dx, double y0, double yf, double dy) {
            var scalingFactor = getScalingFactor(generator, x0, xf, dx, y0, yf, dy);
            int width = (int)Math.Ceiling((xf - x0) / dx);
            int height = (int)Math.Ceiling((yf - y0) / dy);
            Bitmap toReturn = new Bitmap(width, height);
            for (double x = x0; x < xf; x += dx) {
                for (double y = y0; y < yf; y += dy) {
                    double eval = generator(x, y);
                    int xIdx = r((x - x0) / dx);
                    int yIdx = r((y - y0) / dy);
                    if (xIdx < width && yIdx < height) {
                        try {
                            toReturn.SetPixel(xIdx,
                                (height - 1) - yIdx,
                                toColor(eval / scalingFactor));
                        } catch {

                        }
                    }
                }
            }
            return toReturn;
        }

        public static Bitmap Visualize(Func<double, double, double> generator, double x0, double xf, double y0, double yf, Size size) {
            var xRange = xf - x0;
            var dx = xRange / size.Width;
            var yRange = yf - y0;
            var dy = yRange / size.Height;
            return Visualize(generator, x0, xf, dx, y0, yf, dy);
        }
    }
}
