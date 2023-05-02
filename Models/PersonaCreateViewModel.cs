using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APPAdminGroup.Models
{
    public partial class PersonaCreateViewModel
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
        public byte[] image { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<int> id_usuario { get; set; }
        [Required]
        public Nullable<int> id_rol { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Rol Rol { get; set; }
    }
}