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

        public DataTable ObtenerMateriasConHorario(int codEstudiante)
        {
            return datos.EjecutarConsulta($@"
                SELECT TOP 6 m.Cod_Materia, m.Nombre, h.Dia, h.Hora_Inicio, h.Hora_Fin
                FROM Plan_Estudiante pe
                JOIN Plan_Materia pm ON pe.Cod_PlanEstudio = pm.Cod_PlanEstudio
                JOIN Materia m ON pm.Cod_Materia = m.Cod_Materia
                LEFT JOIN Edicion e ON e.Cod_Materia = m.Cod_Materia
                LEFT JOIN Edicion_Aula ea ON e.Cod_Edicion = ea.Cod_Edicion
                LEFT JOIN Aula_Horario ah ON ea.Cod_Aula = ah.Cod_Aula
                LEFT JOIN Horario h ON ah.Cod_Horario = h.Cod_Horario
                WHERE pe.Cod_Estudiante = {codEstudiante}
                ORDER BY m.Nombre");
        }

        public DataTable EjecutarConsulta(string consulta)
        {
            return datos.EjecutarConsulta(consulta);
        }

    }
}
