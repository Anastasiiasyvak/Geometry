namespace Geometry;

public class TreeNode
{
    public double MinLatitude { get; set; }
    public double MaxLatitude { get; set; }
    public double MinLongitude { get; set; }
    public double MaxLongitude { get; set; }

}

static class Program
{
    static void Main()
    {
        TreeNode root = new TreeNode
        {
            MinLatitude = double.MaxValue,
            MaxLatitude = double.MinValue,
            MinLongitude = double.MaxValue,
            MaxLongitude = double.MinValue
        };

        string[] lines = File.ReadAllLines(@"C:\Users\Admin\RiderProjects\Geometry\Geometry\Ukraine points.csv.txt");
        foreach (string line in lines)
        {
            string[] parts = line.Split(";", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 5)
            {
                var latitude = double.Parse(parts[0]);
                var longitude = double.Parse(parts[1]);

                root.MinLatitude = Math.Min(root.MinLatitude, latitude);
                root.MaxLatitude = Math.Max(root.MaxLatitude, latitude);
                root.MinLongitude = Math.Min(root.MinLongitude, longitude);
                root.MaxLongitude = Math.Max(root.MaxLongitude, longitude);
            }
        }
        
        Console.WriteLine($"Min latitude: {root.MinLatitude}");
        Console.WriteLine($"Max latitude: {root.MaxLatitude}");
        Console.WriteLine($"Min longitude: {root.MinLongitude}");
        Console.WriteLine($"Max longitude: {root.MaxLongitude}");
    }
}