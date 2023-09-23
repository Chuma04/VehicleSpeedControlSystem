
namespace VehicleSpeedControlSystem.Server.Controllers;

public class  CoordinatesController : BaseController<Coordinate>
{
    public CoordinatesController(DataContext context) : base(context){}
    
    [HttpPost("check")]
    public ActionResult<RestrictedArea> CheckCoordinates(Coordinate coordinates)
    {
        var restrictedAreas = _context.RestrictedAreas.Include(land => land.Perimeter).ToList();
        if(restrictedAreas.IsNullOrEmpty()) return BadRequest("No restrictedArea found");
        foreach(var restrictedArea in restrictedAreas)
            if (Geometrics.IsInside(coordinates, restrictedArea.Perimeter))
                return Ok(restrictedArea);
        return BadRequest("Not in any restricted area");
    }
    

    
}

// public class  AdminController : BaseController<Admin>
// {
//     public AdminController(DataContext context) : base(context){}
// }
// public class  LandlordController : BaseController<Landlord>
// {
//     public LandlordController(DataContext context) : base(context){}
// }
// public class  PurchaseController : BaseController<Purchase>
// {
//     public PurchaseController(DataContext context) : base(context){}
// }
// public class  TitleDeedController : BaseController<TitleDeed>
// {
//     public TitleDeedController(DataContext context) : base(context){}
// }
// public class  OwnershipController : BaseController<Ownership>
// {
//     public OwnershipController(DataContext context) : base(context){}
// }

public class  UserController : BaseController<User>
{
    public UserController(DataContext context) : base(context){}
}
// public class  ReportController : BaseController<Report>
// {
//     public ReportController(DataContext context) : base(context){}
// }
//
// public class DisputeController : BaseController<Dispute>
// {
//     public DisputeController(DataContext context) : base(context) { }
// }
