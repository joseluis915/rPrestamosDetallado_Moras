using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Añadí estos using
using rPrestamosDetallado_Moras.BLL;
using rPrestamosDetallado_Moras.Entidades;


namespace rPrestamosDetallado_Moras.UI.Registros
{
    public partial class rMoras : Window
    {
        private Moras moras = new Moras();
        public rMoras()
        {
            InitializeComponent();
            //Constructor
            this.DataContext = moras;
        }
        //----------------------------------[ CARGAR - Registro Detallado ]----------------------------------
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = moras;
        }
        //=====================================================[ LIMPIAR ]=====================================================
        private void Limpiar()
        {
            this.moras = new Moras();
            this.DataContext = moras;
        }
        //=====================================================[ Validar ]=====================================================
        private bool Validar()
        {
            bool Validado = true;
            if (MoraIdTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Errada", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            return Validado;

        }
        //=====================================================[ BUSCAR ]=====================================================
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            //----------------------------------[ BUSCAR - Registro Detallado ]----------------------------------
            Moras encontrado = MorasBLL.Buscar(moras.MoraId);

            if (encontrado != null)
            {
                moras = encontrado;
                Cargar();
                MessageBox.Show("Articulo Encontrado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show($"Esta Id de Articulo no fue encontrada.\n\nAsegurese que existe o cree una nueva.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
            }
        }
        //----------------------------------[ AGREGAR FILA - Registro Detallado ]----------------------------------
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            var filaDetalle = new MorasDetalle(Convert.ToInt32(IdTextBox.Text), moras.MoraId, Convert.ToInt32(PrestamoIdDetalleTextBox.Text), FechaDetalleaDataPicker.DisplayDate, Convert.ToSingle(ValorTextBox.Text));

            moras.Detalle.Add(filaDetalle);
            Cargar();

            IdTextBox.Clear();
            ValorTextBox.Clear();
        }
        //----------------------------------[ REMOVER FILA - Registro Detallado ]----------------------------------
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                moras.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
            }
        }
        //=====================================================[ NUEVO ]=====================================================
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //=====================================================[ GUARDAR ]=====================================================
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                var paso = MorasBLL.Guardar(moras);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transaccion Exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transaccion Errada", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //=====================================================[ ELIMINAR ]=====================================================
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (MorasBLL.Eliminar(Utilidades.ToInt(MoraIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
