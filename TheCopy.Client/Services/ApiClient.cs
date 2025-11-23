
using Blazored.LocalStorage;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheCopy.Shared.DataTransferObjects;

namespace TheCopy.Client.Services;

public class ApiClient
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _localStorage;

    public ApiClient(HttpClient http, ILocalStorageService localStorage)
    {
        _http = http;
        _localStorage = localStorage;
    }

    public async Task<AuthResponseDto> Login(LoginRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("/api/auth/login", request);
        return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
    }

    public async Task<AuthResponseDto> Register(RegisterRequestDto request)
    {
        var response = await _http.PostAsJsonAsync("/api/auth/register", request);
        return await response.Content.ReadFromJsonAsync<AuthResponseDto>();
    }

    public async Task<List<ProjectDto>> GetProjects()
    {
        return await _http.GetFromJsonAsync<List<ProjectDto>>("/api/projects");
    }
}
