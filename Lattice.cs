namespace LatticeFinder;
public class Lattice
{
    private static double VertUnit { get; } = Math.Sqrt(3) / 2;

    public double Width { get; init; }
    public double Height { get; init; }

    public Lattice(int width, int height) : this((double)width, (double)height) { }
    public Lattice(double width, double height)  //Width and height of rectangle centered on a lattice node
    {
        if (height == 0) throw new NotImplementedException("Getting triangles assumes there are 2 rows so height zero will break this");

        Width = width;
        Height = height;
    }

    public IEnumerable<Triangle> FindTrianglesInView()
    {
        Point[][] pointArray = GenerateLatticePoints().Select(e => e.ToArray()).ToArray();

        bool onLongRow = pointArray[0].Length > pointArray[1].Length;

        foreach (int rowIndex in Enumerable.Range(0, pointArray.Length))
        {
            Point[] thisRow = pointArray[rowIndex];
            Point[]? nextRow = rowIndex + 1 >= pointArray.Length ? null : pointArray[rowIndex + 1];
            Point[]? prevRow = rowIndex - 1 < 0 ? null : pointArray[rowIndex - 1];

            foreach (int colIndex in Enumerable.Range(0, thisRow.Length - 1))
            {
                int adjIndex = onLongRow ? colIndex : colIndex + 1;
                int nextIndex = colIndex + 1;

                Point point1, point2, point3;
                point1 = thisRow[colIndex];
                point2 = thisRow[nextIndex];
                if (nextRow != null)
                {
                    point3 = nextRow[adjIndex];
                    Triangle ret = new(point1, point2, point3);
                    if (ret.OverlapsRectangle(Width, Height)) //I'm not convinced this Overlap check is necessary, but it's written so...
                        yield return ret;
                }
                if (prevRow != null)
                {
                    point3 = prevRow[adjIndex];
                    Triangle ret = new(point1, point2, point3);
                    if (ret.OverlapsRectangle(Width, Height)) //I'm not convinced this Overlap check is necessary, but it's written so...
                        yield return ret;
                }
            }

            onLongRow = !onLongRow;
        }
    }

    private IEnumerable<IEnumerable<Point>> GenerateLatticePoints()
    {
        double unitWidth = Width / 2;
        bool extendOddRows;
        if (Math.Floor(unitWidth) == unitWidth)
            extendOddRows = true;
        else if (Math.Floor(unitWidth) == unitWidth - .5)
            extendOddRows = false;
        else
            extendOddRows = Math.Ceiling(unitWidth) == Math.Round(unitWidth);
        int countWide = (int)Math.Ceiling(unitWidth); //How long the even rows are
        int countHigh = (int)Math.Ceiling((Height / 2) / VertUnit);

        foreach (double y in Enumerable.Range(-countHigh, 2 * countHigh + 1))
        {
            int count;
            double xOffset;
            if (y % 2 == 0)
            {
                count = countWide * 2 + 1;
                xOffset = 0;
            }
            else
            {
                count = extendOddRows ? countWide * 2 + 2 : countWide * 2;
                xOffset = extendOddRows ? -.5 : .5;
            }

            yield return GenerateLatticeRow(y, -countWide, count, xOffset);
        }
    }

    private IEnumerable<Point> GenerateLatticeRow(double y, int start, int count, double xOffset)
    {
        foreach (double x in Enumerable.Range(start, count))
            yield return new(x + xOffset, y * VertUnit);
    }
}
