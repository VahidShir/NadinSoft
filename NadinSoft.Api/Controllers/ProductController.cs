using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using NadinSoft.Application.Contracts;
using NadinSoft.Domain;

using System.Net;

namespace NadinSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);
        }
        catch (NadinSoftBusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var product = await _productService.GetAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }
        catch (NadinSoftBusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto input)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var product = await _productService.CreateAsync(input);

            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = product.Id }, product);
        }
        catch (NadinSoftBusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _productService.DeleteAsync(id);

            return Ok();
        }
        catch (NadinSoftBusinessException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}