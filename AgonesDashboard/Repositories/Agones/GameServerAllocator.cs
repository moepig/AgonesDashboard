using AgonesDashboard.Models.Agones;
using System.Text;
using System.Text.Json;

namespace AgonesDashboard.Repositories.Agones
{
    public class GameServerAllocator : IGameServerAllocator
    {

        private readonly ILogger<GameServerAllocator> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AgonesUri _agonesUri;


        public GameServerAllocator(ILogger<GameServerAllocator> logger, IHttpClientFactory httpClientFactory, AgonesUri agonesUri)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _agonesUri = agonesUri;
        }

        public async Task<GameServerAllocationResponse?> GameServerAllocation(string k8sNamespace)
        {
            var requestBody = new GameServerAllocationRequest
            {
                Namespace = k8sNamespace
            };
            var requestBodyJson = JsonSerializer.Serialize(requestBody);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _agonesUri.BaseUri);
            httpRequest.Headers.Add("Content-Type", "application/json");
            httpRequest.Content = new StringContent(requestBodyJson, Encoding.UTF8);

            var httpClient = _httpClientFactory.CreateClient();
            try
            {
                var httpResponse = await httpClient.SendAsync(httpRequest);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var deserialized = JsonSerializer.Deserialize<GameServerAllocationResponse>(httpResponse.Content.ReadAsStream());
                    return deserialized;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }

            return null;
        }
    }
}
