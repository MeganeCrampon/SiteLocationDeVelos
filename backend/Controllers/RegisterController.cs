using Microsoft.AspNetCore.Mvc;
using SiteLocaVelos.Models;
using System.Linq;
using BCrypt.Net;

namespace SiteLocaVelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private static readonly List<Utilisateur> ListeUtilisateurs = new List<Utilisateur>();

        [HttpPost]
        public IActionResult Post([FromBody] Utilisateur utilisateur)
        {
            string mdpHache = Bcrypt.Net.BCrypt.HashPassword(utilisateur.Mdp);
            var newMdp = mdpHache
            if (utilisateur == null) return BadRequest("Erreur");
            ListeUtilisateurs.Add(utilisateur);
            return Ok(new { message = "Utilisateur bien enregistré !" });
        }
    }
}