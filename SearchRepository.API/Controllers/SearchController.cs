using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SearchRepository.API.Services;

namespace SearchRepository.API.Controllers;

[Route ("api/")]
[ApiController]
public class SearchController: ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }
    [HttpPost]
    public ActionResult CreateSearch(JsonRequest jsonRequest)
    {
        try
        {
            _searchService.AddSearch(jsonRequest);
            return Created("http://localhost:7147/api", jsonRequest);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public ActionResult DeleteSearch(int id)
    {
        try
        {   
            _searchService.DeleteSearch(id);
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public ActionResult GetSearchs(string subject)
    {   
        return Ok(_searchService.GetSearch(subject));
    }
}