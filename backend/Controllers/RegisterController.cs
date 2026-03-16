using Microsoft.AspNetCore.Mvc;
using SiteLocaVelos.Models;
using System.Linq;

namespace SiteLocaVelos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private static readonly List<Utilisateur> ListeUtilisateurs = new List<Utilisateur>();

        [HttpPost]
        public IActionResult Post([FromBody] Utilisateur nouveau)
        {
            if (nouveau == null) return BadRequest("Erreur");
            ListeUtilisateurs.Add(nouveau);
            return Ok(new { message = "Utilisateur bien enregistré !" });
        }
    }
}