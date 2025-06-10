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
        private GestorUniversidad gestor;
        private List<MateriaViewModel> materiasSeleccionadas;
        public MainWindow()
        {
            InitializeComponent();
            gestor = new GestorUniversidad();  // Inicializamos el gestor para la lógica de negocio
            materiasSeleccionadas = new List<MateriaViewModel>();
        }
        private void BuscarEstudiante_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nombreEstudiante = txtNombreEstudiante.Text;
                if (string.IsNullOrEmpty(nombreEstudiante))
                {
                    MessageBox.Show("Por favor, ingrese el nombre del estudiante.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Llamamos al método de la capa de negocio para obtener la carrera del estudiante
                string carrera = gestor.ObtenerCarreraPorEstudiante(nombreEstudiante);

                if (string.IsNullOrEmpty(carrera))
                {
                    MessageBox.Show("Estudiante no encontrado o sin carrera asignada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Mostrar la carrera en el TextBox correspondiente
                txtCarrera.Text = carrera;

                // Cargar las materias disponibles
                CargarMaterias();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Cargar materias disponibles
        private void CargarMaterias()
        {
            try
            {
                // Obtenemos las materias del negocio
                var materias = gestor.ObtenerMaterias().AsEnumerable().Select(r => new MateriaViewModel
                {
                    Cod_Materia = r.Field<int>("Cod_Materia"),
                    Nombre = r.Field<string>("Nombre"),
                    Credito = r.Field<int>("Credito"),
                    IsSelected = false  // Inicialmente no está seleccionada
                }).ToList();

                dgMaterias.ItemsSource = materias;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las materias: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Añadir inscripción de materia
        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar que se haya ingresado un código de edición
                if (string.IsNullOrEmpty(txtCodigoEdicion.Text))
                {
                    MessageBox.Show("Por favor, ingrese el código de edición.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                int codigoEdicion = int.Parse(txtCodigoEdicion.Text);
                var materiasSeleccionadas = dgMaterias.ItemsSource.Cast<MateriaViewModel>()
                                            .Where(m => m.IsSelected)
                                            .ToList();

                // Verificar que no se superen las 6 materias por edición
                if (materiasSeleccionadas.Count > 6)
                {
                    MessageBox.Show("No puede seleccionar más de 6 materias.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Inscribir las materias seleccionadas
                foreach (var materia in materiasSeleccionadas)
                {
                    gestor.InscribirMateria(txtNombreEstudiante.Text, materia.Cod_Materia);
                }

                MessageBox.Show("Las materias han sido inscritas exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al añadir la inscripción: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Modelo de la materia con estado de selección
        public class MateriaViewModel
        {
            public int Cod_Materia { get; set; }
            public string Nombre { get; set; }
            public int Credito { get; set; }
            public bool IsSelected { get; set; }
        }

        private void Eliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}