
namespace VehicleSpeedControlSystem.Client.Services
{

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private AuthenticationState? _authenticationState { get; set; }
        private ClaimsPrincipal? _user { get; set; }

        public AuthService(HttpClient httpClient,
            NavigationManager navigationManager,
            AuthenticationStateProvider authenticationStateProvider,
            ILocalStorageService localStorage
            )
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task InitialiseAuthState()
        {
            _authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _user = _authenticationState.User;

        }
        public async Task<bool> IsInRole(string role)
        {
            await InitialiseAuthState();
            //check if current authenticated user is in the role "role"
            if (!_user.Identity.IsAuthenticated)
            {
                return false;
            }
            return _user.IsInRole(role);
        }


        public async Task<string> LogIn(UserLoginDto user)
        {
            //LogOut();
            if (user == null) throw new ArgumentNullException("You need to enter valid information");
            var result = await _httpClient.PostAsJsonAsync("api/auth/login", user);
            if (!result.IsSuccessStatusCode) throw new Exception(await result.Content.ReadAsStringAsync());
            var response = await result.Content.ReadAsStringAsync();
            Console.Write($"response : {response}");
            if (response is null) throw new Exception(" Authorization token not recieved from server");
            await _localStorage.SetItemAsStringAsync("token", response);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("/");
            return response;
        }

        public async Task<List<string>> WhatRoles()
        {
            await InitialiseAuthState();
            if (_user is null) return null;
            List<string> roles = new();
            //loop through all the roles of the _user
            foreach (var role in _user.Claims)
            {
                roles.Add(role.Value);
            }
            return roles;

        }

        public async Task<int> GetId()
        {

            await InitialiseAuthState();
            var groupClaim = _user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);

            if (groupClaim != null)
            {
                string groupId = groupClaim.Value;
                Console.WriteLine(groupId);
                // convert grouptId to int and return in 
                return int.Parse(groupId);
            }
            else throw new Exception("AS: Invalid user id");
            ;
        }

        // TODO: make a method that returns the user id

        public async Task<string> LogOut()
        {
            await _localStorage.RemoveItemAsync("token");
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("/signin");
            return "/";
        }

        public async Task<User> CreateAccount(UserDto u)
        {
            var result = await _httpClient.PostAsJsonAsync("api/auth/register", u);
            if (!result.IsSuccessStatusCode) throw new Exception(result.Content.ReadAsStringAsync().Result);
            var response = await result.Content.ReadAsStringAsync();
            Console.Write($"response : {response}");
            if (response is null) throw new Exception(" Authorization token not recieved from server");
            await _localStorage.SetItemAsStringAsync("token", response);
            await _authenticationStateProvider.GetAuthenticationStateAsync();
            _navigationManager.NavigateTo("/");
            return JsonSerializer.Deserialize<User>(response);
        }
    }
}
