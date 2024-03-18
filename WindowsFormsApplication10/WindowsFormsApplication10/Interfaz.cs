// developed by:
//            __                   __    _______    __                          __         ______            
//           /  |                _/  |  /       |  /  |                        /  |       /      \           
//   _______ $$ |____    ______ / $$ |  $$$$$$$/  _$$ |_     ______    ______  $$ |____  /$$$$$$  |  ______  
//  /       |$$      \  /      \$$$$ |  $$ |____ / $$   |   /      \  /      \ $$      \ $$ ___$$ | /      \ 
// /$$$$$$$/ $$$$$$$  |/$$$$$$  | $$ |  $$      \$$$$$$/   /$$$$$$  |/$$$$$$  |$$$$$$$  |  /   $$< /$$$$$$  |
// $$ |      $$ |  $$ |$$ |  $$/  $$ |  $$$$$$$  | $$ | __ $$ |  $$ |$$ |  $$ |$$ |  $$ | _$$$$$  |$$ |  $$/ 
// $$ \_____ $$ |  $$ |$$ |      _$$ |_ /  \__$$ | $$ |/  |$$ \__$$ |$$ |__$$ |$$ |  $$ |/  \__$$ |$$ |      
// $$       |$$ |  $$ |$$ |     / $$   |$$    $$/  $$  $$/ $$    $$/ $$    $$/ $$ |  $$ |$$    $$/ $$ |      
//  $$$$$$$/ $$/   $$/ $$/      $$$$$$/  $$$$$$/    $$$$/   $$$$$$/  $$$$$$$/  $$/   $$/  $$$$$$/  $$/       
//                                                                  $$ |                                    
//                                                                  $$ |                                    
//                                                                  $$/                                     
//https://github.com/Chr15toph3r
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaLecancer1
{

    public partial class LeCancerCompany_interfaz : Form
    {
        private List<Persona> listaEnfermos = new List<Persona>();
        private List<Persona> listaSanos = new List<Persona>();
        
        public LeCancerCompany_interfaz()
        {
            InitializeComponent();
        }
        //eventos
        private void Form1_Load(object sender, EventArgs e)
        {
            //mostrar primeros elementos del combobox en la interfaz
            cb_sexo.SelectedIndex = 0;
            Mostrar_Sexo.SelectedIndex = 0; 
            Cb_filtroedad.SelectedIndex= 0;

        }

        private void checkBox_CheckedChanged(object sender, EventArgs e) //metodo que controla los checkboxes como radiobuttons
        {
            if (sender == checkb_enfermo && checkb_enfermo.Checked)
            {
                checkb_noEnfermo.Checked = false; 
            }
            else if (sender == checkb_noEnfermo && checkb_noEnfermo.Checked)
            {
                checkb_enfermo.Checked = false;
            }
        }

//Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | 

        private void btn_registrar_Click(object sender, EventArgs e)
        {
        //sin validaciones por si quiero editar algo rapido
            //string nombre = txtb_nombre.Text;
            //string cedula = txtb_cedula.Text;
            //int edad = int.Parse(txtb_edad.Text);
            //string sexo = cb_sexo.SelectedItem.ToString();
            //string condicion = txtb_condicion.Text;
            //bool estaEnfermo = checkb_enfermo.Checked;
            //int deuda = int.Parse(txtb_deuda.Text);

            string nombre = txtb_nombre.Text;
            string cedula = txtb_cedula.Text;
            int edad;
            int deuda;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("Por favor, ingrese un nombre valido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cedula) || !cedula.All(char.IsDigit))
            {
                MessageBox.Show("Por favor, ingrese una cedula valida.");
                return;
            }


            if (!int.TryParse(txtb_edad.Text, out edad) || edad < 0)
            {
                MessageBox.Show("Por favor, ingrese una edad valida.");
                return;
            }

            if (cb_sexo.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un sexo.");
                return;
            }
            string sexo = cb_sexo.SelectedItem.ToString();

            if (!int.TryParse(txtb_deuda.Text, out deuda) || deuda < 0)
            {
                MessageBox.Show("Por favor, ingrese una cantidad valida de deuda.");
                return;
            }

            string condicion = txtb_condicion.Text;

            if (string.IsNullOrWhiteSpace(condicion))
            {
                MessageBox.Show("Por favor, ingrese una condicion valida.");
                return;
            }

            bool estaEnfermo = checkb_enfermo.Checked;

            Persona nuevaPersona = new Persona
            {
                Nombre = nombre,
                Cedula = cedula,
                Edad = edad,
                Sexo = sexo,
                Condicion = condicion,
                EstaEnfermo = estaEnfermo,
                Deuda = deuda
            };

            if (estaEnfermo)
            {
                listaEnfermos.Add(nuevaPersona);
            }
            else
            {
                listaSanos.Add(nuevaPersona);
            }

            ActualizarVistaDataGridView(); // Actualiza la vista después de añadir una persona
            ActualizarContadores();
        }

//Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | Boton registrado de datos | 

//Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  

        private void ActualizarVistaDataGridView()
        {
            dataGridView1.Rows.Clear();

            bool filtrarPorEnfermos = mostrarE.Checked;
            bool filtrarPorNoEnfermos = mostrarNE.Checked;
            int edadSeleccionada = -1;

            if (Cb_filtroedad.SelectedIndex > 0)
            {
                edadSeleccionada = int.Parse(Cb_filtroedad.SelectedItem.ToString());
            }

            string filtroSexo = Mostrar_Sexo.SelectedItem.ToString();

            IEnumerable<Persona> listaFiltrada = listaEnfermos.Concat(listaSanos);

            if (filtrarPorEnfermos)
            {
                listaFiltrada = listaFiltrada.Where(persona => persona.EstaEnfermo);
            }
            else if (filtrarPorNoEnfermos)
            {
                listaFiltrada = listaFiltrada.Where(persona => !persona.EstaEnfermo);
            }
            if (edadSeleccionada > 0)
            {
                listaFiltrada = listaFiltrada.Where(persona => persona.Edad >= edadSeleccionada && persona.Edad < edadSeleccionada + 5);
            }
            if (!string.IsNullOrEmpty(filtroSexo) && filtroSexo != "Todos")
            {
                listaFiltrada = listaFiltrada.Where(persona => persona.Sexo.Equals(filtroSexo, StringComparison.OrdinalIgnoreCase));
            }

            // Añadir las personas filtradas al DataGridView
            foreach (var persona in listaFiltrada)
            {
                dataGridView1.Rows.Add(new object[] { persona.Nombre, persona.Cedula, persona.Edad, persona.Sexo, persona.Condicion, persona.EstaEnfermo ? "Si" : "No", persona.Deuda });
            }
        }

//Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  Actualizador datagrid |  

        private void ActualizarContadores()
        {
            // Calcular el total de personas
            int totalPersonas = listaEnfermos.Count + listaSanos.Count;

            // El total de personas enfermas ya lo tenemos en listaEnfermos.Count
            int totalEnfermos = listaEnfermos.Count;

            // El total de personas no enfermas ya lo tenemos en listaSanos.Count
            int totalNoEnfermos = listaSanos.Count;

            // Actualizar los Labels con los números
            lblTotalPersonas.Text = totalPersonas.ToString();
            lblTotalEnfermos.Text = totalEnfermos.ToString();
            lblTotalNoEnfermos.Text = totalNoEnfermos.ToString();
        }

        private void mostrarE_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVistaDataGridView();
        }

        private void mostrarNE_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarVistaDataGridView();
        }

        private void Mostrar_Sexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVistaDataGridView();
        }

        private void Cb_filtroedad_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarVistaDataGridView();
        }
    }
}

//muchas gracias por su atencion :)
