using System;

namespace GeometryExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
    public abstract class GeometryFigure
    {
        public int PointX;
        public virtual int Area() => 0; 
    }
    public class Square : GeometryFigure
    {
        public int PointY;

        public override int Area()
        {
            var piece = GetPiece();
            return piece * piece;
        }
        public int GetPiece() => Math.Abs(PointX - PointY);
    }
    public class Triangle : Square
    {
        public int PointZ;

        // S = √p(p - a)(p - b)(p - c) =
        public virtual new double Area()
        {
            var p = Perimeter();
            return Math.Sqrt(p * (p - PointX) * (p - PointY) * (p - PointZ));
        }
        // p = (a + b + c) / 2 
        public double Perimeter() => (PointX + PointY + PointZ) / 2;
    }
    public class Ellipse : Square
    {
        // S = π a b = π · 2 · 3 = 6 π (м)2 ≈ 18.84 (м)2
        public new double Area() => Math.PI * PointX * PointY;
    }
    public class Rectange : Triangle
    {
        public new int Area() => GetFirstPiece() * GetSecondPiece();
        public int GetFirstPiece() => Math.Abs(PointX - PointY);
        public int GetSecondPiece() => Math.Abs(PointX - PointZ);
    }
}
