using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CasoUno.Principal.Front.ViewModels
{
    public class RegisterVM
    {

        [Required]
        [EmailAddress]
        [Display(Name ="Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Nombre para mostrar")]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="La {0} debe tener al menos {2} caractwres")]
        [DataType(DataType.Password)]
        [Display(Name ="Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirmar Contraseña")]
        [Compare("Password", ErrorMessage ="La contraseña y la confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}