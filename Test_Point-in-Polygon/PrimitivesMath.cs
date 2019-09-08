using System;
namespace Test_Point_in_Polygon
{
    public static class PrimitivesMath
    {
        static public bool DotsEqual(Dot a, Dot b) { return ((a.X == b.X) && (a.Y == b.Y)); }
        static public VectorDot1 MakeVectorDot1(Dot DotBegin, Dot DotEnd) { return new VectorDot1(new Dot(DotEnd.X - DotBegin.X, DotEnd.Y - DotBegin.Y)); }
        static public VectorDot1 MakeVectorDot1(VectorDot2 v) { return new VectorDot1(new Dot(v.EndX - v.BeginX, v.EndY - v.BeginY)); }
        static public VectorDot1 Sum(VectorDot1 a, VectorDot1 b) { return new VectorDot1(new Dot(a.X + b.X, a.Y + b.Y)); } // Сумма веторов.
        static public VectorDot1 Dif(VectorDot1 a, VectorDot1 b) { return new VectorDot1(new Dot(a.X - b.X, a.Y - b.Y)); } // Разность веторов.
        static public double DotProduct(VectorDot1 a, VectorDot1 b) { return (a.X * b.X) + (a.Y * b.Y); } // Скалярное произведение векторов.
        static public double DotProduct(VectorDot2 a, VectorDot2 b) { return DotProduct(MakeVectorDot1(a), MakeVectorDot1(b)); }
        static public double CrossProduct(VectorDot1 a, VectorDot1 b) { return (a.X * b.Y) - (a.Y * b.X); } // Векторное произведение векторов.
        static public double CrossProduct(VectorDot2 a, VectorDot2 b) { return CrossProduct(MakeVectorDot1(a), MakeVectorDot1(b)); }
        static public bool DotOnSegment(Dot dot, Segment seg) // Проверка нахождения точки на отрезке.
        {
            double CrossProd = CrossProduct(new VectorDot2(seg.Dot1, seg.Dot2), new VectorDot2(seg.Dot1, dot));
            double DotProdAB = DotProduct(new VectorDot2(seg.Dot1, seg.Dot2), new VectorDot2(seg.Dot1, dot));
            double DotProdBA = DotProduct(new VectorDot2(seg.Dot2, seg.Dot1), new VectorDot2(seg.Dot2, dot));
            //Console.WriteLine($"CrossProd={CrossProd}, DotProdAB={DotProdAB}, DotProdBA={DotProdBA}");
            if ((Math.Round(CrossProd, 9) == 0) && (DotProdAB >= 0) && (DotProdBA >= 0)) return true; // 
            return false;
        }
        static public bool SegmentsCrossing(Segment seg1, Segment seg2) // Метод обнаружения пересечения отрезков. Не учитывает нахождение точки одного отрезка на другом отрезке.
        {
            double CrossProd_seg1_seg2A = CrossProduct(new VectorDot2(seg1.Dot1, seg1.Dot2), new VectorDot2(seg1.Dot1, seg2.Dot1));
            double CrossProd_seg1_seg2B = CrossProduct(new VectorDot2(seg1.Dot1, seg1.Dot2), new VectorDot2(seg1.Dot1, seg2.Dot2));
            double CrossProd_seg2_seg1A = CrossProduct(new VectorDot2(seg2.Dot1, seg2.Dot2), new VectorDot2(seg2.Dot1, seg1.Dot1));
            double CrossProd_seg2_seg1B = CrossProduct(new VectorDot2(seg2.Dot1, seg2.Dot2), new VectorDot2(seg2.Dot1, seg1.Dot2));
            if (((CrossProd_seg1_seg2A * CrossProd_seg1_seg2B) < 0) &&
                ((CrossProd_seg2_seg1A * CrossProd_seg2_seg1B) < 0)) return true;
            return false;
        }
    }
}
