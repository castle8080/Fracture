using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fracture.Fractal
{
    public class CoordinateTransformer
    {
        public double ExternalCenterX { get; set; }
        public double ExternalCenterY { get; set; }

        public double LocalCenterX { get; set; }
        public double LocalCenterY { get; set; }

        public double XRatio { get; set; }
        public double YRatio { get; set; }

        public Tuple<double, double> TransformToLocal(double x, double y)
        {
            return TransformToLocal(Tuple.Create(x, y));
        }

        public Tuple<double, double> TransformToLocal(Tuple<double, double> point)
        {
            var localX = (point.Item1 - ExternalCenterX) * XRatio + LocalCenterX;
            var localY = (point.Item2 - ExternalCenterY) * YRatio + LocalCenterY;
            return Tuple.Create(localX, localY);
        }

        public Tuple<double, double> TransformToExternal(double x, double y)
        {
            return TransformToExternal(Tuple.Create(x, y));
        }

        public Tuple<double, double> TransformToExternal(Tuple<double, double> point)
        {
            var externalX = (point.Item1 - LocalCenterX) / XRatio + ExternalCenterX;
            var externalY = (point.Item2 - LocalCenterY) / YRatio + ExternalCenterY;
            return Tuple.Create(externalX, externalY);
        }

        public static CoordinateTransformer CreatePixelTransformer(
            int pixWidth,
            int pixHeight,
            double logicalXOrigin,
            double logicalYOrigin,
            double logicalXWidth)
        {
            return new CoordinateTransformer
            {
                ExternalCenterX = (double)pixWidth / 2.0,
                ExternalCenterY = (double)pixHeight / 2.0,
                XRatio = logicalXWidth / pixWidth,
                YRatio = -(logicalXWidth / pixWidth),
                LocalCenterX = logicalXOrigin,
                LocalCenterY = logicalYOrigin
            };
        }
    }
}
