// See https://aka.ms/new-console-template for more information

using LatticeFinder;

Console.WriteLine("Rectangle Width (in units):");
string? rectWidthS = Console.ReadLine();
Console.WriteLine("Rectangle Height (in units):");
string? rectHeightS = Console.ReadLine();
Console.WriteLine();

_ = int.TryParse(rectWidthS, out int rectWidth);
_ = int.TryParse(rectHeightS, out int rectHeight);

IEnumerable<Triangle> triangles = Lattice.FindTrianglesInView(rectWidth, rectHeight);
Console.WriteLine($"Number of Trianlges overlapping square: {triangles.Count()}");
Console.WriteLine();
Point ranPoint = triangles.Skip(3).First().Point3;
ranPoint.ToPolar(2, out double a, out double theta, out double phi);
Console.WriteLine($"Random point: X = {ranPoint.X}, Y = {ranPoint.Y}");
Console.WriteLine($"Random point converted to Polar: Camera Height = 2, a = {a}, Theta = {theta}, Phi = {phi}");
