using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// ---------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
// ---------------------------------

namespace apiRESTCenso.Models
{
    public class clsApiStatus
    {
        // Estructura de atributos del modelo
        // Estatus de ejecución del endpoint
        public bool statusExec { get; set; }
        // Descripción del estado de salida
        public string msg { get; set; }
        // Código de ejecución del resultado
        public int ban { get; set; }
        // Objeto Json para envío de datos
        public JObject datos { get; set; }
    }
}