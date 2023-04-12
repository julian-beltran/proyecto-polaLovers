using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionGrupoMusical.Models
{
    public class Persona
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string descripcion { get; set; }
        public byte image { get; set; }
        public DateTime date { get; set; }
        public int id_usuario { get; set; }




    }
}