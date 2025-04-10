using Famyok.DomainLayer.Models.Http.Identity.Requests;
using Famyok.DomainLayer.Models.Http.Identity.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Famyok.IdentityProvider.Controllers;

[ApiController]
[Route("[controller]")]
public class DummyIdentityController : ControllerBase
{
    private readonly ILogger<DummyIdentityController> _logger;

    public DummyIdentityController(ILogger<DummyIdentityController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public DummyIdentityResponse GetDummy(DummyIdentityRequest dto)
    {
        return new DummyIdentityResponse();
    }

    [HttpPost]
    public DummyIdentityResponse PostDummy(DummyIdentityRequest dto)
    {
        return new DummyIdentityResponse();
    }

    [HttpPut]
    public DummyIdentityResponse PutDummy(DummyIdentityRequest dto)
    {
        return new DummyIdentityResponse();
    }

    [HttpDelete]
    public DummyIdentityResponse DeleteDummy(DummyIdentityRequest dto)
    {
        return new DummyIdentityResponse();
    }
}