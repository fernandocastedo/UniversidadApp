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
        private AccesoDatos accesoDatos;

        // Constructor que inicializa la clase de acceso a datos
        public GestorUniversidad()
        {
            accesoDatos = new AccesoDatos();
        }

        // Método para obtener la carrera de un estudiante
        public string ObtenerCarreraPorEstudiante(string nombreEstudiante)
        {
            try
            {
                // Consulta para obtener la carrera del estudiante
                string consulta = $"SELECT Carrera FROM Persona WHERE CI IN " +
                                  $"(SELECT CI FROM Estudiante WHERE Cod_Estudiante = " +
                                  $"(SELECT Cod_Estudiante FROM Estudiante WHERE CI = '{nombreEstudiante}'))";

                DataTable dt = accesoDatos.EjecutarConsulta(consulta);

                // Verificamos si se obtuvo el resultado
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Carrera"].ToString(); // Devuelve la carrera del estudiante
                }
                else
                {
                    return null; // Si no se encuentra el estudiante, retornamos null
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la carrera: {ex.Message}");
            }
        }

        // Método para obtener las materias disponibles
        public DataTable ObtenerMaterias()
        {
            try
            {
                // Consulta para obtener las materias disponibles
                string consulta = "SELECT Cod_Materia, Nombre, Credito FROM Materia";
                return accesoDatos.EjecutarConsulta(consulta); // Retorna las materias como un DataTable
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las materias: {ex.Message}");
            }
        }

        // Método para inscribir a un estudiante en una materia
        public void InscribirMateria(string nombreEstudiante, int codMateria)
        {
            try
            {
                // Obtener el código del estudiante
                string consultaEstudiante = $"SELECT Cod_Estudiante FROM Estudiante WHERE CI = '{nombreEstudiante}'";
                DataTable dtEstudiante = accesoDatos.EjecutarConsulta(consultaEstudiante);

                if (dtEstudiante.Rows.Count == 0)
                {
                    throw new Exception("Estudiante no encontrado");
                }

                int codEstudiante = (int)dtEstudiante.Rows[0]["Cod_Estudiante"];

                // Consulta para insertar la inscripción del estudiante en la materia
                string consulta = $"INSERT INTO Est_EdNota (Cod_Estudiante, Cod_Materia, Nota) " +
                                  $"VALUES ({codEstudiante}, {codMateria}, NULL)"; // Aquí NULL para la nota inicial

                accesoDatos.EjecutarConsulta(consulta); // Ejecutamos la consulta de inscripción
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al inscribir la materia: {ex.Message}");
            }
        }

        // Método para eliminar la inscripción de un estudiante en una materia
        public void EliminarInscripcion(string nombreEstudiante, int codMateria)
        {
            try
            {
                // Obtener el código del estudiante
                string consultaEstudiante = $"SELECT Cod_Estudiante FROM Estudiante WHERE CI = '{nombreEstudiante}'";
                DataTable dtEstudiante = accesoDatos.EjecutarConsulta(consultaEstudiante);

                if (dtEstudiante.Rows.Count == 0)
                {
                    throw new Exception("Estudiante no encontrado");
                }

                int codEstudiante = (int)dtEstudiante.Rows[0]["Cod_Estudiante"];

                // Consulta para eliminar la inscripción del estudiante en la materia
                string consulta = $"DELETE FROM Est_EdNota WHERE Cod_Estudiante = {codEstudiante} AND Cod_Materia = {codMateria}";

                accesoDatos.EjecutarConsulta(consulta); // Ejecutamos la consulta de eliminación
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la inscripción: {ex.Message}");
            }
        }
    }
}
