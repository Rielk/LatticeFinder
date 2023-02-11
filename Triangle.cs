namespace LatticeFinder;
public record Triangle(Point Point1, Point Point2, Point Point3)
{
    private double? area = null;
    public double Area
    {
        get
        {
            if (area.HasValue) return area.Value;
            area = CalculateArea();
            return area.Value;
        }
    }

    public IEnumerable<Point> AllPoints
    {
        get
        {
            yield return Point1;
            yield return Point2;
            yield return Point3;
        }
    }

    internal bool OverlapsRectangle(double width, double height) //Width and height of rectangle centered at (0,0)
    {
        if (AllPoints.Any(p => p.InRectangle(width, height)))
            return true;
        else if (RectangleCorners().Any(prop => prop.InTriangle(this)))
            return true;
        return false;


        IEnumerable<Point> RectangleCorners()
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            yield return new Point(halfWidth, halfHeight);
            yield return new Point(-halfWidth, halfHeight);
            yield return new Point(halfWidth, -halfHeight);
            yield return new Point(-halfWidth, -halfHeight);
        }
    }

    public double CalculateArea() //Explanation: https://www.omnicalculator.com/math/area-triangle-coordinates
    {
        return Math.Abs((Point1.X * (Point2.Y - Point3.Y))
                      + (Point2.X * (Point3.Y - Point1.Y))
                      + (Point3.X * (Point1.Y - Point2.Y))) / 2;
    }
}
