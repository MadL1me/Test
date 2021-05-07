using NUnit.Framework;
using Shapes;

namespace Tests
{
    public class Tests
    {
        [TestCase(0,1,2)]
        [TestCase(100,1,2)]
        [TestCase(-3,5,7)]
        [TestCase(1,1,1)]
        [TestCase(3,120,123)]
        public void TriangleInvalidParamsTest(double a, double b, double c)
        {
            try
            {
                new Triangle(a, b, c);
            }
            catch (NotValidParametersException)
            {
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
            
            Assert.Pass();
        }

        [TestCase(3,4,5, true)]
        [TestCase(10,5,8.66, true)]
        [TestCase(1,2,3, false)]
        public void TriangleIsRightAngleTest(double a, double b, double c, bool isRightAngled)
        {
            var triangle = new Triangle(a, b, c);
            Assert.IsTrue(triangle.IsRightAngled() == isRightAngled);
        } 

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(123)]
        public void CircleInvalidParamsTest(double radius)
        {
            try
            {
                new Circle(radius);
            }
            catch (NotValidParametersException)
            {
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
            
            Assert.Pass();
        }

        [TestCase(1,2,2,0.9682)]
        [TestCase(1,3,3,1.479019)]
        [TestCase(8,10,5,19.81003)]
        public void TriangleGetAreaTest(double a, double b, double c, double area)
        {
            var triangle = new Triangle(a, b, c);
            Assert.IsTrue(area.IsEqual(triangle.GetArea()));
        }
        
        [TestCase(1, 3.1415)]
        [TestCase(2, 12.5663)]
        [TestCase(10, 314.159)]
        public void CircleGetAreaTest(double radius, double area)
        {
            var circle = new Circle(radius);
            Assert.IsTrue(area.IsEqual(circle.GetArea()));
        }
    }
}