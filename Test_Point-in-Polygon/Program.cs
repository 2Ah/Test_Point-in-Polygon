using System;
namespace Test_Point_in_Polygon
{
    class Program
    {
        static void Main()
        {
            bool doNext = true;
            int i = 0;
            double x, y;
            Polygon polygon = new Polygon();
            Dot dot0 = new Dot();
            //*
            do
            {
                Console.WriteLine($"Ввод точки P{i} полигона: ");
                try
                {
                    Console.Write("x = "); x = Convert.ToDouble(Console.ReadLine());
                    Console.Write("y = "); y = Convert.ToDouble(Console.ReadLine());
                    polygon.DotAdd(new Dot(x, y));
                    i++;
                }
                catch (Exception e) { Console.WriteLine(e.Message); }
                if (!polygon.IsEnoughDots) continue;
                Console.Write($"Продолжить ввод точек? (нажмите <y> для согласия): ");
                if (Console.ReadLine() != "y") doNext = false;
            } while (doNext); //*/ 
            /* Тестовый полигон:
            polygon.DotAdd(new Dot(-2, -2));
            polygon.DotAdd(new Dot(2, 7));
            polygon.DotAdd(new Dot(6, 4));
            polygon.DotAdd(new Dot(2, 6));
            polygon.DotAdd(new Dot(5, 0));
            polygon.DotAdd(new Dot(5, 2));
            polygon.DotAdd(new Dot(4, 3));
            polygon.DotAdd(new Dot(6, 3));
            polygon.DotAdd(new Dot(6, -2));  //*/
            Console.WriteLine("Созданные точки полигона:");
            foreach (Dot p in polygon.Dots) { Console.WriteLine(p); }
            try
            {
                if (polygon.Build() == null) Console.WriteLine("Полигон не построен.");
                do
                {
                    doNext = false;
                    Console.WriteLine($"Ввод точки для проверки нахождения на полигоне: ");
                    try
                    {
                        Console.Write("x = "); x = Convert.ToDouble(Console.ReadLine());
                        Console.Write("y = "); y = Convert.ToDouble(Console.ReadLine());
                        dot0.Set(x, y);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        doNext = true;
                    }
                } while (doNext);
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

            //*
            bool? b = polygon.TestDotOnPolygon(dot0);
            if (b == true) Console.WriteLine("Точка лежит на полигоне.");
            else if (b == false) Console.WriteLine("Точка лежит вне полигона.");
            else Console.WriteLine("Полигон не построен.");
            Console.ReadKey();
        }
    }
}
