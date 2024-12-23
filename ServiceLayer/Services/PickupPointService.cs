using ServiceLayer.Models;
using System.Net.Http.Json;

namespace ServiceLayer.Services
{
    public class PickupPointService
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress = "http://localhost:5103/api/";

        public PickupPointService()
        {
            _client = new() { BaseAddress = new Uri(_baseAddress) };
        }

        public async Task<List<PickupPoint>?> GetPickupPointsAsync()//получение списка пунктов выдачи из БД
        {
            try
            {
                return await _client.GetFromJsonAsync<List<PickupPoint>>("PickupPoints");
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка получения пунктов доставки при выполнении запроса к API: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
