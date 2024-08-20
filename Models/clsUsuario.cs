using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// --------------------------------------
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySql.Data.MySqlClient;
using System.Data;
// --------------------------------------

namespace apiRESTCenso.Models
{
    public class clsUsuario
    {
        // ---------------------------------
        // Definición de la estructura del modelo
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string usuario { get; set; }
        public string contrasena { get; set; }
        public string ruta { get; set; }
        public string tipo { get; set; }

        // Definición de los métodos de procesos
        // Constructores
        public clsUsuario() { 
            // Constructor estándar
        }
        public clsUsuario(string usuario, string contrasena) {
            this.usuario = usuario;
            this.contrasena = contrasena;        
        }
        public clsUsuario(string nombre,
                          string apellidoPaterno,
                          string apellidoMaterno,
                          string usuario,
                          string contrasena,
                          string ruta,
                          string tipo) {
            this.nombre = nombre;
            this.apellidoPaterno = apellidoPaterno;
            this.apellidoMaterno = apellidoMaterno;
            this.usuario = usuario;
            this.contrasena = contrasena;
            this.ruta = ruta;
            this.tipo = tipo;
        }
        // Atributos y métodos de procesos
        private string cadConn = "server=localhost;database=control_acceso;uid=root;pwd=ingenieria;";

        // Proceso de inserción de usuarios
        public DataSet spInsUsuario() {

            string cadSql = "call spInsUsuario('" + this.nombre + "','" +
                                                    this.apellidoPaterno + "','" +
                                                    this.apellidoMaterno + "','" +
                                                    this.usuario + "','" +
                                                    this.contrasena + "','" +
                                                    this.ruta + "'," +
                                                    this.tipo + ");";

            MySqlConnection cnn = new MySqlConnection(cadConn);
            DataSet ds = new DataSet();
            // Objeto adaptador para ejecutar el comando y recibir
            // respuesta
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            // Ejecución del comando y recepción de respuesta
            da.Fill(ds, "spInsUsuario");
            // Envío de salida
            return ds;
        }
        // ----------------------------------
        // Método para validar control de acceso a usuarios
        public DataSet spValidarAcceso() {

            string cadSql = "call spValidarAcceso('" + this.usuario + "','" +
                                                       this.contrasena + "');";

            MySqlConnection cnn = new MySqlConnection(cadConn);
            DataSet ds = new DataSet();
            // Objeto adaptador para ejecutar el comando y recibir
            // respuesta
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            // Ejecución del comando y recepción de respuesta
            da.Fill(ds, "spValidarAcceso");
            // Envío de salida
            return ds;
        }
        // ---------------------------------------------------------
        // Método para enviar registros de la vista vwRptUsuario
        public DataSet vwRptUsuario()
        {
            string cadSql = "select * from vwRptUsuario;";
            MySqlConnection cnn = new MySqlConnection(cadConn);
            DataSet ds = new DataSet();
            // Objeto adaptador para ejecutar el comando y recibir
            // respuesta
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            // Ejecución del comando y recepción de respuesta
            da.Fill(ds, "vwRptUsuario");
            // Envío de salida
            return ds;
        }
        // ---------------------------------------------------------
        // Método para enviar registros de la vista vwTipoUsuario
        public DataSet vwTipoUsuario()
        {
            string cadSql = "select * from vwTipoUsuario;";
            MySqlConnection cnn = new MySqlConnection(cadConn);
            DataSet ds = new DataSet();
            // Objeto adaptador para ejecutar el comando y recibir
            // respuesta
            MySqlDataAdapter da = new MySqlDataAdapter(cadSql, cnn);
            // Ejecución del comando y recepción de respuesta
            da.Fill(ds, "vwTipoUsuario");
            // Envío de salida
            return ds;
        }

    }
}