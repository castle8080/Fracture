
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;
using System.Diagnostics;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Fracture.Controllers;

namespace Fracture.Fractal
{
    public class MandelbrotGenerator
    {
        public byte[] Generate(FractalImageSettings input)
        {
            int width = input.PixelWidth;
            int height = input.PixelHeight;

            var f = MakeConverter(input);
            var color = MakeBasicColorizer();

            var start = DateTime.Now;
            var sw = new Stopwatch();

            using (Image<Rgba32> image = new Image<Rgba32>(width, height))
            {
                for (int wp = 0; wp < width; wp++)
                {
                    for (int hp = 0; hp < height; hp++)
                    {
                        var pos = f(Tuple.Create(wp, hp));
                        sw.Start();
                        image[wp, hp] = color(pos);
                        sw.Stop();
                    }
                }
                Console.WriteLine($"Mandelbrot run: {sw.ElapsedMilliseconds} ms.");
                Console.WriteLine($"Generated raw image: {(DateTime.Now - start).TotalSeconds}");
                var result = ToPng(image);
                Console.WriteLine($"Encoded as PNG: {(DateTime.Now - start).TotalSeconds}");
                return result;
            }
        }

        private Func<Tuple<double, double>, Rgba32> MakeBasicColorizer()
        {
            return (pixPos) =>
            {
                var x = 0.0;
                var y = 0.0;
                var maxIteration = 1000;
                var iteration = 0;

                while(true)
                {
                    var x2 = x * x;
                    var y2 = y * y;
                    if (iteration >= maxIteration)
                    {
                        return Rgba32.Black; 
                    }
                    else if (x2 + y2 >= 4)
                    {
                        return Rgba32.White;
                    }
                    var xtemp = x2 - y2 + pixPos.Item1;
                    y = 2 * x * y + pixPos.Item2;
                    x = xtemp;
                    iteration++;
                }
            };
        }

        private Func<Tuple<int, int>, Tuple<double, double>> MakeConverter(FractalImageSettings input)
        {
            var tx = CoordinateTransformer.CreatePixelTransformer(input.PixelWidth, input.PixelHeight, input.OriginX, input.OriginY, input.LogicalWidth);
            return (pixPos) => tx.TransformToLocal(pixPos.Item1, pixPos.Item2);
        }

        private byte[] ToPng(Image<Rgba32> image)
        {
            var mem = new MemoryStream();
            image.SaveAsPng(mem);
            return mem.ToArray();
        }
    }
}
