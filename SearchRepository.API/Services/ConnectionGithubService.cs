using SearchRepository.API.Interfaces;

namespace SearchRepository.API.Services;

public class ConnectionGitHubService : IConnectionGitHubService
{
    public virtual async Task<string> GetGitHubRepositories(string subject)
    {
        HttpClient httpClient = new HttpClient();
        
        string request =$"https://api.github.com/search/repositories?={subject}";
        
        httpClient.DefaultRequestHeaders.Add("User-Agent",".Net Foudation Repository Reporter");
        var response = httpClient.GetStringAsync(request);
        var jsonString = await response;

        return jsonString;
    }
}