using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using RegistroCompletoDAL;

namespace RegistroCompleto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Personas persona = new Personas();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = persona;
        }

        

        public void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Contexto context = new Contexto();

            var found = context.Personas.Find(Convert.ToInt32(IdTextBox.Text));

            if(found != null){
                persona = found;
                NombreTextBox.Text = persona.Nombre;
                TelefonoTextBox.Text = Convert.ToString(persona.Telefono);
                CedulaTextBox.Text = Convert.ToString(persona.Cedula);
                DireccionTextBox.Text = Convert.ToString(persona.Direccion);
                FechaDateTimePicker.Text = Convert.ToString(persona.FechaNacimiento);
                
            }   

            context.Dispose();
        }

        private void Limpiar()
        {
            this.persona = new Personas();
            this.DataContext = persona;
        }
        public void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        public void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
           Contexto context = new Contexto();
            context.Personas.Add(persona);
            int cant = context.SaveChanges();

            context.Dispose();

            if(cant > 0){
                MessageBox.Show("Guardo !");
                persona = new Personas();
                DataContext = persona;
            }
        }

        public static bool Eliminar(int id){
            bool eliminado = false;

            Contexto context = new Contexto();

            try{
                var persona = context.Personas.Find(id);

                if(persona != null){
                    context.Personas.Remove(persona);
                    eliminado = context.SaveChanges() > 0;
                } 
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                context.Dispose();
            }

            return eliminado;
        }
        public void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            Contexto context = new Contexto();


               if( Eliminar(Convert.ToInt32(IdTextBox.Text))){
                    MessageBox.Show(" Borro !");
                    Limpiar();
               }
                
           
        }
    }



    

}
