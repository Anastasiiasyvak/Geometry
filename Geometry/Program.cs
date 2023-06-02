// namespace Geometry;
//
// static class Program
// {
//     static void Main()
//     {    
//         string filePath = @"C:\Users\Admin\RiderProjects\Geometry\Geometry\Ukraine points.csv.txt";
//         Console.WriteLine("Separate the elements with a semicolon");
//         string input = Console.ReadLine()!;
//         string[] strElements = input.Split(";", StringSplitOptions.RemoveEmptyEntries);
//         double[] elements = Array.ConvertAll(strElements, double.Parse);
//         var latitude = elements[0];
//         var longitude = elements[1];
//         var radius = elements[2];
//         string[] lines = File.ReadAllLines(filePath);
//
//         foreach (string line in lines)
//         {
//             string[] parts = line.Split(";", StringSplitOptions.RemoveEmptyEntries);
//             
//             if (parts.Length >= 5)
//             {
//                 var partsLatitude = double.Parse(parts[0]);
//                 var partsLongitude = double.Parse(parts[1]);
//                 var partsName = parts[4];
//             
//                 // haversine ↓↓↓
//                 double earthRadius = 6371;
//                 double deltaLatitude = DegreeToRadian(partsLatitude - latitude);
//                 double deltaLongitude = DegreeToRadian(partsLongitude - longitude);
//
//                 double a = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
//                            Math.Cos(DegreeToRadian(latitude)) * Math.Cos(DegreeToRadian(partsLatitude)) *
//                            Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
//                 double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
//                 double distance = earthRadius * c;
//
//                 if (distance < radius)
//                 {
//                     Console.WriteLine("Point: " + partsName);
//                 }
//             }
//         }
//     }
//
//     static double DegreeToRadian(double degree)
//     {
//         return degree * Math.PI / 180;
//     }
// }