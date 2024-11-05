using System.Text.Json.Nodes;
using System.Xml.Linq;
using Azure;
using Microsoft.VisualBasic;

using Newtonsoft.Json.Linq;
using SearchRepository.API.Interfaces;

namespace SearchRepository.API.Services;

public class  ConvertToJsonService: IConvertToJsonService
{

    public virtual  List<Repository> ParseJson (string jsonString)
    {   

        List<Repository> repositories = new List<Repository>();

        var jsonRespone = JObject.Parse(jsonString);

        var jsonItem = jsonRespone["items"];

        foreach(var item in jsonItem)
        {
            repositories.Add(new Repository
            {
                Name = item["name"].ToString(),
                Author = item["login"].ToString(),
                Stars = Convert.ToInt32(item["stargazers_count"]),
                Watches = Convert.ToInt32(item["watchers_count"]),
                Url = item["html_url"].ToString()
            });

        }
        return repositories;
    }

}