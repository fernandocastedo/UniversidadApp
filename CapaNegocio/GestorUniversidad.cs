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
                // Consulta para obtener la carrera del estudiante (utilizando Cod_Carrera)
                string consulta = $"SELECT c.Nombre_Carrera FROM Persona p " +
                                  $"JOIN Carrera c ON p.Cod_Carrera = c.Cod_Carrera " +
                                  $"WHERE p.CI = '{nombreEstudiante}'";

                DataTable dt = accesoDatos.EjecutarConsulta(consulta);

                // Verificamos si se obtuvo el resultado
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["Nombre_Carrera"].ToString(); // Devuelve el nombre de la carrera
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

                // Obtener la edición de la materia para la inscripción
                string consultaEdicion = $"SELECT Cod_Edicion FROM Edicion_Materia WHERE Cod_Materia = {codMateria} ORDER BY Cod_Edicion DESC LIMIT 1";
                DataTable dtEdicion = accesoDatos.EjecutarConsulta(consultaEdicion);

                if (dtEdicion.Rows.Count == 0)
                {
                    throw new Exception("No se encontró una edición para la materia.");
                }

                int codEdicion = (int)dtEdicion.Rows[0]["Cod_Edicion"];

                // Consulta para insertar la inscripción del estudiante en la materia
                string consulta = $"INSERT INTO Est_EdNota (Cod_Estudiante, Cod_Edicion, Cod_Materia, Nota) " +
                                  $"VALUES ({codEstudiante}, {codEdicion}, {codMateria}, NULL)"; // Aquí NULL para la nota inicial

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

                // Obtener la edición de la materia para eliminar la inscripción
                string consultaEdicion = $"SELECT Cod_Edicion FROM Edicion_Materia WHERE Cod_Materia = {codMateria} ORDER BY Cod_Edicion DESC LIMIT 1";
                DataTable dtEdicion = accesoDatos.EjecutarConsulta(consultaEdicion);

                if (dtEdicion.Rows.Count == 0)
                {
                    throw new Exception("No se encontró una edición para la materia.");
                }

                int codEdicion = (int)dtEdicion.Rows[0]["Cod_Edicion"];

                // Consulta para eliminar la inscripción del estudiante en la materia
                string consulta = $"DELETE FROM Est_EdNota WHERE Cod_Estudiante = {codEstudiante} AND Cod_Edicion = {codEdicion} AND Cod_Materia = {codMateria}";

                accesoDatos.EjecutarConsulta(consulta); // Ejecutamos la consulta de eliminación
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la inscripción: {ex.Message}");
            }
        }
    }
}
