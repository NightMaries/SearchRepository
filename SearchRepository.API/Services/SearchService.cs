namespace SearchRepository.API.Services;

using Microsoft.EntityFrameworkCore;
using SearchRepository.API.Services;

public class SearchService: ISearchService
{
    private readonly SearchRepositoryContext _db;

    public SearchService(SearchRepositoryContext db)
    {
        _db = db;
    }

    public async Task AddSearch(JsonRequest jsonRequest)
    {
        if(jsonRequest.Subject != "")
        {

            _db.jsonRequests.Add(jsonRequest);
            try
            {
                _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception($"Ошибка: {ex.Message}");
            }
        }

    }

    public async Task DeleteSearch(int id)
    {
        var JsonRequest = _db.jsonRequests.Where(j => j.Id == id).FirstOrDefaultAsync();
        _db.Remove(JsonRequest);
        try
        {
            _db.SaveChanges();
        }
        catch(Exception ex)
        {
            throw new Exception($"Ошибка: {ex.Message}");
        }

    }

    public async Task<List<Repository>> GetSearch(string subject)
    {
        string jsonString = "";

        var request = _db.jsonRequests.Where(j => j.Subject == subject).FirstOrDefaultAsync();

        if( await request == null)
        {
            var github = new ConnectionGitHubService();
            jsonString = await github.GetGitHubRepositories(subject);
           

           AddSearch(new JsonRequest()
           {    
                Subject = subject,
                JsonString = jsonString
           });
        }

        var convert = new ConvertToJsonService();
        return convert.ParseJson(jsonString);
    }

}