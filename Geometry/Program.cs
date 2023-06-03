// using System.Diagnostics; 
//
// namespace Geometry
// {
//     static class Program
//     {
//         static void Main()
//         {
//             string filePath = "/home/nastia/for_new_projects/Geometry/Geometry/Ukraine points.csv.txt";
//             Console.WriteLine("Separate the elements with a semicolon");
//             string input = Console.ReadLine();
//             string[] strElements = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
//             double[] elements = Array.ConvertAll(strElements, double.Parse);
//             var latitude = elements[0];
//             var longitude = elements[1];
//             var radius = elements[2];
//             string[] lines = File.ReadAllLines(filePath);
//             double earthRadius = 6371; // середній радіус землі в кілометрах 
//             
//             var sw = new Stopwatch();
//             sw.Start();
//             
//             foreach (string line in lines)
//             {
//                 string[] parts = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
//
//                 if (parts.Length >= 5)
//                 {
//                     var partsLatitude = double.Parse(parts[0]);
//                     var partsLongitude = double.Parse(parts[1]);
//                     var partsName = parts[4];
//
//                     // haversine ↓↓↓
//
//                     double deltaLatitude = DegreeToRadian(partsLatitude - latitude);
//                     double deltaLongitude = DegreeToRadian(partsLongitude - longitude);
//
//                     double a = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
//                                Math.Cos(DegreeToRadian(latitude)) * Math.Cos(DegreeToRadian(partsLatitude)) *
//                                Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
//                     double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
//                     double distance = earthRadius * c;
//
//                     if (distance <= radius)
//                     {
//                         Console.WriteLine("Point: " + partsName);
//                     }
//                 }
//             }
//             
//             sw.Stop();
//             Console.WriteLine($"Elapsed time: {sw.Elapsed}");
//         }
//
//         static double DegreeToRadian(double degree)
//         {
//             return degree * Math.PI / 180;
//         }
//     }
// }