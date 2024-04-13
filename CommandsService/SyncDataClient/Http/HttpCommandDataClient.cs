
using System.Net.Http.Headers;
// using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CommandsService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
  public class HttpCommandDataClient : ICommandDataClient
  {
    private readonly HttpClient _httpClient;

    public HttpCommandDataClient(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
      var httpContent = new StringContent(JsonSerializer.Serialize(platform),
      Encoding.UTF8,
      MediaTypeHeaderValue.Parse("application/json")
      );

      var response = await _httpClient.PostAsync("", httpContent);
    }
  }
}