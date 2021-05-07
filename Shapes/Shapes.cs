using System;

namespace Shapes
{
    public abstract class Shape<T>
    {
        public abstract T GetArea();
    }

    public class Triangle : Shape<double>
    {
        public readonly double SideA;
        public readonly double SideB;
        public readonly double SideC;

        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                throw new NotValidParametersException("Side cannot be <= 0");

            if (a + b < c || a + c < b || c + b < a)
                throw new NotValidParametersException("The sum of two sides cannot be less then third side");
            
            SideA = a;
            SideB = b;
            SideC = c;
        }

        public override double GetArea()
        {
            var p = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(p * (p - SideA) * (p - SideB) * (p - SideC));
        }

        public bool IsRightAngled(double epsilon = 0.1)
        {
            return
                SideA.IsEqual(Math.Sqrt(Math.Pow(SideB, 2) + Math.Pow(SideC, 2)), epsilon) ||
                SideB.IsEqual(Math.Sqrt(Math.Pow(SideC, 2) + Math.Pow(SideA, 2)), epsilon) ||
                SideC.IsEqual(Math.Sqrt(Math.Pow(SideA, 2) + Math.Pow(SideB, 2)), epsilon);
        }
    }

    public class Circle : Shape<double>
    {
        public readonly double Radius;

        public Circle(double radius)
        {
            if (radius <= 0)
                throw new NotValidParametersException("Radius cannot be less or equal to 0");
            
            Radius = radius;
        }

        public override double GetArea() => Radius * Radius * Math.PI;
    }

    public class NotValidParametersException : Exception
    {
        public NotValidParametersException(string message) : base("Not valid parameters: " + message)
        {
            // if we want add logging later for example, we can do it there
        }
    }

    public static class Extensions
    {
        public static bool IsEqual(this double number, double comparable, double epsilon = 0.1) => 
            Math.Abs(number - comparable) <= epsilon;
    }
    
    /* Задание с SQL
     *
     * Так как у каждой категории может быть множество товаров и наоборот
     * у каждого товара может быть множество категорий
     * то это связь Многие-ко-многим, реализуемая таблицей связи
     * 
     * Итого, имеем следующие таблицы:
     * 
     * Categories: 
     * categoryId, categoryName, ...other fields...
     *
     * Products: 
     * productId, productName, ...other fields...
     *
     * CP:
     * categoryId, productId
     *
     * Итого, SQL запрос может выглядеть предположительно так:
     * 
     * SELECT DISTINCT p.productName, c.categoryName
     * FROM Products as p
     * JOIN CP as cp ON p.productId = cp.productId
     * JOIN Categories as c ON cp.categoryId = c.categoryId
     * UNION
     * SELECT p.productName, "Don't have category"
     * FROM Products as p
     * LEFT JOIN CP as cp
     * ON p.productId = cp.productId
     * WHERE cp.productId IS NULL
     * 
     */
}