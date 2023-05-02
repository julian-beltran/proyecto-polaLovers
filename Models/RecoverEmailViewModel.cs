using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APPAdminGroup.Models
{
    public class RecoverEmailViewModel
    {
        public string tokenCorreo { get; set; }
    }

    public class ContraseñaRecoveryViewModel
    {
        public string tokenContraseña { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contraseña { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("contraseña", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string confirmar_contraseña { get; set; }
    }
    public class ContraseñaStartRecoveryViewModel {
        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string correo { get; set; }
    }

    public class loginViewModel
    {
        [Required]
        public string usuario1 { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contraseña { get; set; }

        public Nullable<bool> confirmarEmail { get; set; }
    }

}