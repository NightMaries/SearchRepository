namespace SearchRepository.API.Services;

public interface ISearchService
{

     public Task<JsonRequest> AddSearch(JsonRequest jsonRequest);
     public Task DeleteSearch(int id);
      
     public  Task<List<Repository>> GetSearch(string subject);
}