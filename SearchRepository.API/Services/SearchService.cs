namespace SearchRepository.API.Services;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SearchRepository.API.Services;

public class SearchService: ISearchService
{
    private readonly SearchRepositoryContext _db;

    public SearchService(SearchRepositoryContext db)
    {
        _db = db;
    }

    public async Task<JsonRequest> AddSearch(JsonRequest jsonRequest)
    {
        if (jsonRequest.Subject != "")
        {
            var github = new ConnectionGitHubService();
            jsonRequest.JsonString = await github.GetGitHubRepositories(jsonRequest.Subject);
        }

        try 
        {
                await _db.jsonRequests.AddAsync(jsonRequest);
                await _db.SaveChangesAsync();
        }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        return jsonRequest;


    }

    public async Task DeleteSearch(int id)
    {
        var JsonRequest = await _db.jsonRequests.Where(j => j.Id == id).FirstOrDefaultAsync();
        _db.Remove(JsonRequest);
        try
        {
            await _db.SaveChangesAsync();
        }
        catch(Exception ex)
        {
            throw new Exception($"Ошибка: {ex.Message}");
        }
    }

    public async Task<List<Repository>> GetSearch(string subject)
    {

        string jsonString = "";

        var request = await _db.jsonRequests.Where(j => j.Subject == subject).FirstOrDefaultAsync();

        if (request == null)
        {
            var github = new ConnectionGitHubService();
            jsonString = await github.GetGitHubRepositories(subject);
            AddSearch(new JsonRequest() { Subject = subject });
        }
        else 
        {
            jsonString = request.JsonString;
        }
        var convert = new ConvertToJsonService();
        var list = await convert.ParseJson(jsonString);
        foreach (var item in list)
        {
           await _db.Repositories.AddAsync(item);
        }
        try
        {
            await _db.SaveChangesAsync();

        }
        catch(Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }

        return list;
    }

}