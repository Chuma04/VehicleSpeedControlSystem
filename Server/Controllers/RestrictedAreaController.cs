namespace VehicleSpeedControlSystem.Server.Controllers;

public class  RestrictedAreaController : BaseController<RestrictedArea>
{
    public RestrictedAreaController(DataContext context) : base(context){}

    [HttpGet]
    public override ActionResult<List<RestrictedArea>> Get() => _context.RestrictedAreas.Include(land=>land.Perimeter).ToList() ?? new() ;
    
    // [HttpGet("GetLandByLandlordId/{landlordId}")]
    // public ActionResult<List<RestrictedArea>> GetLandByLandlordId(int landlordId)
    // {
    //     var land = _context.RestrictedAreas.Include(land=>land.Landlord).ToList()
    //         .Where(x=>x.Landlord != null && x.Landlord.Id==landlordId).ToList();
    //     return land.IsNullOrEmpty() ? BadRequest($"There is no restrictedArea with LandlordId = {landlordId}") : Ok(land);
    // }
    
    // [HttpGet("ReserveLand/{landId}")]
    // public ActionResult<RestrictedArea> ReserveLand(int landId)
    // {
    //     var land = _context.RestrictedAreas.FirstOrDefault(x=>x.Id==landId); //This is not going to work in this context, but okay
    //     if (land is null) return BadRequest($"There is no restrictedArea with Id = {landId}");
    //     land.IsReserved = true;
    //     _context.SaveChanges();
    //     return Ok(land);
    // }
    
    // [HttpGet("UnReserveLand/{landId}")]
    // public ActionResult<RestrictedArea> UnReserveLand(int landId)
    // {
    //     var land = _context.RestrictedAreas.FirstOrDefault(x=>x.Id==landId); //This is not going to work in this context, but okay
    //     if (land is null) return BadRequest($"There is no restrictedArea with Id = {landId}");
    //     land.IsReserved = false;
    //     _context.SaveChanges();
    //     return Ok(land);
    // }
    
    [HttpPost]
    [HttpGet("CreateRestrictedArea")]
    public override  ActionResult<RestrictedArea> Post(RestrictedArea newRestrictedArea)
    {
        var coordinates = newRestrictedArea.Perimeter;
        if (Geometrics.IsValidPolygon(coordinates) is false) return BadRequest("Invalid polygon");
        var savedRestrictedAreas = _context.RestrictedAreas.Include(l=>l.Perimeter).ToList();
        foreach(var savedRestrictedArea in savedRestrictedAreas)
            foreach(var coordinate in newRestrictedArea.Perimeter)
                if (Geometrics.IsInside(coordinate, savedRestrictedArea.Perimeter))
                    return BadRequest($"This area is already marked as a restricted area ({savedRestrictedArea.Name})");
        _context.RestrictedAreas.Add(newRestrictedArea);
        _context.SaveChanges();
        return Ok(newRestrictedArea);
    }
    
    [HttpGet("SearchLandByCoordinate")]
    public ActionResult<RestrictedArea> SearchAreaByCoordinate(Coordinate coordinate)
    {
        var restrictedAreas = _context.RestrictedAreas.ToList();
        if(restrictedAreas.IsNullOrEmpty()) return BadRequest("No restrictedArea found");
        foreach(var restrictedArea in restrictedAreas)
            if (Geometrics.IsInside(coordinate, restrictedArea.Perimeter))
                return Ok(restrictedArea);
        return BadRequest("No restricted area found");
    }
    
}