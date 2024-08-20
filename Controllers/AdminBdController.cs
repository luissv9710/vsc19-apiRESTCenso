using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// ------------------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using apiRESTCenso.Models;
using MySql.Data.MySqlClient;
// ------------------------------------------

namespace apiRESTCenso.Controllers
{
    public class AdminBdController : ApiController
    {
        private string cadConn = "server=localhost;database=control_acceso;uid=root;pwd=ingenieria;";

        // Endpoint para validar acceso y conexión a BD Mysql

        [HttpGet]
        [Route("das/adminbd/checkMySqlConnection")]
        public clsApiStatus checkMySqlConnection() {
            // -------------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            // -------------------------------------------
            try
            {
                MySqlConnection cnn = new MySqlConnection(cadConn);
                // Chequeo de abrir/cerrar conexión
                cnn.Open();
                // Cerrar
                cnn.Close();
                // ----------------------------------
                // Formateo del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Conexión exitosa (MySql-control_acceso)";
                objRespuesta.ban = 1;   // <--- Éxito
                jsonResp.Add("msgData", "Conexión exitosa (MySql-control_acceso)");
                objRespuesta.datos = jsonResp;

            }
            catch (Exception ex) {
                // Formateo del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Fallo en Conexión (MySql-control_acceso)";
                objRespuesta.ban = 0;   // <--- Fallo
                jsonResp.Add("msgData", ex.Message.ToString());
                jsonResp.Add("exception", ex.InnerException.ToString());
                objRespuesta.datos = jsonResp;
            }

            return objRespuesta;
        }



    }
}
