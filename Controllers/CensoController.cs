using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
// ------------------------
using apiRESTCenso.Models;

namespace apiRESTCenso.Controllers
{
    public class CensoController : ApiController
    {
        // Arreglo de Objetos tipo clsCensoDatos
        public static clsCensoDatos[] objCensoDatos = null;

        // GET: api/Censo
        public IEnumerable<clsCensoDatos> Get()
        {
            return objCensoDatos;
        }

        // GET: api/CensoDemo/5
        public clsCensoDatos Get(int id)
        {
            // PROCESO PARA MANIPULAR UN OBJETO DE DATOS DEL TIPO clsCensoDatos
            // SE VALIDA EL FILTRADO DEL OBJETO EN EL REPOSITORIO GLOBAL DE LA API PARA ACTUALIZAR
            // TODOS SUS DATOS
            int i;
            clsCensoDatos elemento = new clsCensoDatos();
            elemento.id = 0;
            elemento.nombre = "";
            elemento.apellidoPaterno = "";
            elemento.rol = "";

            for (i = 0; i < objCensoDatos.Length; i++)
            {
                if (objCensoDatos[i].id == id)
                {
                    // ACTUALIZACIÓN DE LOS DATOS DEL OBJETO ENCONTRADO
                    elemento.id = objCensoDatos[i].id;
                    elemento.nombre = objCensoDatos[i].nombre;
                    elemento.apellidoPaterno = objCensoDatos[i].apellidoPaterno;
                    elemento.rol = objCensoDatos[i].rol;
                    break;
                }
            }

            // SE RETORNA A LA INSTANCIA HTTPREQUEST LA BANDERA DE PUT EXITOSO O FALLIDO
            return elemento;
        }

        // POST: api/Censo
        public string Post([FromBody]clsCensoDatos modelo)
        {
            // PROCESO PARA MANIPULAR OBJETOS DE DATOS DEL TIPO clsCensoDatos
            // SE VALIDA LA INSERCIÓN DE OBJETOS INDEPENDIENTES AL REPOSITORIO GLOBAL DE LA API
            int i;
            if (objCensoDatos == null)
            {
                // INSERCIÓN DEL 1ER OBJETO
                objCensoDatos = new clsCensoDatos[1];
                objCensoDatos[0] = modelo;
            }
            else
            {
                // INSERCION DE (N) OBJETOS
                clsCensoDatos[] objCensoDatosAux = new clsCensoDatos[objCensoDatos.Length + 1];
                for (i = 0; i < objCensoDatos.Length; i++)
                    objCensoDatosAux[i] = objCensoDatos[i];
                objCensoDatosAux[i] = modelo;

                // ACTUALIZACIÓN DEL REPOSITORIO DE OBJETOS DE LA API
                objCensoDatos = null;
                objCensoDatos = new clsCensoDatos[objCensoDatosAux.Length];
                objCensoDatos = objCensoDatosAux;
                objCensoDatosAux = null;
            }

            // SE RETORNA A LA INSTANCIA HTTPREQUEST LOS VALORES INSERTADOS MEDIANTE POST
            return modelo.id + ", " + modelo.nombre + ", " + modelo.apellidoPaterno + ", " + modelo.rol;

        }

        // PUT: api/CensoDemo/5
        public string Put(int id, [FromBody] clsCensoDatos modelo)
        {
            // PROCESO PARA MANIPULAR UN OBJETO DE DATOS DEL TIPO clsCensoDatos
            // SE VALIDA EL FILTRADO DEL OBJETO EN EL REPOSITORIO GLOBAL DE LA API PARA ACTUALIZAR
            // TODOS SUS DATOS
            int i, ban = 0;
            if (objCensoDatos != null)
            {
                for (i = 0; i < objCensoDatos.Length; i++)
                {
                    if (objCensoDatos[i].id == id)
                    {
                        ban = 1;
                        // ACTUALIZACIÓN DE LOS DATOS DEL OBJETO ENCONTRADO
                        objCensoDatos[i].nombre = modelo.nombre;
                        objCensoDatos[i].apellidoPaterno = modelo.apellidoPaterno;
                        objCensoDatos[i].rol = modelo.rol;
                        //break;
                    }
                }

                // SE RETORNA A LA INSTANCIA HTTPREQUEST LA BANDERA DE PUT EXITOSO O FALLIDO
                return "ban: " + ban;
            }
            else
                return "ban: -1";
        }

        // DELETE: api/CensoDemo/5
        public string Delete(int id)
        {
            int i, j, ban = 0;

            if (objCensoDatos != null)
            {
                // PROCESO PARA VALIDAR EL FILTRADO DEL OBJETO QUE SE ELIMINARÁ
                for (i = 0; i < objCensoDatos.Length; i++)
                    if (objCensoDatos[i].id == id)
                        ban = 1;

                // PROCESO DE ELIMINACIÓN DEL OBJETO EN EL REPOSITORIO GLOBAL DE LA API
                if (ban == 1)
                {
                    clsCensoDatos[] objCensoDatosAux = new clsCensoDatos[objCensoDatos.Length - 1];

                    for (i = 0, j = 0; i < objCensoDatos.Length; i++)
                    {
                        // OMISIÓN DEL OBJETO, PARA ELIMINARLO DEL REPOSITORIO, PARA FINALMENTE 
                        // ACTUALIZARLO CON LOS OBJETOS VIGENTES
                        if (objCensoDatos[i].id != id)
                        {
                            objCensoDatosAux[j] = objCensoDatos[i];
                            j++;
                        }
                    }

                    // ACTUALIZACIÓN DEL REPOSITORIO DE OBJETOS DE LA API
                    objCensoDatos = null;
                    objCensoDatos = new clsCensoDatos[objCensoDatosAux.Length];
                    objCensoDatos = objCensoDatosAux;
                    objCensoDatosAux = null;
                }

                // SE RETORNA A LA INSTANCIA HTTPREQUEST LA BANDERA DE DELETE EXITOSO O FALLIDO
                return "ban: " + ban;
            }
            else
                return "ban: -1";
        }
    }
}
