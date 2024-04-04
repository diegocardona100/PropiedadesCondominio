using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropiedadesCondominio
{
    public partial class Form1 : Form
    {
        private List<Propietario> propietarios = new List<Propietario>();
        private List<Propiedades> propiedades = new List<Propiedades>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            CargarDatosPropietarios();
            CargarDatosPropiedades();
            MostrarDatosGridView();
        }

        private void CargarDatosPropietarios()
        {
            
            propietarios.Add(new Propietario { DPI = "123456789", Nombre = "Juan", Apellido = "Pérez" });
            propietarios.Add(new Propietario { DPI = "987654321", Nombre = "María", Apellido = "López" });
        }

        private void CargarDatosPropiedades()
        {
            
            propiedades.Add(new Propiedades { NumeroCasa = 1, DPIPropietario = "123456789", CuotaMantenimiento = 200 });
            propiedades.Add(new Propiedades { NumeroCasa = 2, DPIPropietario = "987654321", CuotaMantenimiento = 300 });
        }

        private void MostrarDatosGridView()
        {
            var query = from prop in propiedades
                        join propietario in propietarios on prop.DPIPropietario equals propietario.DPI
                        orderby prop.CuotaMantenimiento
                        select new
                        {
                            propietario.Nombre,
                            propietario.Apellido,
                            prop.NumeroCasa,
                            prop.CuotaMantenimiento
                        };

            dataGridView1.DataSource = query.ToList();
        }

        private void btnMostrarAltasBajas_Click(object sender, EventArgs e)
        {
            MostrarTresMasAltas();
            MostrarTresMasBajas();
            MostrarMayorCuotaTotal();
        }

        private void MostrarTresMasAltas()
        {
            var tresMasAltas = propiedades.OrderByDescending(p => p.CuotaMantenimiento).Take(3);
            MessageBox.Show("Tres cuotas más altas:\n" + string.Join("\n", tresMasAltas.Select(p => $"{p.NumeroCasa}: {p.CuotaMantenimiento}")));
        }

        private void MostrarTresMasBajas()
        {
            var tresMasBajas = propiedades.OrderBy(p => p.CuotaMantenimiento).Take(3);
            MessageBox.Show("Tres cuotas más bajas:\n" + string.Join("\n", tresMasBajas.Select(p => $"{p.NumeroCasa}: {p.CuotaMantenimiento}")));
        }

        private void MostrarMayorCuotaTotal()
        {
            var mayorCuotaTotal = propiedades.OrderByDescending(p => p.CuotaMantenimiento).FirstOrDefault();
            var propietario = propietarios.FirstOrDefault(prop => prop.DPI == mayorCuotaTotal?.DPIPropietario);

            if (propietario != null)
            {
                MessageBox.Show($"Propietario con la cuota total más alta:\n{propietario.Nombre} {propietario.Apellido}\nCuota Total: {mayorCuotaTotal.CuotaMantenimiento}");
            }
        }
    }
}
