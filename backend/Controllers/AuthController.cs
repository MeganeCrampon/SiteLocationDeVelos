using Microsoft.AspNetCore.Mvc;
using SiteLocaVelos.Models;
using System.Linq;
using BCrypt;

namespace SiteLocaVelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly List<Utilisateur> ListeUtilisateurs = new List<Utilisateur>();

        [HttpPost]
        public IActionResult Login([FromBody] Utilisateur loginInfo)
        {
            var utilisateurFetch = ListeUtilisateurs.FirstOrDefault(u => u.Email == loginInfo.Email);
            if (utilisateurFetch == null) return Unauthorized(new { message = "Cet email n'existe pas."});

            bool estValide = BCrypt.Net.BCrypt.Verify(loginInfo.Mdp, utilisateurFetch.Mdp);

            if (estValide) {
                return Ok(new { message = $"Connexion réussie ! Ravi de vous revoir {utilisateurFetch.Nom}!" });
            } else { 
                return Unauthorized(new { message = "Mot de passe incorrect:"});
            }
        }
    }
}