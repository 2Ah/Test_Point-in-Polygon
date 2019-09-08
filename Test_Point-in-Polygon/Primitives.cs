namespace Test_Point_in_Polygon
{
    public struct Dot
    {
        private double x, y;
        public double X { get { return x; } }
        public double Y { get { return y; } }
        public Dot(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        override public string ToString() { return $"({x}; {y})"; }
        public void Set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public struct Segment
    {
        private Dot dot1, dot2;
        public Dot Dot1 { get { return dot1; } }
        public Dot Dot2 { get { return dot2; } }
        public double Dot1_X { get { return dot1.X; } }
        public double Dot1_Y { get { return dot1.Y; } }
        public double Dot2_X { get { return dot2.X; } }
        public double Dot2_Y { get { return dot2.Y; } }
        public Segment(Dot dot1, Dot dot2)
        {
            this.dot1 = dot1;
            this.dot2 = dot2;
        }
        override public string ToString() { return $"[{dot1}{dot2}]"; }
        public void Set(Dot dot1, Dot dot2)
        {
            this.dot1 = dot1;
            this.dot2 = dot2;
        }
    }
    public struct VectorDot1 // Вектор строится из начала координат, поэтому описывается одной точкой.
    {
        Dot dotEnd;
        public Dot DotEnd { get { return dotEnd; } }
        public double X { get { return dotEnd.X; } }
        public double Y { get { return dotEnd.Y; } }
        public VectorDot1(Dot dotEnd) { this.dotEnd = dotEnd; } 
        override public string ToString() { return $"[{dotEnd}]"; }
        public void Set(Dot dotEnd) { this.dotEnd = dotEnd; }
    }
    public struct VectorDot2 // Вектор описывается 2 точками.
    {
        Dot dotBegin, dotEnd;
        public Dot DotBegin { get { return dotBegin; } }
        public Dot DotEnd { get { return dotEnd; } }
        public double BeginX { get { return dotBegin.X; } }
        public double BeginY { get { return dotBegin.Y; } }
        public double EndX { get { return dotEnd.X; } }
        public double EndY { get { return dotEnd.Y; } }
        public VectorDot2(Dot dotBegin, Dot dotEnd)
        {
            this.dotBegin = dotBegin;
            this.dotEnd = dotEnd;
        }
        override public string ToString() { return $"[{DotBegin}{DotEnd}]"; }
        public void Set(Dot dotBegin, Dot dotEnd)
        {
            this.dotBegin = dotBegin;
            this.dotEnd = dotEnd;
        }
    }
}