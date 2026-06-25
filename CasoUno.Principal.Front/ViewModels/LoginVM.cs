using System.ComponentModel.DataAnnotations;

namespace CasoUno.Principal.Front.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}