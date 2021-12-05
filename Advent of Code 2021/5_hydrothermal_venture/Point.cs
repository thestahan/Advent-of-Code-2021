namespace _5_hydrothermal_venture
{
    internal record Point
    {
        public Point(string x, string y)
        {
            X = Int32.Parse(x);
            Y = Int32.Parse(y);
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
