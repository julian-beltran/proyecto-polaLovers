using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionGrupoMusical.Models
{
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public string confirmar_contraseña { set; get; }
        public string correo { get; set; }

    }
}