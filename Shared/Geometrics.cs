using System.Reflection.Metadata;
using VehicleSpeedControlSystem.Shared.Entities;

namespace VehicleSpeedControlSystem.Shared;
//This class is used to store the geometrics of the land
public class Geometrics
{
    // Define a constant for the radius of the earth in kilometers
    public readonly static double EarthRadius = 6371;

    //This method is used to calculate the area of the land given the coordinates
    public static double CalculateArea(List<Coordinate> coordinates, Units units = Units.Meters)
    {
        if (!IsValidPolygon(coordinates))
        {
            throw new Exception("Invalid polygon");
        }
        double area = 0;
        int j = coordinates.Count - 1;
        for (int i = 0; i < coordinates.Count; i++)
        {
            area += (coordinates[j].Longitude + coordinates[i].Longitude) * (coordinates[j].Longitude - coordinates[i].Latitude);
            j = i;
        }

        switch (units)
        {
            case Units.Meters:
                return Math.Abs(area / 2) * 111111;
            case Units.Kilometers:
                return Math.Abs(area / 2) * 111.111;
            case Units.Miles:
                return Math.Abs(area / 2) * 69.0;
            case Units.Yards:
                return Math.Abs(area / 2) * 111111 * 1.09361;
            default:
                return Math.Abs(area / 2);
        }
    }

    //function that checks if the given set of coordinates is a valid polygon
    public static bool IsValidPolygon(List<Coordinate> coordinates)
    {
        //check if the coordinates are more than 3
        if (coordinates.Count < 3) return false;

        int j = coordinates.Count - 1;
        for (int i = 0; i < coordinates.Count; j = i++)
            if (coordinates[i].Latitude == coordinates[j].Latitude && coordinates[i].Longitude == coordinates[j].Longitude)
                return false;
        return true;
    }

    //function that checks if the given set of coordinates is in a given coordinate list parameter
    public static bool IsInside(Coordinate point, List<Coordinate> perimeter)
    {
        if (IsValidPolygon(perimeter) is false) throw new Exception("Invalid polygon");
        int i, j;
        bool c = false;
        for (i = 0, j = perimeter.Count - 1; i < perimeter.Count; j = i++)
        {
            if (((perimeter[i].Latitude > point.Latitude) != (perimeter[j].Latitude > point.Latitude)) &&
                (point.Longitude < (perimeter[j].Longitude - perimeter[i].Longitude)
                    * (point.Latitude - perimeter[i].Latitude)
                    / (perimeter[j].Latitude - perimeter[i].Latitude) + perimeter[i].Longitude))
                c = !c;
        }

        return c;
    }

    public static List<Coordinate> OrderCoordinates(List<Coordinate> coordinates)
    {
        // Find the center of all points
        double centerX = 0;
        double centerY = 0;
        foreach (var coordinate in coordinates)
        {
            centerX += coordinate.Latitude;
            centerY += coordinate.Longitude;
        }
        centerX /= coordinates.Count;
        centerY /= coordinates.Count;

        // Sort by angle from center
        coordinates.Sort((a, b) =>
        {
            double a1 = (Math.Atan2(a.Latitude - centerX, a.Longitude - centerY) + 2 * Math.PI) % (2 * Math.PI);
            double a2 = (Math.Atan2(b.Latitude - centerX, b.Longitude - centerY) + 2 * Math.PI) % (2 * Math.PI);
            return a1.CompareTo(a2);
        });

        return coordinates;
    }

    // A function to calculate the area of a triangle using Heron's formula
    public static double Area(Triangle t)
    {
        // Find the lengths of the sides
        double a = Math.Sqrt(Math.Pow(t.p1.Latitude - t.p2.Latitude, 2) + Math.Pow(t.p1.Longitude - t.p2.Longitude, 2));
        double b = Math.Sqrt(Math.Pow(t.p2.Latitude - t.p3.Latitude, 2) + Math.Pow(t.p2.Longitude - t.p3.Longitude, 2));
        double c = Math.Sqrt(Math.Pow(t.p3.Latitude - t.p1.Latitude, 2) + Math.Pow(t.p3.Longitude - t.p1.Longitude, 2));

        // Find the semi-perimeter
        double s = (a + b + c) / 2;

        // Return the area using Heron's formula
        return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
    }

    // A function to calculate the centroid of a triangle as the average of the vertices
    public static Coordinate Centroid(Triangle t)
    {
        // Find the average of the x and y coordinates of the vertices
        double latitude = (t.p1.Latitude + t.p2.Latitude + t.p3.Latitude) / 3;
        double longitude = (t.p1.Longitude + t.p2.Longitude + t.p3.Longitude) / 3;

        // Return a new point with the calculated coordinates
        return new Coordinate(latitude, longitude);
    }

