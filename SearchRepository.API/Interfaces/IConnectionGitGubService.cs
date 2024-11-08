namespace SearchRepository.API.Interfaces;

public interface IConnectionGitHubService
{
    public Task<string> GetGitHubRepositories(string subject);
}