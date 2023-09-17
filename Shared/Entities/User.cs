using System.Data;

namespace VehicleSpeedControlSystem.Shared.Entities;
//Used to keep track of visitors to the site who do not own land
public class User : BaseUserEntity {
    public string Email { get; set; }
    public int Id { get; set; }
    public string? Token { get; set; } = null;
    public Role Role { get; set; } = Role.General;
    public string Password { get; set; }
}

public class UserDto : User
{
    public readonly int? Id = null;
    public string? Token { get; set; } = null;
    public Role Role { get; set; } = Role.General;
    public string Password { get; set; }

}
public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public enum Role
{
    Admin,
    General,
}