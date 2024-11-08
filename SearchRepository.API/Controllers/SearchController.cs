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
    public async Task<ActionResult> CreateSearch(JsonRequest jsonRequest)
    {
        try
        {
            await _searchService.AddSearch(jsonRequest);
            return Created("http://localhost:7147/api", jsonRequest);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteSearch(int id)
    {
        try
        {   
            await _searchService.DeleteSearch(id);
            return NoContent();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<ActionResult<JsonRequest>> GetSearchs(string subject)
    {
        var search = await _searchService.GetSearch(subject);
        if (search == null)
            return BadRequest();
        else
            return Ok(search);

    }
}