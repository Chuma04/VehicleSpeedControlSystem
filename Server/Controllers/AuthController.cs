namespace VehicleSpeedControlSystem.Server.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _dateContext;
        private readonly IConfiguration _configuration;
        private List<User> users = new List<User>();
        public AuthController(DataContext dataContext, IConfiguration configuration)
        {
            _dateContext = dataContext;
            _configuration = configuration;
            List<User> baseUsers = new List<User>();
            //foreach (var item in _dateContext.Teachers) users.Add(item as BaseUserEntity);
            //foreach (var item in _dateContext.Parents) users.Add(item as BaseUserEntity);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDto user)
        {
            if (user == null) return BadRequest("You need to enter valid information");
            var result = _dateContext.Users.FirstOrDefault(x => x.Email.ToLower().Trim().Equals(user.Email.ToLower().Trim()));
            if (result == null) return BadRequest("Invalid Email");
            if (user.Password == null) return BadRequest("Invalid Password");
            if(!user.Password.Equals(result.Password)) return BadRequest("Incorrect Password");
            return Ok(CreateToken(result));
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto user)
        {
            if (user == null) return BadRequest("You need to enter valid information");
            var result = _dateContext.Users.FirstOrDefault(x => x.Email.ToLower().Trim().Equals(user.Email.ToLower().Trim()));
            if (result != null) return BadRequest("Email already in use");
            if (user.Password == null) return BadRequest("Invalid Password");
            var newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };
            _dateContext.Users.Add(newUser);
            await _dateContext.SaveChangesAsync();
            return Ok(newUser);
        }

        public static string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString())

            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("DefaultTokenis the best defualt tocken there is")); //MUST BE CHANGED 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine("completed");
            return jwt;
        }
    }
}
