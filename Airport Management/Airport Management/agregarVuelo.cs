﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Airport_Management.Clases;

namespace Airport_Management
{
    public partial class agregarVuelo : Form
    {
        GestionVuelos gv = new GestionVuelos();

        public agregarVuelo()
        {
            InitializeComponent();
        }

        private void activarbotonagregar()
        {
            bool estado = true;
            if (cmb_Ruta.Text.Equals(""))
            {
                estado = false;
            }
            if (gv.VueloExiste(txtVuelo.Text) || txtVuelo.Text == "")
            {
                estado = false;
            }
            btnAgregar.Enabled = estado;


        }

        private void agregarVuelo_Load(object sender, EventArgs e)
        {
            AccesoDatos ac = new AccesoDatos();
            string consulta = "select codigo_RTA from rutas";
            ac.AgregaraComboBox(consulta, ref cmb_Ruta);

            timer_Fecha.Format = DateTimePickerFormat.Custom;
            timer_Fecha.CustomFormat = "dd'/'MM'/'yy 'a las ' HH:mm:ss 'Horas' ";
            timer_Fecha.MinDate = DateTime.Today;

            //activarbotonagregar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (cmb_Ruta.Text == "" || txtVuelo.Text == "")
            {
                MessageBox.Show("Todos los campos deben estar completos");
                txtVuelo.Text = "";
                cmb_Ruta.Text = "";
            }
            else if (gv.VueloExiste(txtVuelo.Text))
                MessageBox.Show("El código de vuelo ya existe, modificar");
            
            else
            {
                string prov = timer_Fecha.Value.ToString();
                string[] fecha = prov.Split();

                if (fecha[2] == "a.m.") fecha[2] = "am";
                else fecha[2] = "pm";
                string consulta = "INSERT INTO vuelos (codigo_VLO, codigo_RTA, fecha_salida_VLO, sincronizado_VLO) SELECT '" + txtVuelo.Text + "', '" + cmb_Ruta.Text + "', '" + fecha[0] + " " + fecha[1] + " " + fecha[2] + "', 0";

                AccesoDatos ac = new AccesoDatos();
                ac.EjecutarConsulta(consulta);

                MessageBox.Show("Vuelo agregado con éxito");
                //MessageBox.Show(fecha[0]);
                //MessageBox.Show(fecha[1]);
                //MessageBox.Show(fecha[2]);

                txtVuelo.Text = "";
                cmb_Ruta.Text = "";
            }
        }

        private void txtVuelo_Leave(object sender, EventArgs e)
        {
            //activarbotonagregar();
        }

        private void cmb_Ruta_TextChanged(object sender, EventArgs e)
        {
            //activarbotonagregar();
        }

    }
}
