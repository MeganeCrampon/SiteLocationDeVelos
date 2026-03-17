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
            if (utilisateur == null) return BadRequest("Erreur");
            string mdpHache = BCrypt.Net.BCrypt.HashPassword(utilisateur.Mdp);
            utilisateur.Mdp = mdpHache;

            ListeUtilisateurs.Add(utilisateur);
            return Ok(new { message = "Utilisateur bien enregistré !" });
        }
    }
}