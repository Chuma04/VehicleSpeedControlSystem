namespace VehicleSpeedControlSystem.Client.Services;

public class AreaService : BaseService<RestrictedArea> , IAreaService
{
    public AreaService(HttpClient httpClient) : base(httpClient)
    {
    }

}

public class CoordinateService : BaseService<Coordinate> , ICoordinateService
{
    public CoordinateService(HttpClient httpClient) : base(httpClient)
    {
    }

}

// public class AdminService : BaseService<Admin> , IAdminService
// {
//     public AdminService(HttpClient httpClient) : base(httpClient)
//     {
//     }
// }
//
// public class LandlordService : BaseService<Landlord> , ILandlordService
// {
//     public LandlordService(HttpClient httpClient) : base(httpClient)
//     {
//     }
// }

// public class PurchaseService : BaseService<Purchase> , IPurchaseService
// {
//     public PurchaseService(HttpClient httpClient) : base(httpClient)
//     {
//     }
// }

// public class TitleDeedService : BaseService<TitleDeed> , ITitleDeedService
// {
//     public TitleDeedService(HttpClient httpClient) : base(httpClient)
//     {
//     }
// }
//
// public class OwnershipService : BaseService<Ownership> , IOwnershipService
// {
//     public OwnershipService(HttpClient httpClient) : base(httpClient)
//     {
//     }
// }

public class UserService : BaseService<User> , IUserService
{
    public UserService(HttpClient httpClient) : base(httpClient)
    {
    }
}

// public class ReportService : BaseService<Report> , IReportService
// {
//     public ReportService(HttpClient httpClient) : base(httpClient)
//     {
//     }
// }
//
// public class DisputeService : BaseService<Dispute> , IDisputeService
// {
//     public DisputeService(HttpClient httpClient) : base(httpClient)
//     {
//     }
// }

