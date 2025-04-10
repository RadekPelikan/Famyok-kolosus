using Famyok.DomainLayer.Models.Http.Api.Requests;
using Famyok.DomainLayer.Models.Http.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Famyok.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyApiController : ControllerBase
{
    private readonly ILogger<DummyApiController> _logger;

    public DummyApiController(ILogger<DummyApiController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public DummyApiResponse GetDummy(DummyApiRequest dto)
    {
        return new DummyApiResponse();
    }

    [HttpPost]
    public DummyApiResponse PostDummy(DummyApiRequest dto)
    {
        return new DummyApiResponse();
    }

    [HttpPut]
    public DummyApiResponse PutDummy(DummyApiRequest dto)
    {
        return new DummyApiResponse();
    }

    [HttpDelete]
    public DummyApiResponse DeleteDummy(DummyApiRequest dto)
    {
        return new DummyApiResponse();
    }
}