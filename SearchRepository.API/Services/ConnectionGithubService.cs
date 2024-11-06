using SearchRepository.API.Interfaces;

namespace SearchRepository.API.Services;

public class ConnectionGitHubService : IConnectionGitHubService
{
    public async virtual Task<string> GetGitHubRepositories(string subject)
    {
        HttpClient httpClient = new HttpClient();
        
        httpClient.DefaultRequestHeaders.Add("User-Agent", "YourAppName/1.0");
        var response = await httpClient.GetStringAsync($"https://api.github.com/search/repositories?q={subject}");
        var jsonString = response.ToString();

        return  jsonString;
    }
}