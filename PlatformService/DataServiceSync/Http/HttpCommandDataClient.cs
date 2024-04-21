
using System.Net.Http.Headers;
// using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using PlatformService.DTO;

namespace PlatformService.DataServiceSyncs.Http
{
  public class HttpCommandDataClient : ICommandDataClient
  {
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
    {
      _httpClient = httpClient;
      _config = config;
    }

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
      var httpContent = new StringContent(JsonSerializer.Serialize(platform),
      Encoding.UTF8,
      MediaTypeHeaderValue.Parse("application/json; charset=utf-8")
      );
      string ep = _config["CommandService"] ?? "no endpoint";

      var response = await _httpClient.PostAsync(ep, httpContent);
      if (response.IsSuccessStatusCode)
      {
        Console.WriteLine("--> Sync HTTP POST to CommandService was OK!");
      }
      else
      {
        Console.WriteLine("--> Sync POST to CommandService was NOT OK!");
      }
    }
  }
}