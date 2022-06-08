using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserMangmentService.Models;

namespace UserMangmentService.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AdminController(UserManager<ApplicationUser> user, RoleManager<IdentityRole> role, IConfiguration configuration)
        {
            _userManager = user;
            _roleManager = role;
            _configuration = configuration;
        }
        [HttpGet]
        [Authorize]
        [Authorize(Roles = Roles.Admin)]
        public string get()
        {
            return "Hello World";
        }

        [HttpGet]
        [Route("loginuserdetail")]
        public async Task<UserDetail> Get(string user)
        {
            Response response = new Response();
            try
            {
                UserDetail tbl = new UserDetail();
                var userres = await _userManager.FindByNameAsync(user);
                if (userres != null)
                {
                    tbl.UserName = userres.UserName;
                    tbl.Email = userres.Email;
                    return tbl;
                }
                else
                    throw new Exception("failed to find user");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
            }
            return null;
        }


        //[AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDetail userRegister)
        {
            Response response = new Response();
            try
            {
                var userExist = await _userManager.FindByNameAsync(userRegister.UserName);
                if (userExist != null)
                {
                    response.Message = "User Already Exists";
                    response.Status = "Error";
                    response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                    throw new Exception(response.Message);
                }
                ApplicationUser user = new ApplicationUser()
                {
                    Email = userRegister.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = userRegister.UserName
                };
                var result = await _userManager.CreateAsync(user, userRegister.Password);
                if (!result.Succeeded)
                {
                    string message = String.Empty;
                    foreach (var err in result.Errors)
                    {
                        var m = err.Description.ToString();
                        message += m;
                    }
                    response.Message = message;
                    throw new Exception(response.Message);
                }
                if (!await _roleManager.RoleExistsAsync(Roles.User))
                    await _roleManager.CreateAsync(new IdentityRole(Roles.User));
                if (await _roleManager.RoleExistsAsync(Roles.User))
                {
                    await _userManager.AddToRoleAsync(user, Roles.User);
                }

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status404NotFound.ToString();
                return NotFound(response);
            }
            response.Status = "Success";
            response.Message = "User Created Successfully";
            response.StatusCode = StatusCodes.Status200OK.ToString();
            return Ok(response);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserDetail userLogin)
        {
            Response response = new Response();
            try
            {
                var user = await _userManager.FindByNameAsync(userLogin.UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, userLogin.Password))
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var authClaim = new List<Claim>{
                    new Claim(ClaimTypes.Name,user.UserName),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                    foreach (var userRole in userRoles)
                    {
                        authClaim.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:Issuer"],
                        audience: _configuration["JWT:Audience"],
                        expires: DateTime.Now.AddHours(5),
                        claims: authClaim,
                        signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                        );
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status404NotFound.ToString();
                return NotFound(response);
            }

            response.Message = "Unauthorized User";
            response.Status = "Error";
            response.StatusCode = StatusCodes.Status401Unauthorized.ToString();
            return Unauthorized(response);
        }

        [HttpPost]
        [Route("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserDetail userRegister)
        {
            Response response = new Response();
            try
            {
                var userExist = await _userManager.FindByNameAsync(userRegister.UserName);
                if (userExist != null)
                {
                    response.Message = "User Already Exists";
                    response.Status = "Error";
                    response.StatusCode = StatusCodes.Status500InternalServerError.ToString();
                    throw new Exception();
                }
                ApplicationUser user = new ApplicationUser()
                {
                    Email = userRegister.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = userRegister.UserName
                };
                var result = await _userManager.CreateAsync(user, userRegister.Password);
                if (!result.Succeeded)
                {
                    string message = String.Empty;
                    foreach (var err in result.Errors)
                    {
                        var m = err.Description.ToString();
                        message += m;
                    }
                    response.Message = message;
                    throw new Exception(response.Message);
                }
                if (!await _roleManager.RoleExistsAsync(Roles.Admin))
                    await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
                if (!await _roleManager.RoleExistsAsync(Roles.User))
                    await _roleManager.CreateAsync(new IdentityRole(Roles.User));
                if (await _roleManager.RoleExistsAsync(Roles.Admin))
                {
                    await _userManager.AddToRoleAsync(user, Roles.Admin);
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = "Error";
                response.StatusCode = StatusCodes.Status404NotFound.ToString();
                return NotFound(response);
            }
            response.Status = "Success";
            response.Message = "User Created Successfully";
            response.StatusCode = StatusCodes.Status200OK.ToString();
            return Ok(response);
        }

    }
}
