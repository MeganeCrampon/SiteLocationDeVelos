using Microsoft.AspNetCore.Mvc;
using SiteLocaVelos.Models;
using System.Linq;
using BCrypt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SiteLocaVelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private static readonly List<Utilisateur> ListeUtilisateurs = new List<Utilisateur>();
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Utilisateur loginInfo)
        {
            var utilisateurFetch = ListeUtilisateurs.FirstOrDefault(u => u.Email == loginInfo.Email);
            if (utilisateurFetch == null) return Unauthorized(new { message = "Cet email n'existe pas."});

            bool estValide = BCrypt.Net.BCrypt.Verify(loginInfo.Mdp, utilisateurFetch.Mdp);

            if (estValide) 
            {
                var claims = new[] {
                    new Claim(ClaimTypes.Name, utilisateurFetch.Nom),
                    new Claim(ClaimTypes.Email, utilisateurFetch.Email)};
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)); // ! de sécurité pour rassurer à VSCode 
                var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer : "MonServeurVelo", 
                    audience : "MonAppVue", 
                    claims : claims, 
                    expires : DateTime.Now.AddHours(2), 
                    signingCredentials : signature);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new { 
                    token = tokenString,
                    message = $"Connexion réussie ! Ravi de vous revoir {utilisateurFetch.Nom}!" 
                    });
            } else { 
                return Unauthorized(new { message = "Mot de passe incorrect:"});
            }
        }
    }
}