using System.Diagnostics;

namespace Geometry
{
    public class TreeNode
    {
        public double MinLatitude { get; set; } 
        public double MaxLatitude { get; set; }
        public double MinLongitude { get; set; }
        public double MaxLongitude { get; set; }
        public List<Point> Points { get; }
        public List<TreeNode> Children { get;}

        public TreeNode()
        {
            Points = new List<Point>();
            Children = new List<TreeNode>();
        }
    }

    public class Point
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    static class Program
    {
        static void Main()
        {
            string filePath = @"C:\Users\Admin\RiderProjects\Geometry\Geometry\Ukraine points.csv.txt";
            Console.WriteLine("Separate the elements with a semicolon");
            string input = Console.ReadLine()!;
            string[] strElements = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
            double[] elements = Array.ConvertAll(strElements, double.Parse);
            var latitude = elements[0];
            var longitude = elements[1];
            var radius = elements[2];
            string[] lines = File.ReadAllLines(filePath);

            TreeNode root = new TreeNode
            {
                MinLatitude = double.MaxValue,
                MaxLatitude = double.MinValue,
                MinLongitude = double.MaxValue,
                MaxLongitude = double.MinValue
            };

            foreach (string line in lines)
            {
                string[] parts = line.Split(";", StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length >= 5)
                {
                    var partsLatitude = double.Parse(parts[0]);
                    var partsLongitude = double.Parse(parts[1]);
                    var partsName = parts[4];
                    
                    root.MinLatitude = Math.Min(root.MinLatitude, partsLatitude);
                    root.MaxLatitude = Math.Max(root.MaxLatitude, partsLatitude);
                    root.MinLongitude = Math.Min(root.MinLongitude, partsLongitude);
                    root.MaxLongitude = Math.Max(root.MaxLongitude, partsLongitude);
                    
                    root.Points.Add(new Point { Name = partsName, Latitude = partsLatitude, Longitude = partsLongitude });
                }
            }

            BuildRTree(root, "latitude", 10);

            List<Point> foundPoints = new List<Point>();

            var sw = new Stopwatch();
            sw.Start();

            FindPointsInTree(root, latitude, longitude, radius, foundPoints);

            sw.Stop();
            
            Console.WriteLine($"Found {foundPoints.Count} points within the specified radius:");
            foreach (var point in foundPoints)
            {
                Console.WriteLine($"Point: {point.Name} (Latitude: {point.Latitude} / Longitude: {point.Longitude})");
            }
            
            Console.WriteLine($"Elapsed time: {sw.Elapsed}");
        }
        
        static void BuildRTree(TreeNode node, string axis, int threshold)
        {
            if (axis == "latitude")
            {
                if (node.Points.Count <= threshold)
                {
                    return;
                }

                node.Points.Sort((p1, p2) => p1.Latitude.CompareTo(p2.Latitude));
                double medianLatitude = node.Points[node.Points.Count / 2].Latitude;

                TreeNode leftNode = new TreeNode
                {
                    MinLatitude = node.MinLatitude,
                    MaxLatitude = medianLatitude,
                    MinLongitude = node.MinLongitude,
                    MaxLongitude = node.MaxLongitude
                };

                TreeNode rightNode = new TreeNode
                {
                    MinLatitude = medianLatitude,
                    MaxLatitude = node.MaxLatitude,
                    MinLongitude = node.MinLongitude,
                    MaxLongitude = node.MaxLongitude
                };

                node.Children.Add(leftNode);
                node.Children.Add(rightNode);

                BuildRTree(leftNode, "longitude", threshold);
                BuildRTree(rightNode, "longitude", threshold);
            }
            
            else if (axis == "longitude")
            {
                if (node.Points.Count <= threshold)
                {
                    return;
                }
                
                node.Points.Sort((p1, p2) => p1.Longitude.CompareTo(p2.Longitude));
                double medianLongitude = node.Points[node.Points.Count / 2].Longitude;

                TreeNode leftNode = new TreeNode
                {
                    MinLatitude = node.MinLatitude,
                    MaxLatitude = node.MaxLatitude,
                    MinLongitude = node.MinLongitude,
                    MaxLongitude = medianLongitude
                };

                TreeNode rightNode = new TreeNode
                {
                    MinLatitude = node.MinLatitude,
                    MaxLatitude = node.MaxLatitude,
                    MinLongitude = medianLongitude,
                    MaxLongitude = node.MaxLongitude
                };

                node.Children.Add(leftNode);
                node.Children.Add(rightNode);

                BuildRTree(leftNode, "latitude", threshold);
                BuildRTree(rightNode, "latitude", threshold);
            }
        }

        static void FindPointsInTree(TreeNode node, double latitude, double longitude, double radius, List<Point> foundPoints)
        {
            if (!RectanglesIntersect(node.MinLatitude, node.MaxLatitude, node.MinLongitude, node.MaxLongitude, latitude - radius, latitude + radius, longitude - radius, longitude + radius))
            {
                return;
            }

            foreach (var point in node.Points)
            {
                // haversine ↓↓↓
                double deltaLatitude = DegreeToRadian(point.Latitude - latitude);
                double deltaLongitude = DegreeToRadian(point.Longitude - longitude);

                double a = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
                           Math.Cos(DegreeToRadian(latitude)) * Math.Cos(DegreeToRadian(point.Latitude)) *
                           Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double distance = 6371 * c;

                if (distance <= radius)
                {
                    foundPoints.Add(point);
                }
            }

            foreach (var childNode in node.Children)
            {
                FindPointsInTree(childNode, latitude, longitude, radius, foundPoints);
            }
        }

        static bool RectanglesIntersect(double minLat1, double maxLat1, double minLon1, double maxLon1, double minLat2, double maxLat2, double minLon2, double maxLon2)
        {
            return minLat1 <= maxLat2 && maxLat1 >= minLat2 && minLon1 <= maxLon2 && maxLon1 >= minLon2;
        }
        
        static double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180;
        }
    }
}
