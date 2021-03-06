﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Airport_Management.Clases
{
    class GestionRutas
    {

        public GestionRutas()
        {

        }

        public bool CodigoExiste(string codigo)
        {
            AccesoDatos ad = new AccesoDatos();
            string consulta = "Select * from rutas where codigo_RTA = '" + codigo + "'";

            int cantidad = ad.ContarRegistros(consulta);
            if (cantidad > 0) return true;
            return false;
        }
         
        public bool AgregarRuta(string AtoPartida, string AtoLlegada, int ETA, int posta)
        { 

            AccesoDatos ad = new AccesoDatos();
            string consultaSQL = "INSERT INTO rutas (codigo_RTA, ATOpartida_RTA, ATOarrivo_RTA, ETA_RTA, posta_RTA) SELECT '" + AtoPartida + "-" + AtoLlegada + "', '" + AtoPartida + "', '" + AtoLlegada + "', " + ETA.ToString() + ", " + posta.ToString();

            ad.EjecutarConsulta(consultaSQL);
            return true;
        }
        public bool modificarRuta(string codigoRuta, int ETA, int posta)
        {
            AccesoDatos ad = new AccesoDatos();
            string consultaSQL = "UPDATE rutas SET ETA_RTA = " + ETA.ToString() + ", posta_RTA = " + posta.ToString() + "WHERE codigo_RTA = '" + codigoRuta + "'";

            ad.EjecutarConsulta(consultaSQL);
            return true;
        }

        public bool eliminarRuta (string codigo)
        {
            AccesoDatos ad = new AccesoDatos();
            string consultaSQL = "DELETE FROM rutas WHERE codigo_RTA = '" + codigo + "'";

            ad.EjecutarConsulta(consultaSQL);
            return true;
        }

    /*    public bool eliminarRuta(String NombreTabla, DataSet ds)
        {
            int FilasEliminadas = 0;
            foreach (DataRow fila in ds.Tables[NombreTabla].Rows)
            {
                SqlCommand Comando = new SqlCommand();
                fila.RejectChanges();
                AccesoDatos ad = new AccesoDatos();
                ArmarParametrosRutasEliminar(ref Comando, fila);
                FilasEliminadas = ad.EjecutarProcedimientoAlmacenado(Comando, "spEliminarRuta");
            }
            if (FilasEliminadas >= 1)
                return true;
            else
                return false;
        }
        */

        private void ArmarParametrosRutasEliminar(ref SqlCommand Comando, DataRow fila)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@Codigo", SqlDbType.VarChar);
            SqlParametros.Value = fila["Codigo"];

        }



        /*  public bool modificarAvion(String NombreTabla, DataSet ds)
          {
              int FilasActualizadas = 0;
              foreach (DataRow fila in ds.Tables[NombreTabla].Rows)
              {
                  SqlCommand Comando = new SqlCommand();
                  ArmarParametrosAviones(ref Comando, fila);
                  AccesoDatos ad = new AccesoDatos();

                  FilasActualizadas = ad.EjecutarProcedimientoAlmacenado(Comando, "spModificarAvion");
              }
              if (FilasActualizadas >= 1)
                  return true;
              else
                  return false;
          }
          */
        public void ArmarParametrosRutas(ref SqlCommand Comando, String codigo_av, String tipo_av)
        {
            SqlParameter SqlParametros = new SqlParameter();
            SqlParametros = Comando.Parameters.Add("@Codigo", SqlDbType.VarChar, 10);
            SqlParametros.Value = codigo_av;
            SqlParametros = Comando.Parameters.Add("@Tipo", SqlDbType.VarChar, 10);
            SqlParametros.Value = tipo_av;
        }

        public Boolean insertarRuta(String NombreTabla, String Codigo, String Tipo)
        {
            SqlCommand Comando = new SqlCommand();
            ArmarParametrosRutas(ref Comando, Codigo, Tipo);
            AccesoDatos ad = new AccesoDatos();
            ad.EjecutarProcedimientoAlmacenado(Comando, "spInsertarRuta");
            return true;
        }

        public DataSet TraerRutaCodigo(string codigo)
        {
            DataSet ds = new DataSet();
            AccesoDatos ad = new AccesoDatos();
            string consulta = "Select * from rutas where codigo_RTA = '" + codigo + "'";

            ad.cargaTabla("Rutas", consulta, ref ds);
            return ds;
        }























    }
}
