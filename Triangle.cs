namespace LatticeFinder;
public class Triangle
{
    public Point Point1 { get; private set; }
    public Point Point2 { get; private set; }
    public Point Point3 { get; private set; }

    public Triangle(Point point1, Point point2, Point point3)
    {
        Point1 = point1;
        Point2 = point2;
        Point3 = point3;
    }
}
