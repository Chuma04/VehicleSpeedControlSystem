namespace VehicleSpeedControlSystem.Client.Services.Contracts;

public interface IBaseService<T> where T : BaseEntity
{
    List<T> Objects { get; set; }
    T Object { get; set; }
    Task<List<T>> Get(bool forceRefresh = true);
    Task<T> GetByID(int id);
    Task<T> Create(T dto);
    Task<T> Update(T t);
    Task<string> Delete(int id);
}