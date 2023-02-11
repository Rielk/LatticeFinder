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

    /// <summary>
    /// Converts the point to angles. For a camera at (0,0), positioned a distance of Height above the latice.
    /// </summary>
    /// <param name="height">Height of the camera above (0,0)</param>
    /// <param name="a">Radius on the 2D plane from (0,0)</param>
    /// <param name="theta">Angle on the 2D latice plane. 0 is horizontal in the positive x direction, increasing anti-clockwise about (0,0)</param>
    /// <param name="phi">Angle from the camera, 0 is directly towards the plane. Will always be positive</param>
    public void ToPolar(double height, out double a, out double theta, out double phi)
    {
        if (X == 0)
        {
            if (Y > 0)
                theta = Math.PI / 2;
            else if (Y < 0)
                theta = -Math.PI / 2;
            else
                theta = 0;
        }
        else
            theta = Math.Atan(Y / X);

        //Correct for full circle (technically not needed)
        if (X < 0) theta += Math.PI;
        else if (Y < 0) theta += 2 * Math.PI;

        a = Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        phi = Math.Atan(a / height);
    }
}
