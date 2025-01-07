using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Contracts;
using System.Text.Json;

namespace LojaVirtual.Web.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/categorias/";

    public CategoriaService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<CategoriaViewModel>> GetTodasCategorias()
    {
        var client = _httpClientFactory.CreateClient("ProdutoApi");
        IEnumerable<CategoriaViewModel> categorias;

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode) 
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                categorias = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return categorias;
    }
}
