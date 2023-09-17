namespace VehicleSpeedControlSystem.Client.Services;

    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        public readonly HttpClient _httpClient;
        public BaseService(HttpClient httpClient) => _httpClient = httpClient;

    public List<T> Objects { get; set; } = new();
        public T Object { get; set; }=new();

        public virtual async Task<T> Create(T dto)
        {
            var request = await _httpClient.PostAsJsonAsync<T>($"api/{typeof(T).Name}", dto);
            if (!request.IsSuccessStatusCode) throw new Exception($"Creating {typeof(T).Name} returned {request.StatusCode}");
            var response = await request.Content.ReadFromJsonAsync<T>();
            if (response is null) throw new Exception($"Creating {typeof(T).Name} returned is null");
            Object = response;
            Objects.Add(Object);
            return response;
        }

        public virtual async Task<string> Delete(int id)
        {
            var request = await _httpClient.DeleteAsync($"api/{typeof(T).Name}/{id}");
            if (!request.IsSuccessStatusCode) throw new Exception(request.ReasonPhrase);
            var response = await request.Content.ReadFromJsonAsync<string>();
            if (response == null) throw new Exception("No object was deleted");
            Objects.Remove(Objects.First(o=>o.Id==id));
            return response;
        }

        public virtual async Task<List<T>> Get(bool forceRefresh)
        {
            if (Objects is not null)
            {
                if (forceRefresh) Objects.Clear();
                if(Objects.Count > 0) return Objects; // TODO: Add a check for if the objects are up to date (last updated
            }
            var request = await _httpClient.GetAsync($"api/{typeof(T).Name}");
            if (!request.IsSuccessStatusCode) throw new Exception(request.ReasonPhrase);
            var objects = request.Content.ReadFromJsonAsync<List<T>>().Result;
            if (objects is null) throw new Exception("No objects were found");
            Objects = objects;
            return objects;
        }

          public virtual async Task<T> GetByID(int id)
        {
            if(Object.Id == id) return Object; // TODO: Add a check for if the object is up to date (last updated
            if(Objects.Count > 0) 
            {
                var obj = Objects.FirstOrDefault(o=>o.Id==id); 
                if(obj != null) return obj; // TODO: Add a check for if the object is up to date (last updated
            } // TODO: Add a check for if the object is up to date (last updated
            var request = await _httpClient.GetAsync($"api/{typeof(T).Name}/{id}");
            if (!request.IsSuccessStatusCode) throw new Exception(request.ReasonPhrase);
            var response = await request.Content.ReadFromJsonAsync<T>();
            if (response is null) throw new Exception("No object was found");
            Object = response;
            return response;
        }

          

          public virtual async Task<T> Update(T t)
        {
            var request = await _httpClient.PutAsJsonAsync<T>($"api/{typeof(T).Name}", t);
            if (!request.IsSuccessStatusCode) throw new Exception(request.ReasonPhrase);
            var response = await request.Content.ReadFromJsonAsync<T>();
            if (response is null) throw new Exception("No object was updated");
            //Update the local data
            var obj = Objects.FirstOrDefault(o => o.Id == response.Id);
            if (obj != null) Objects.Remove(obj);
            Objects.Add(response);
            //return response
            return response;
        }
    }
