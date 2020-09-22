using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebAPI.Models;

namespace WebAPI.Controllers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly DbFirstContext _db;
        private readonly IConfiguration _config;

        public AuthenticationController(DbFirstContext context, IConfiguration config)
        {
            _db = context;
            _config = config;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var MyTenant = _db.Tenants.FirstOrDefault();

            if (MyTenant.Tstatus == 0)
            {
                return Unauthorized("Votre tenant n'est pas activé.");
            }

            if (MyTenant.Tstatus == 2)
            {
                return Unauthorized("Page en cours de maintenance.");
            }

            if (model == null)
            {
                return BadRequest("Invalid request");
            }

            if (!_db.Utilisateurs.Any(x => x.UsrLogin == model.Login))
            {
                return Unauthorized("Le nom d'utilisateur est incorrect.");
            }

            var dbusr = _db.Utilisateurs.FirstOrDefault(x => x.UsrName == model.Login);

            if (!ValidatePassword(dbusr, model.Password))
            {
                // return Unauthorized("Le nom d'utilisateur ou le mot de passe sont incorrect.");
            }

            if (!dbusr.UsrActive)
            {
                return Unauthorized("Votre compte est désactivé !");
            }

            if (_db.AuthorisationQueries.GetDefault(dbusr.UsrLogin) == null)
            {
                return Unauthorized("Vous n'avez pas d’autorisation sur aucune société .");
            }

            return Ok(new { Token = GenerateTokens() }); // L'authentification est réussie.
        }

        private bool ValidatePassword(Utilisateur user, string password)
        {
            return PasswordHasher.VerifyHashedPassword(user.UsrPasswordHash, password);
        }

        private string GenerateTokens()
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
        }
    }
}