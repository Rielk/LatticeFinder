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

    internal bool OverlapsRectangle(double width, double height) //Width and height of rectangle centered at (0,0)
    {
        if (Point1.InRectangle(width, height))
            return true;
        if (Point2.InRectangle(width, height))
            return true;
        if (Point3.InRectangle(width, height))
            return true;
        if (RectangleCorners().Any(prop => prop.InTriangle(this)))
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
