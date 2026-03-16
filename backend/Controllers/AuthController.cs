using Microsoft.AspNetCore.Mvc;
using SiteLocaVelos.Models;
using System.Linq;

namespace SiteLocaVelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly List<Utilisateur> _utilisateurs = new List<Utilisateur>();

        [HttpPost]
        public IActionResult Login([FromBody] Utilisateur loginInfo)
        {
            var user = _utilisateurs.FirstOrDefault(u => u.Email == loginInfo.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Cet email n'existe pas."});
            }
            if (user.Mdp != loginInfo.Mdp)
            {
                return Unauthorized(new { message = "Mot de passe incorect:"});
            }
            return Ok(new { message = $"Connexion réussie ! Ravi de vous revoir {user.Nom}!" });
        }
    }
}