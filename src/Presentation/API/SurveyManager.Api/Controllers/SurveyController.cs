using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SurveyManager.Api.Controllers;


[Route("[controller]s")]
public class SurveyController : ApiController
{
    [HttpGet]
    public IActionResult ListSurveys()
    {
        return Ok(Array.Empty<string>());
    }
}