using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _tenantContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TokenController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration,
            ApplicationDbContext tenantContext, RoleManager<IdentityRole> roleManager
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _tenantContext = tenantContext;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                // Get the user roles
                var roles = await _userManager.GetRolesAsync(user);
                var guid = Guid.NewGuid().ToString();
                var claims = new List<Claim>
                {
                   new Claim(JwtRegisteredClaimNames.Jti, guid),
                   //new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                   new Claim(ClaimTypes.NameIdentifier, user.Id),
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.Email, user.Email)
                };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(15),
                    signingCredentials: creds);

                var identityCliams = new List<Claim>
                {
                        new Claim(JwtRegisteredClaimNames.Jti, guid),
                       
                };


                // Sign in the user with claims
                await _signInManager.SignInWithClaimsAsync(user, isPersistent: true, identityCliams);
                /*if (result.Succeeded)
                { 
                
                }*/

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Invalid login attempt.");
        }

        [HttpPost("tenantlogin")]
        public async Task<IActionResult> TenantLogin([FromBody] TenantLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tenantUser = await _tenantContext.TenantUsers.SingleOrDefaultAsync(c => c.TenantUserName == model.TenantUserName);

            if (tenantUser != null)
            {

                var getTenant = await _tenantContext.Tenants.FindAsync(tenantUser.TenantID);

                var user = await _userManager.FindByIdAsync(tenantUser.AspUserID ?? "");

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    // Get the user role
                    var role = await _roleManager.FindByIdAsync(tenantUser.AspRoleID ?? "");

                    var guid = Guid.NewGuid().ToString();
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, guid),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim("TenantUserName", model.TenantUserName),
                        new Claim("TenantID", getTenant.TenantID.ToString()),
                        new Claim("PkID", tenantUser.PkID.ToString()),
                        new Claim("CompanyName", getTenant.CompanyName),
                        new Claim(ClaimTypes.Role, role?.Name ?? "No Role")
                    };
                    
                    //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Issuer"],
                        claims: claims,
                        expires: DateTime.Now.AddDays(15),
                        signingCredentials: creds
                    );

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenString = tokenHandler.WriteToken(token);

                    // Create the ClaimsPrincipal
                    //var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var identityCliams = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, guid),
                        new Claim("TenantUserName", model.TenantUserName),
                        new Claim("TenantID", getTenant.TenantID.ToString()),
                        new Claim("PkID", tenantUser.PkID.ToString()),
                        new Claim("CompanyName", getTenant.CompanyName),
                        new Claim(ClaimTypes.Role, role?.Name ?? "No Role")
                    };

                    // Sign in the user with claims
                    await _signInManager.SignInWithClaimsAsync(user, isPersistent: true, identityCliams);

                    return Ok(new
                    {
                        token = tokenString,
                        expiration = token.ValidTo
                    });
                }
            }

            return BadRequest("Invalid login attempt.");
        }

    }

    public class LoginModel
     {
        [Required]
        [EmailAddress]
        public string UserName { get; set; }
     
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
     }

     public class TenantLoginModel
     {
        [Required]
        public string TenantUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
     }

}
