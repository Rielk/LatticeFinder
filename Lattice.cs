namespace LatticeFinder;
public static class Lattice
{
    private static double VertUnit { get; } = Math.Sqrt(3) / 2;

    public static IEnumerable<Triangle> FindTrianglesInView(double width, double height)
    {
        IEnumerable<IEnumerable<Point>> pointEnum = GenerateLatticePoints(width, height);

        Point[] prevRow = pointEnum.First().ToArray();
        Point[] thisRow = prevRow;

        foreach (IEnumerable<Point> rowEnum in pointEnum.Skip(1))
        {
            prevRow = thisRow;
            thisRow = rowEnum.ToArray();
            bool onLongRow = thisRow.Length > prevRow.Length;

            //Do the downward point triangles:
            if (onLongRow)
            {
                foreach (int colIndex in Enumerable.Range(1, thisRow.Length - 2)) //End points don't have a triangle above them
                {
                    int leftIndex = colIndex - 1;
                    int rightIndex = colIndex;

                    Point point1, point2, point3;
                    point1 = thisRow[colIndex];
                    point2 = prevRow[leftIndex];
                    point3 = prevRow[rightIndex];

                    if (GenerateAndCheckTriangle(point1, point2, point3, out Triangle? ret))
                        yield return ret;
                }
            }
            else
            {
                foreach (int colIndex in Enumerable.Range(0, thisRow.Length))
                {
                    int leftIndex = colIndex;
                    int rightIndex = colIndex + 1;

                    Point point1, point2, point3;
                    point1 = thisRow[colIndex];
                    point2 = prevRow[leftIndex];
                    point3 = prevRow[rightIndex];

                    if (GenerateAndCheckTriangle(point1, point2, point3, out Triangle? ret))
                        yield return ret;
                }
            }

            //Do the upward point triangles:
            if (onLongRow)
            {
                foreach (int colIndex in Enumerable.Range(0, thisRow.Length - 1))
                {
                    int nextIndex = colIndex + 1;
                    int adjIndex = colIndex;

                    Point point1, point2, point3;
                    point1 = thisRow[colIndex];
                    point2 = thisRow[nextIndex];
                    point3 = prevRow[adjIndex];

                    if (GenerateAndCheckTriangle(point1, point2, point3, out Triangle? ret))
                        yield return ret;
                }
            }
            else
            {
                foreach (int colIndex in Enumerable.Range(0, thisRow.Length - 1))
                {
                    int nextIndex = colIndex + 1;
                    int adjIndex = colIndex + 1;

                    Point point1, point2, point3;
                    point1 = thisRow[colIndex];
                    point2 = thisRow[nextIndex];
                    point3 = prevRow[adjIndex];

                    if (GenerateAndCheckTriangle(point1, point2, point3, out Triangle? ret))
                        yield return ret;
                }
            }
        }

        bool GenerateAndCheckTriangle(Point point1, Point point2, Point point3, out Triangle triangle)
        {
            triangle = new(point1, point2, point3);
            if (triangle.OverlapsRectangle(width, height)) //I'm not convinced this Overlap check is necessary, but it's written so...
                return true;
            else
                return false;
        }
    }

    private static IEnumerable<IEnumerable<Point>> GenerateLatticePoints(double width, double height)
    {
        double unitWidth = width / 2;
        bool extendOddRows;
        if (Math.Floor(unitWidth) == unitWidth)
            extendOddRows = true;
        else if (Math.Floor(unitWidth) == unitWidth - .5)
            extendOddRows = false;
        else
            extendOddRows = Math.Ceiling(unitWidth) == Math.Round(unitWidth);
        int countWide = (int)Math.Ceiling(unitWidth); //How long the even rows are
        int countHigh = (int)Math.Ceiling((height / 2) / VertUnit);

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

    private static IEnumerable<Point> GenerateLatticeRow(double y, int start, int count, double xOffset)
    {
        foreach (double x in Enumerable.Range(start, count))
            yield return new(x + xOffset, y * VertUnit);
    }
}
