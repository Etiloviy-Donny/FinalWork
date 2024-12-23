using ServiceLayer.DTOs;
using ServiceLayer.Models;
using System.Net.Http.Json;
using System.Net.Http;

namespace ServiceLayer.Services
{
    public class ProductService
    {
        private readonly HttpClient _client;
        private readonly string _baseAddress = "http://localhost:5103/api/";

        public ProductService()
        {
            _client = new() { BaseAddress = new Uri(_baseAddress) };
        }
        public async Task<List<ProductDTO>> GetProductsAsync(string subs, string? sortMethod, decimal minCost, decimal? maxCost, string manuf)//получение списка продуктов из БД с фильтрацией
        {
            try
            {
                var productList = await _client.GetFromJsonAsync<IEnumerable<ProductDTO>>("Products");
                productList = productList.Where(p => p.TotalCost >= minCost).Where(p => p.ProductName.ToLower().Contains(subs.ToLower()));

                if (maxCost != null)
                {
                    productList = productList.Where(p => p.TotalCost <= maxCost);
                }

                if (!string.IsNullOrEmpty(manuf))
                {
                    productList = productList.Where(p => p.ProductManufacturer == manuf);
                }

                if (sortMethod == "asc")
                {
                    return productList.OrderBy(x => x.TotalCost).ToList();
                }
                else if (sortMethod == "desc")
                {
                    return productList.OrderByDescending(x => x.TotalCost).ToList();
                }
                return productList.ToList();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка получения всех товаров при выполнении запроса к API: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetProductsCountAsync()
        {
            try
            {
                var allProducts = await GetProductsAsync("", "", 0, null, "");
                return allProducts.Count;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка получения количества товаров при выполнении запроса к API: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<string>> GetManufacturersAsync()
        {
            try
            {
                var manufacterers = await _client.GetFromJsonAsync<IEnumerable<Manufacturer>>("Manufacturers");
                List<string> manufacturers = ["Все производители", .. manufacterers.Select(p => p.Name).Distinct().ToList()];
                return manufacturers;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка получения производителей при выполнении запроса к API: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GetProductNameByArticleAsync(string article)
        {
            try
            {
                var manufacterers = await _client.GetFromJsonAsync<IEnumerable<Product>>("Products");
                return manufacterers.Where(p => p.ProductArticleNumber == article).Select(p => p.ProductName).FirstOrDefault();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("Ошибка получения данных товара при выполнении запроса к API: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
