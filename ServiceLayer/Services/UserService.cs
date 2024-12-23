using ServiceLayer.Models;
using System.Net.Http.Json;


namespace ServiceLayer.Services
{
    public class UserService
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress = "http://localhost:5103/api/";

        public UserService()
        {
            _client = new() { BaseAddress = new Uri(_baseAddress) };
        }
        public async Task<User?> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            try
            {
                var users = await _client.GetFromJsonAsync<IEnumerable<User>>("Users");
                return users.FirstOrDefault(u => u.UserLogin == login && u.UserPassword == password);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка получения данных пользователя при выполнении запроса к API: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string?> GetUserFullNameWithOrderIdAsync(int? orderId)
        {
            try
            {
                var response = await _client.GetAsync($"Orders/{orderId}");
                response.EnsureSuccessStatusCode();
                var order = await response.Content.ReadFromJsonAsync<Order>();

                if (order.UserId != null)
                {
                    response = await _client.GetAsync($"Users/{order?.UserId}");
                    response.EnsureSuccessStatusCode();
                    var user = await response.Content.ReadFromJsonAsync<User>();

                    return $"{user.UserSurname} {user.UserName} {user.UserPatronymic}";
                }

                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка получения данных пользователя при выполнении запроса к API: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
