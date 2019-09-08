using System;
using System.Collections.Generic;
namespace Test_Point_in_Polygon
{
    public class PolygonException : Exception
    {
        public Object Sender { get; }
        public PolygonException(Object sender, string message) : base(message)
        {
            Sender = sender;
        }
    }
    public class Polygon
    {
        bool isEnoughDots = false; // Результат проверки на достаточное количество точек для постройки полигона (должно быть >= 3).
        bool isPolygonBuilt = false; // Результат проверки на существование границ полигона. 
        readonly List<Dot> dots = new List<Dot>(16); // Точки полигона.
        readonly List<Segment> borders = new List<Segment>(16); // Список отрезков - границ полигона.
        public List<Dot> Dots { get { return dots; } }
        public List<Segment> Borders { get { return borders; } }
        public bool IsEnoughDots { get { return isEnoughDots; } }
        public bool IsPolygonBuilt { get { return isPolygonBuilt; } }
        public bool? DotAdd(Dot dot)
        {
            int dotsCount = dots.Count;
            if (dotsCount > 0)
            {
                if (PrimitivesMath.DotsEqual(dot, dots[0])) throw new PolygonException(this, "Ошибка. Попытка создания точки полигона на месте уже созданной.");
                Segment segNew = new Segment(dots[dotsCount - 1], dot);
                Segment seg = new Segment();
                for (int i = 0; i < dotsCount - 1; i++)
                {
                    seg.Set(dots[i], dots[i + 1]);
                    if (PrimitivesMath.DotOnSegment(dot, seg))
                        throw new PolygonException(this, "Ошибка. Попытка создания точки полигона на уже созданной границе.");
                    if (PrimitivesMath.SegmentsCrossing(seg, segNew))
                        throw new PolygonException(this, "Ошибка. Попытка создания границы полигона образующей пересечения с уже созданными границами.");
                }
            }
            dots.Add(dot);
            if (dots.Count >= 3) isEnoughDots = true;
            return true;
        }/*
        public bool? DotDel(int? position = null)
        {
            if (position > dots.Count - 1) return null;
            if (position == null) dots.RemoveAt(dots.Count - 1);
            else dots.RemoveAt((int)position);
            if (dots.Count < 3)
            {
                isEnoughDots = false;
                isPolygonBuilt = false;
                borders.Clear();
            }
            return true;
        }*/
        public void Clear()
        {
            dots.Clear();
            borders.Clear();
            isEnoughDots = false;
            isPolygonBuilt = false;
        }
        public bool? Build() // Заполнение списка векторов сторон полигона.
        {
            if (!IsEnoughDots) return null;
            int i, dotsCount = dots.Count; 
            Segment segEnd = new Segment(dots[dotsCount - 1], dots[0]);
            Segment seg = new Segment();
            for (i = 0; i < dotsCount - 1; i++) // Поиск пересечений замыкающей границы полигона с другими границами.
            {
                seg.Set(dots[i], dots[i + 1]);
                if (PrimitivesMath.SegmentsCrossing(seg, segEnd))
                    throw new PolygonException(this, "Ошибка. Замыкающая граница полигона имеет пересечения с созданными ранее.");
            } 
            borders.Clear();
            for (i = 0; i < dotsCount; ++i) { borders.Add(new Segment(dots[i], dots[(i + 1) % dotsCount])); }
            return isPolygonBuilt = true;
        }
        public bool? TestDotOnPolygon(Dot dot0) // Тестирование нахождения точки в полигоне.
        {
            if ((!IsEnoughDots) || (borders.Count < 3)) return null;
            foreach (Segment seg in borders) if (PrimitivesMath.DotOnSegment(dot0, seg)) return true; // Проверка нахождения точки на границах полигона.
            int i, dotsCount = dots.Count;
            double CrossProd, DotProd,
                ratioDotToPolygon = 0; // Вычисляемый коэффициент для определения нахождения точки на полигоне. Если ~|2Pi|, то точка на полигоне, если ~0, то точка вне. 
            List<VectorDot1> vectorsTest = new List<VectorDot1>(dotsCount); // Список векторов для тестирования нахождения точки на полигоне.
            for (i = 0; i < dotsCount; ++i) { vectorsTest.Add(PrimitivesMath.MakeVectorDot1(dot0, dots[i])); }
            for (i = 0; i < dotsCount; ++i)
            {
                CrossProd = PrimitivesMath.CrossProduct(vectorsTest[i], vectorsTest[(i + 1) % dotsCount]);
                DotProd = PrimitivesMath.DotProduct(vectorsTest[i], vectorsTest[(i + 1) % dotsCount]);
                ratioDotToPolygon += Math.Atan2(CrossProd, DotProd);
            }
            //Console.WriteLine($"{ratioDotToPolygon}");
            if ((int)ratioDotToPolygon == 0) return false;
            return true;
        }
    }
}
