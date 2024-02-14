﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

using NadinSoft.Application.Contracts;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NadinSoft.Api.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApiSettings _apiSettings;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IOptionsSnapshot<ApiSettings> options)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _apiSettings = options.Value;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = new IdentityUser
        {
            UserName = request.UserName,
            Email = request.Email,
            PhoneNumber = request.Mobile,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(new { Error = result.Errors.Select(x => x.Description) });
        }

        return Created();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password,
                                                                            isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Unauthorized(new SignInResponseDto
            {
                IsSuccessful = false,
                ErrorMessage = "Invalid authentication"
            });
        }

        var user = await _userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return Unauthorized(new SignInResponseDto
            {
                IsSuccessful = false,
                ErrorMessage = "Invalid authentication"
            });
        }

        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);

        var tokenOptions = new JwtSecurityToken(issuer: _apiSettings.ValidIssuer, audience: _apiSettings.ValidAudience,
            claims: claims, expires: DateTime.UtcNow.AddDays(3),
            signingCredentials: signingCredentials);

        string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return Ok(new SignInResponseDto { 
        
            IsSuccessful = true,
            Email = user.Email,
            Mobile = user.PhoneNumber,
            UserName = user.UserName,
            Token = token
        });
    }

    private SigningCredentials GetSigningCredentials()
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha512);
    }

    private List<Claim> GetClaims(IdentityUser user)
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email)
        };
    }
}