    // A function to find the center of a polygon given a list of edge coordinates
    public static Coordinate Center(List<Coordinate> edges)
    {
        // First reorder the coordinates to eliminate triangluation problems
        edges = OrderCoordinates(edges);
        // Check if the list is empty or has less than three points
        if (edges == null || edges.Count < 3)
        {
            throw new ArgumentException("Invalid input");
        }

        // Check if the polygon is regular by comparing the distances from each edge point to the first point
        bool regular = true;
        double distance = Math.Sqrt(Math.Pow(edges[0].Latitude - edges[1].Latitude, 2) + Math.Pow(edges[0].Longitude - edges[1].Longitude, 2));
        for (int i = 2; i < edges.Count; i++)
        {
            double d = Math.Sqrt(Math.Pow(edges[0].Latitude - edges[i].Latitude, 2) + Math.Pow(edges[0].Longitude - edges[i].Longitude, 2));
            if (d != distance)
            {
                regular = false;
                break;
            }
        }

        // If the polygon is regular, return the average of the edge points as the center
        if (regular)
        {
            double latitude = edges.Average(p => p.Latitude);
            double longitude = edges.Average(p => p.Longitude);
            return new Coordinate(latitude, longitude);
        }

        // If the polygon is irregular, divide it into triangles by choosing an arbitrary point as the origin and connecting it to every other edge point
        List<Triangle> triangles = new List<Triangle>();
        Coordinate origin = edges[0];
        for (int i = 1; i < edges.Count - 1; i++)
        {
            triangles.Add(new Triangle(origin, edges[i], edges[i + 1]));
        }

        // Find the centroid and area of each triangle and store them in lists
        List<Coordinate> centroids = new List<Coordinate>();
        List<double> areas = new List<double>();
        foreach (Triangle t in triangles)
        {
            centroids.Add(Centroid(t));
            areas.Add(Area(t));
        }

        // Find the total area of the polygon by summing up the areas of the triangles
        double totalArea = areas.Sum();
        // Find the weighted average of the centroids of the triangles using the areas as weights
        double x = 0;
        double y = 0;
        for (int i = 0; i < centroids.Count; i++)
        {
            x += centroids[i].Latitude * areas[i] / totalArea;
            y += centroids[i].Longitude * areas[i] / totalArea;
        }
        // Return a new point with the calculated coordinates as the center of the polygon
        return new Coordinate(x, y);
    }

    // Define a method to convert degrees to radians
    public static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180;
    }

    // Define a method to calculate the area of a polygon using the formula from [Area of any polygon (Coordinate Geometry)]
    public static double PolygonArea(List<Coordinate> points)
    {
        // Choose any point as the origin
        Coordinate origin = points[0];

        // Convert the points into Cartesian coordinates relative to the origin
        List<double> x = new List<double>();
        List<double> y = new List<double>();
        foreach (Coordinate p in points)
        {
            x.Add(HaversineDistance(origin, new Coordinate(origin.Latitude, p.Longitude)));
            y.Add(HaversineDistance(origin, new Coordinate(p.Latitude, origin.Longitude)));
        }

        // Add the origin again to close the polygon
        x.Add(x[0]);
        y.Add(y[0]);

        // Calculate the area using the formula
        double area = 0;
        for (int i = 0; i < x.Count - 1; i++)
        {
            area += x[i] * y[i + 1] - y[i] * x[i + 1];
        }
        area = Math.Abs(area / 2);

        return area;
    }

    // Define a method to calculate the distance between two points using the Haversine formula
    public static double HaversineDistance(Coordinate p1, Coordinate p2)
    {
        double dLat = ToRadians(p2.Latitude - p1.Latitude);
        double dLon = ToRadians(p2.Longitude - p1.Longitude);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(ToRadians(p1.Latitude)) * Math.Cos(ToRadians(p2.Latitude)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return EarthRadius * c;
    }

    // Define a method to calculate the zoom from the area using the inverse formula 
    public static double ZoomFromArea(double area)
    {
        // Define the constants for the formula
        double a = 1.33e10; double b = 0.48;
        // Calculate the zoom using the formula
        double zoom = (Math.Log(area) - Math.Log(a)) / Math.Log(b);
        return zoom;
    }
}

public class Triangle
{
    public Coordinate p1;
    public Coordinate p2;
    public Coordinate p3;

    public Triangle(Coordinate p1, Coordinate p2, Coordinate p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }




}






public enum Units
{
    Meters,
    Kilometers,
    Miles,
    Yards,
}

