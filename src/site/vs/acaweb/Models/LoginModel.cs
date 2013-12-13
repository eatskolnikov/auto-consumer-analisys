using System.ComponentModel.DataAnnotations;

namespace acaweb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="El Nombre de Usuario es requerido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La Contraseña es requerida")]
        public string Password { get; set; }
    }
}
