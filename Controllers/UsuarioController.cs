using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// --------------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using apiRESTCenso.Models;
using System.Data;
// --------------------------------------

namespace apiRESTCenso.Controllers
{
    public class UsuarioController : ApiController
    {

        [HttpPost]
        [Route("das/usuario/spInsUsuario")]
        public clsApiStatus spInsUsuario([FromBody] clsUsuario modelo)
        {
            // -------------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            // -------------------------------------------
            try
            {
                // ------------------------------------
                // Creación del objeto y ejcución del método de insercion
                clsUsuario objUsuario = new clsUsuario(modelo.nombre,
                                                       modelo.apellidoPaterno,
                                                       modelo.apellidoMaterno,
                                                       modelo.usuario,
                                                       modelo.contrasena,
                                                       modelo.ruta,
                                                       modelo.tipo);
                DataSet ds = new DataSet();
                ds = objUsuario.spInsUsuario();   // <--------------
                // ------------------------------------
                // Formateo del objeto de salida
                objRespuesta.statusExec = true;
                objRespuesta.msg = "Ejecución exitosa (MySql-control_acceso-spInsUsuario)";

                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString() );

                jsonResp.Add("msgData", "Ejecución exitosa (MySql-control_acceso-spInsUsuario)");
                objRespuesta.datos = jsonResp;

            }
            catch (Exception ex)
            {
                // Formateo del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Fallo en Ejecución (MySql-control_acceso-spInsUsuario)";
                objRespuesta.ban = 0;   // <--- Fallo
                jsonResp.Add("msgData", ex.Message.ToString());
                jsonResp.Add("exception", ex.InnerException.ToString());
                objRespuesta.datos = jsonResp;
            }
            // --------------------------
            // Envío del objeto de salida
            return objRespuesta;
        }
        // ------------------------------------------------
        // Método para publicación del proceso de Acceso
        [HttpPost]
        [Route("das/usuario/spValidarAcceso")]
        public clsApiStatus spValidarAcceso([FromBody] clsUsuario modelo)
        {
            // -------------------------------------------
            clsApiStatus objRespuesta = new clsApiStatus();
            JObject jsonResp = new JObject();
            // -------------------------------------------
            try
            {
                // ------------------------------------
                clsUsuario objUsuario = new clsUsuario(modelo.usuario,
                                                       modelo.contrasena);
                DataSet ds = new DataSet();
                ds = objUsuario.spValidarAcceso();
                // ------------------------------------
                // Formateo del objeto de salida

                objRespuesta.statusExec = true;
                objRespuesta.msg = "Ejecución exitosa (MySql-control_acceso-spValidarAcceso)";

                objRespuesta.ban = int.Parse(ds.Tables[0].Rows[0][0].ToString());

                // -----------------------------------
                // Validación de los datos recibidos
                if (objRespuesta.ban == 1)
                {
                    jsonResp.Add("usu_nombre_completo", ds.Tables[0].Rows[0][1].ToString());
                    jsonResp.Add("usu_ruta", ds.Tables[0].Rows[0][2].ToString());
                    jsonResp.Add("usu_usuario", ds.Tables[0].Rows[0][3].ToString());
                    jsonResp.Add("tip_descripcion", ds.Tables[0].Rows[0][4].ToString());
                }
                else {
                    jsonResp.Add("msgData", "Ejecución exitosa (MySql-control_acceso-spValidarAcceso)");
                }
                objRespuesta.datos = jsonResp;
            }
            catch (Exception ex) {
                // Formateo del objeto de salida
                objRespuesta.statusExec = false;
                objRespuesta.msg = "Fallo en Ejecución (MySql-control_acceso-spValidarAcceso)";
                objRespuesta.ban = 0;   // <--- Fallo
                jsonResp.Add("msgData", ex.Message.ToString());
                jsonResp.Add("exception", ex.InnerException.ToString());
                objRespuesta.datos = jsonResp;
            }
            return objRespuesta;
        }
        // ------------------------------------------------
        // Método para publicación del proceso de reporte de usuarios
        [HttpGet]
        [Route("das/usuario/vwRptUsuario")]
        public DataSet vwRptUsuario()
        {
            DataSet ds = new DataSet();
            try
            {
                // ------------------------------------
                clsUsuario objUsuario = new clsUsuario();
                ds = objUsuario.vwRptUsuario();
                // ------------------------------------
            }
            catch (Exception ex) {
                // ------------------------------------
                // Configuración del DataSet de salida
                DataTable dt = new DataTable("vwRptUsuario");
                dt.Columns.Add("statusExec");
                dt.Columns.Add("msg");
                dt.Columns.Add("ban");
                dt.Columns.Add("msgData");
                dt.Columns.Add("msgException");
                DataRow dr = dt.NewRow();
                dr["statusExec"] = "false";
                dr["msg"] = "Error en consulta de datos del reporte";
                dr["ban"] = "0";
                dr["msgData"] = ex.Message.ToString();
                dr["msgException"] = ex.InnerException.ToString();
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);                
            }
            return ds;
        }

        // ------------------------------------------------
        // Método para publicación del proceso de reporte de usuarios
        [HttpGet]
        [Route("das/usuario/vwTipoUsuario")]
        public DataSet vwTipoUsuario()
        {
            DataSet ds = new DataSet();
            try
            {
                // ------------------------------------
                clsUsuario objUsuario = new clsUsuario();
                ds = objUsuario.vwTipoUsuario();
                // ------------------------------------
            }
            catch (Exception ex)
            {
                // ------------------------------------
                // Configuración del DataSet de salida
                DataTable dt = new DataTable("vwTipoUsuario");
                dt.Columns.Add("statusExec");
                dt.Columns.Add("msg");
                dt.Columns.Add("ban");
                dt.Columns.Add("msgData");
                dt.Columns.Add("msgException");
                DataRow dr = dt.NewRow();
                dr["statusExec"] = "false";
                dr["msg"] = "Error en consulta de datos del reporte";
                dr["ban"] = "0";
                dr["msgData"] = ex.Message.ToString();
                dr["msgException"] = ex.InnerException.ToString();
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }
            return ds;
        }


    }
}
