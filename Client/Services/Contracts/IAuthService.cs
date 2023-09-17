namespace VehicleSpeedControlSystem.Client.Services.Contracts
{
    public interface IAuthService
    {
        Task<string> LogIn(UserLoginDto user);
        Task<string> LogOut();
        Task<bool> IsInRole(string role);
        Task<List<string>> WhatRoles();
        Task InitialiseAuthState();
        Task<int> GetId();
        Task<User> CreateAccount(UserDto user);
    }
}
