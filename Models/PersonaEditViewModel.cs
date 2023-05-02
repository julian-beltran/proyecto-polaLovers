using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APPAdminGroup.Models
{
    public class PersonaEditViewModel
    {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apelllido { get; set; }
        [Required]
        public string telefono { get; set; }
        [Required]
        public string direccion { get; set; }
        public string descripcion { get; set; }
        [Required]
        public Nullable<int> id_rol { get; set; }

        [Required]
        public string usuario1 { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Contraseña")]
        //public string contraseña { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirmar contraseña")]
        //[Compare("contraseña", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        //public string confirmar_contraseña { get; set; }

        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string correo { get; set; }


    }

    public class personaEditPerfilViewModel {
        public int id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apelllido { get; set; }
        [Required]
        public string telefono { get; set; }
        [Required]
        public string direccion { get; set; }
        [Required]
        public string usuario1 { get; set; }
        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string correo { get; set; }
        public byte[] image { get; set; }
    }

}