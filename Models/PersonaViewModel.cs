using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APPAdminGroup.Models
{
    public class PersonaViewModel
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
        public Nullable<int> id_rol { get; set; }
        public byte[] image { get; set; }

        public Rol rol { get; set; }
        public Usuario usuario { get; set; }

    }
}