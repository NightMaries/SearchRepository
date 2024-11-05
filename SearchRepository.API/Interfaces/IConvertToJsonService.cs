namespace SearchRepository.API.Interfaces;

public  interface IConvertToJsonService
{
    public List<Repository> ParseJson(string jsonString);
}