using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AplicacionGrupoMusical.Models
{
    public class Conexion
    {
        public static string CN = ConfigurationManager.ConnectionStrings["Db-conexion"].ConnectionString;
    }
}