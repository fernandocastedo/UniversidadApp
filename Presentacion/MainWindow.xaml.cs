using CapaNegocio;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CargarDatos();
        }
        private void CargarDatos()
        {
            GestorUniversidad gestor = new GestorUniversidad()           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var estudiantes = datos.EjecutarConsulta("SELECT Cod_Estudiante FROM Estudiante");
            cbEstudiante.ItemsSource = estudiantes.AsDataView();
            cbEstudiante.DisplayMemberPath = "Cod_Estudiante";

            cbAnio.ItemsSource = datos.EjecutarConsulta("SELECT DISTINCT Año FROM Gestion").AsDataView();
            cbAnio.DisplayMemberPath = "Año";

            cbSemestre.ItemsSource = datos.EjecutarConsulta("SELECT DISTINCT Semestre FROM Gestion").AsDataView();
            cbSemestre.DisplayMemberPath = "Semestre";

            cbGrupo.ItemsSource = datos.EjecutarConsulta("SELECT Cod_Grupo, Nombre FROM Grupo").AsDataView();
            cbGrupo.DisplayMemberPath = "Nombre";
            cbGrupo.SelectedValuePath = "Cod_Grupo";
        }
        private void cbEstudiante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEstudiante.SelectedItem == null) return;
            int codEstudiante = Convert.ToInt32((cbEstudiante.SelectedItem as DataRowView)["Cod_Estudiante"]);

            var materias = datos.EjecutarConsulta($@"
            SELECT m.Cod_Materia, m.Nombre 
            FROM Plan_Estudiante pe
            JOIN Plan_Materia pm ON pe.Cod_PlanEstudio = pm.Cod_PlanEstudio
            JOIN Materia m ON pm.Cod_Materia = m.Cod_Materia
            WHERE pe.Cod_Estudiante = {codEstudiante}");

            lbMaterias.ItemsSource = materias.AsDataView();
            lbMaterias.DisplayMemberPath = "Nombre";
            lbMaterias.SelectedValuePath = "Cod_Materia";
        }
        private void BtnInscribir_Click(object sender, RoutedEventArgs e)
        {
            int codEstudiante = Convert.ToInt32(cbEstudiante.Text);
            int anio = Convert.ToInt32(cbAnio.Text);
            string semestre = cbSemestre.Text;
            int codGrupo = Convert.ToInt32(cbGrupo.SelectedValue);

            foreach (DataRowView materia in lbMaterias.SelectedItems)
            {
                int codMateria = Convert.ToInt32(materia["Cod_Materia"]);

                // Verifica si ya existe la Edicion
                string queryEdicion = $@"
            SELECT Cod_Edicion FROM Edicion
            WHERE Cod_Materia = {codMateria} AND Año = {anio} AND Semestre = '{semestre}' AND Cod_Grupo = {codGrupo}";

                var dtEd = datos.EjecutarConsulta(queryEdicion);
                int codEdicion;

                if (dtEd.Rows.Count > 0)
                {
                    codEdicion = Convert.ToInt32(dtEd.Rows[0]["Cod_Edicion"]);
                }
                else
                {
                    // Insertar nueva Edición
                    datos.EjecutarConsulta($"INSERT INTO Edicion (Cod_Edicion, Año, Semestre, Cod_Grupo, Cod_Materia) VALUES (NEXT VALUE FOR Seq_Edicion, {anio}, '{semestre}', {codGrupo}, {codMateria})");
                    dtEd = datos.EjecutarConsulta(queryEdicion);
                    codEdicion = Convert.ToInt32(dtEd.Rows[0]["Cod_Edicion"]);
                }

                // Insertar en Asistencia
                datos.EjecutarConsulta($"INSERT INTO Asistencia (Cod_Estudiante, Cod_Edicion) VALUES ({codEstudiante}, {codEdicion})");
            }

            MessageBox.Show("Inscripción realizada.");
        }


    }
}