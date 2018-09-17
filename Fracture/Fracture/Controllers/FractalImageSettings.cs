using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fracture.Controllers
{
    public class FractalImageSettings
    {
        public string Fractal { get; set; }

        public int PixelWidth { get; set; }

        public int PixelHeight { get; set; }

        public double OriginX { get; set; }

        public double OriginY { get; set; }

        public double LogicalWidth { get; set; }

        public FractalImageSettings()
        {
            Fractal = "mandelbrot";
            PixelWidth = 400;
            PixelHeight = 400;
            OriginX = -0.75;
            OriginY = 0;
            LogicalWidth = 3.0;
        }

        public override string ToString()
        {
            return $"Fractal={Fractal}, PixelWidth={PixelWidth}, PixelHeight={PixelHeight}, OriginX={OriginX}, OriginY={OriginY} LogicalWidth={LogicalWidth}";
        }
    }
}
