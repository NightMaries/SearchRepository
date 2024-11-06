namespace SearchRepository.API.Interfaces;

public  interface IConvertToJsonService
{
    public Task<List<Repository>> ParseJson(string jsonString);
}