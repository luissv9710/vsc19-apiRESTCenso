using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apiRESTCenso.Models
{
    public class clsCensoDatos
    {
        // ---------------------------------------------------------------------------------
        // 3.-) DEFINICIÓN DE LA ESTRUCTURA DE LOS DATOS APLICADA EN LA WEB API REST
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string rol { get; set; }
        // ---------------------------------------------------------------------------------
    }
}