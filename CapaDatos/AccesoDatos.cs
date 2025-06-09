using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class AccesoDatos
    {
        private string cadenaConexion = @"Data Source=FERCASTEDO;Initial Catalog=UniversidadDB;Integrated Security=True";

        public DataTable EjecutarConsulta(string consulta)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                SqlDataAdapter da = new SqlDataAdapter(consulta, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void EjecutarComando(string comando)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand(comando, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
