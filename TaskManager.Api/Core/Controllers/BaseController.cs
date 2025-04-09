using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.Core.Helpers.ExtensionMethods;
using TaskManager.Api.Core.Models;
using TaskManager.Api.Core.Services.Interfaces;

namespace TaskManager.Api.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<T>(IBaseService<T> service) : ControllerBase where T : BaseModel
{
    private readonly IBaseService<T> _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int page, [FromQuery] int pageSize)
    {
        return (await _service.GetAllAsync()).ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return (await _service.GetByIdAsync(id)).ToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] T model)
    {
        return (await _service.CreateAsync(model)).ToActionResult();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] T model)
    {
        return (await _service.UpdateAsync(id, model)).ToActionResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteByIdAsync([FromRoute] int id)
    {
        return (await _service.DeleteByIdAsync(id)).ToActionResult();
    }
}
