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
        private GestorUniversidad datos = new GestorUniversidad();
        private DataTable materiasSeleccionadas = new DataTable();

        public MainWindow()
        {
            InitializeComponent();
            materiasSeleccionadas.Columns.Add("Cod_Materia", typeof(int));
            materiasSeleccionadas.Columns.Add("Nombre", typeof(string));
            materiasSeleccionadas.Columns.Add("Dia", typeof(string));
            materiasSeleccionadas.Columns.Add("Hora_Inicio", typeof(string));
            materiasSeleccionadas.Columns.Add("Hora_Fin", typeof(string));
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbEstudiante.ItemsSource = datos.EjecutarConsulta("SELECT Cod_Estudiante FROM Estudiante").DefaultView;
            cbEstudiante.DisplayMemberPath = "Cod_Estudiante";
        }

        private void cbEstudiante_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEstudiante.SelectedItem == null) return;
            int codEstudiante = Convert.ToInt32((cbEstudiante.SelectedItem as DataRowView)["Cod_Estudiante"]);
            dgDisponibles.ItemsSource = datos.ObtenerMateriasConHorario(codEstudiante).DefaultView;
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (dgDisponibles.SelectedItem == null || materiasSeleccionadas.Rows.Count >= 6) return;

            var row = (DataRowView)dgDisponibles.SelectedItem;
            int cod = Convert.ToInt32(row["Cod_Materia"]);

            if (!materiasSeleccionadas.AsEnumerable().Any(r => (int)r["Cod_Materia"] == cod))
            {
                materiasSeleccionadas.Rows.Add(row["Cod_Materia"], row["Nombre"], row["Dia"], row["Hora_Inicio"], row["Hora_Fin"]);
                dgSeleccionadas.ItemsSource = materiasSeleccionadas.DefaultView;
            }
        }

        private void BtnQuitar_Click(object sender, RoutedEventArgs e)
        {
            if (dgSeleccionadas.SelectedItem == null) return;
            var row = (DataRowView)dgSeleccionadas.SelectedItem;
            materiasSeleccionadas.Rows.Remove(row.Row);
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Materias seleccionadas: " + materiasSeleccionadas.Rows.Count);
            // Aquí insertarías la lógica para guardar en BD con validaciones adicionales.
        }
    }
}