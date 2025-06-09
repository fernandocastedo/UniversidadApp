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
        private string cadenaConexion = @"Data Source=FERCASTEDO;Initial Catalog=UniversidadDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";


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
        public bool InsertarNota(EstEdNota nota)
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "INSERT INTO Est_EdNota (Cod_Estudiante, Cod_Edicion, Cod_Materia, Nota) VALUES (@Est, @Ed, @Mat, @Nota)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Est", nota.Cod_Estudiante);
                cmd.Parameters.AddWithValue("@Ed", nota.Cod_Edicion);
                cmd.Parameters.AddWithValue("@Mat", nota.Cod_Materia);
                cmd.Parameters.AddWithValue("@Nota", nota.Nota);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable ObtenerNotas()
        {
            using (SqlConnection con = new SqlConnection(cadenaConexion))
            {
                string query = "SELECT * FROM Est_EdNota";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public class EstEdNota
        {
            public int Cod_Estudiante { get; set; }
            public int Cod_Edicion { get; set; }
            public int Cod_Materia { get; set; }
            public decimal Nota { get; set; }
        }

    }
}
