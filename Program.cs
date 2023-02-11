// See https://aka.ms/new-console-template for more information

using LatticeFinder;

Console.WriteLine("Rectangle Width (in units):");
string? rectWidthS = Console.ReadLine();
Console.WriteLine("Rectangle Height (in units):");
string? rectHeightS = Console.ReadLine();
Console.WriteLine();

_ = int.TryParse(rectWidthS, out int rectWidth);
_ = int.TryParse(rectHeightS, out int rectHeight);


List<Triangle> triangles = Lattice.FindTrianglesInView(rectWidth, rectHeight);
Console.WriteLine($"Number of Trianlges overlapping square: {triangles.Count}");
