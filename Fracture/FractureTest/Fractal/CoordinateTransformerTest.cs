using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Fracture.Fractal
{
    public class CoordinateTransformerTest
    {

        [Fact]
        public void TransformTest()
        {
            var tx = CoordinateTransformer.CreatePixelTransformer(400, 200, 0, 0, 2);

            TestPointTransform(tx, 200.0, 100.0, 0.0, 0.0);
            TestPointTransform(tx, 0.0, 0.0, -1.0, 0.5);
            TestPointTransform(tx, 400.0, 200.0, 1.0, -0.5);
        }

        private void TestPointTransform(CoordinateTransformer tx, double externalX, double externalY, double localX, double localY)
        {
            var result = tx.TransformToLocal(externalX, externalY);
            Assert.Equal(Tuple.Create(localX, localY), result);

            result = tx.TransformToExternal(localX, localY);
            Assert.Equal(Tuple.Create(externalX, externalY), result);
        }
    }
}
