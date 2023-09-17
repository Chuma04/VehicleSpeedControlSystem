global using dymaptic.GeoBlazor.Core;
using dymaptic.GeoBlazor.Core.Objects;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleSpeedControlSystem.Shared.Entities;

public class RestrictedArea : BaseEntity
{
    public List<Coordinate> Perimeter { get; set; } = new();
    
    public int SpeedLimit { get; set; }
    
    // [NotMapped]
    // public double Area => Geometrics.CalculateArea(Perimeter, Units.Meters);
    // public Landlord? Landlord { get; set; } = new();
    public Classification Classification { get; set; } = Classification.Residential;
    // public Purchase? Purchase { get; set; } = new();
    // public TitleDeed? TitleDeed { get; set; } = new();
    // public List<Ownership> OwnershipHistory { get; set; } = new();
    // public bool IsReserved { get; set; }

     public string HtmlString()
     {
         return $"""
             <b>Landlord:</b> {Name}<br>
             <b>Classification:</b> {Classification}<br>
             <b>Perimeter:</b> {Perimeter.Count}<br>
             <b>Id:</b> {Id}<br>
             <a href='https://www.google.com/maps/search/?api=1&query={Perimeter[0].Latitude},{Perimeter[0].Longitude}'>View on Google Maps</a><br>
             <h3 >Click the search button that has appeared on top to view the details of this land</h3>
             """;
     }

    [NotMapped]
    public MapPath MapPath=> new MapPath(Perimeter.Select(x=>new MapPoint(x.Longitude,x.Latitude)));
}
    
public class Coordinate : BaseEntity
{
    public Coordinate()
    {
    }
    public Coordinate(double? mapPointLatitude, double? mapPointLongitude)
    {
        Latitude = mapPointLatitude ?? 0;
        Longitude = mapPointLongitude ?? 0;
    }
    public int Land { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}
    
public enum Classification {
    School,
    Residential,
    Commercial,
    Industrial,
    Agricultural,
}
    
// public class Purchase : BaseEntity
// {
//     public DateTime Date { get; set; }
//     public double Price { get; set; }
// }
    
// public class TitleDeed : BaseEntity
// {
//     public string Number { get; set; }  = string.Empty;
//     public DateTime Date { get; set; }
//     public string Location { get; set; } = string.Empty;
//     public string Size { get; set; } = string.Empty;
//     public string Type { get; set; } = string.Empty;
// }
    

// public class Ownership : BaseEntity
// {
//     public Landlord? User { get; set; }
//     public DateTime Date { get; set; }
// }
