using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Contracts;
using System.Text.Json;

namespace LojaVirtual.Web.Services;

public class ProdutoService : IProdutoService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndpoint = "/api/produtos/";
    private readonly JsonSerializerOptions _options;
    private ProdutoViewModel produtoViewModel;
    private IEnumerable<ProdutoViewModel> produtosViewModel;

    public ProdutoService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProdutoViewModel>> GetTodosProdutos()
    {
        var client = _httpClientFactory.CreateClient("ProdutoApi");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode) 
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtosViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _options);

            
            }
            else
            {
                return null;
            }
        }
        return produtosViewModel;
    }

    public async Task<ProdutoViewModel> ProcurarProdutoById(int id)
    {
        var client = _httpClientFactory.CreateClient("ProdutoApi");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtoViewModel = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);


            }
            else
            {
                return null;
            }
        }
        return produtoViewModel;
    }

    public async Task<ProdutoViewModel> CreateProduto(ProdutoViewModel produtoViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProdutoApi");

        StringContent content = new(JsonSerializer.Serialize(produtoViewModel),
                                                  System.Text.Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtoViewModel = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);


            }
            else
            {
                return null;
            }
        }
        return produtoViewModel;
    }
    public async Task<ProdutoViewModel> UpdateProduto(ProdutoViewModel produtoViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProdutoApi");

        ProdutoViewModel produtoUpdated = new ProdutoViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, produtoUpdated))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                produtoUpdated = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtoUpdated;
    }
    public async Task<bool> DeleteProdutoById(int id)
    {
        var client = _httpClientFactory.CreateClient("ProdutoApi");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

}
