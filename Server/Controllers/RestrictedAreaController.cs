namespace VehicleSpeedControlSystem.Server.Controllers;

public class  RestrictedAreaController : BaseController<RestrictedArea>
{
    public RestrictedAreaController(DataContext context) : base(context){}

    [HttpGet]
    public override ActionResult<List<RestrictedArea>> Get() => _context.RestrictedAreas.Include(land=>land.Perimeter).ToList() ?? new() ;
    
    // override the delete method to delete the coordinates and the restricted area
    [HttpDelete("{Id}")]
    public override ActionResult Delete(int Id)
    {
        var restrictedArea = _context.RestrictedAreas
            .Include(area => area.Perimeter)
            .FirstOrDefault(x=>x.Id==Id);
        if (restrictedArea is null) return BadRequest($"Area was not found");
        
        _context.Coordinates.RemoveRange(restrictedArea.Perimeter);
        _context.RestrictedAreas.Remove(restrictedArea);
        
        _context.SaveChanges();
        
        return Ok("Area deleted successfully");
    }
    
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
    
    [HttpPost("SearchLandByCoordinate")]
    public ActionResult<RestrictedArea> SearchAreaByCoordinate(Coordinate coordinate)
    {
        var restrictedAreas = _context.RestrictedAreas.Include(x=>x.Perimeter).ToList();
        if(restrictedAreas.IsNullOrEmpty()) return BadRequest("No restricted area found");
        foreach (var restrictedArea in restrictedAreas)
        {
            try
            {
                if (Geometrics.IsInside(coordinate, restrictedArea.Perimeter))
                    return Ok(restrictedArea.SpeedLimit);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        return BadRequest("Not in any restricted area");
    }
    
}