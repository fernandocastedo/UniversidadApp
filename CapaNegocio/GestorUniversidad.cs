using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class GestorUniversidad
    {
        private AccesoDatos datos = new AccesoDatos();

        public DataTable ObtenerEstudiantes()
        {
            string consulta = "SELECT * FROM Estudiante"; // Usa tu tabla real
            return datos.EjecutarConsulta(consulta);
        }
    }
}
