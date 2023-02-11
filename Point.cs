namespace LatticeFinder;
public record Point(double X, double Y)
{
    internal bool InRectangle(double width, double height) //Width and height of rectangle centered at (0,0)
    {
        double right = width / 2;
        if (X > right) return false;
        double left = -right;
        if (X < left) return false;
        double top = height / 2;
        if (Y > top) return false;
        double bottom = -top;
        if (Y < bottom) return false;

        return true;
    }

    internal bool InTriangle(Triangle triangle)
    {
        double totalArea = triangle.Area;
        double area1 = new Triangle(triangle.Point1, triangle.Point2, this).Area;
        double area2 = new Triangle(triangle.Point2, triangle.Point3, this).Area;
        double area3 = new Triangle(triangle.Point3, triangle.Point1, this).Area;
        if (area1 + area2 + area3 > totalArea)
            return false;
        return true;
    }
}
