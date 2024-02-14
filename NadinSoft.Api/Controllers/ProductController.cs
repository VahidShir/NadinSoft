using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using NadinSoft.Application.Commands;
using NadinSoft.Application.Contracts;
using NadinSoft.Application.Queries;
using NadinSoft.Domain;

using System.Net;
using System.Security.Claims;

namespace NadinSoft.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly UserManager<IdentityUser> _userManager;
    private Guid UserId;

    public ProductController(IMediator mediator, UserManager<IdentityUser> userManager)
    {
        _mediator = mediator;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var products = await _mediator.Send(new GetProductsListQuery());

            return Ok(products);
        }
        catch (Exception e) when (e is NadinSoftBusinessException || e is ArgumentException)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var product = await _mediator.Send(new GetProductByIdQuery());

            if (product is null)
                return NotFound();

            return Ok(product);
        }
        catch (Exception e) when (e is NadinSoftBusinessException || e is ArgumentException)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(ProductCreateDto input)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        try
        {
            var product = await _mediator.Send(new CreateProductCommand(createdBy: User.Identity.Name, input));

            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = product.Id }, product);
        }
        catch (Exception e) when (e is NadinSoftBusinessException || e is ArgumentException)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _mediator.Send(new DeleteProductCommand(userName: User.Identity.Name, id));

            return Ok();
        }
        catch (NadinSoftForbiddenException e)
        {
            return Unauthorized(e.Message);
        }
        catch (Exception e) when (e is NadinSoftBusinessException || e is ArgumentException)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
        }
    }
}