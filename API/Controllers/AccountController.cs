using System;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.DTOs;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Authorize]
public class AccountController : BaseApiController
{
    private readonly DataContext _dataContext;
    private readonly ITokenService _tokenService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="tokenService"></param>
    public AccountController(DataContext dataContext, ITokenService tokenService)
    {
        _dataContext = dataContext;
        _tokenService = tokenService;

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [HttpPost("register")]//account/register
    public async Task<ActionResult<AppUser>> Register(RegisterDto register)
    {

        if (await UserIsNullOrEmpty(register.Username) ||
     await UserIsNullOrEmpty(register.Password))
        {
            return BadRequest("UserName/Password is null or empty");
        }

        if (!await UserExists(register.Username))
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                UserName = register.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmac.Key
            };
            //await _dataContext.Users.ToListAsync();
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();

            return new AppUser
            {

                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt
            };
        }

        return BadRequest("This user already exists");
    }
    // Post: api/<ValuesController> // api/values  
    //https://localhost:5006/api/account/Login?username=sam&password=password
    //https://localhost:5006/api/account/login
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(user => user.UserName == loginDto.Username);

        if (user == null) return Unauthorized("Invalid Username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password ");

        }

        return new UserDto
        {

            Username = user.UserName,
            Token =  _tokenService.CreateToken(user)
             // _tokenService.CreateToken(user)
        };
    }




    #region private methon zone

    ///

    private async Task<bool> UserExists(string username)
    {

        return await _dataContext.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
    /*      private async Task<bool> UserExists(LoginDto userLogin)
        {
            using var hmac = new HMACSHA512();

            return await _dataContext.Users.SingleOfDefaultAsync(user => user.UserName == userLogin.UserName.ToLower() &&
             user.PasswordHash == hmac.ComputeHash(Dencoding.UTF8.GetBytes(userLogin.Password)) &&
             user.PasswordSalt ==  hmac.GetHashCode);
        } */


    private async Task<bool> UserIsNullOrEmpty(string username)
    {

        return String.IsNullOrEmpty(username);
    }
    #endregion ---------------   

}
