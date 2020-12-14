using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Practica4
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-3TNU4KA\\MIKERSERVER;Initial Catalog=ClaseTopicos;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        int ID = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "" && txtUsuario.Text != "" && txtContra.Text != "")
            {
                cmd = new SqlCommand("insert into usuarios(id,usuario,contraseña) values(@id,@usuario,@contra)", conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@contra", txtContra.Text);
                cmd.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Datos insertados correctamente");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Incluya datos por favor");
            }
        }
        private void DisplayData()
        {
            conexion.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from usuarios", conexion);
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            conexion.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            txtId.Text = "";
            txtUsuario.Text = "";
            txtContra.Text = "";
            ID = 0;
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtUsuario.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtContra.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        //Update Record  
        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (txtId.Text != "" && txtUsuario.Text != "" && txtContra.Text != "")
            {
                cmd = new SqlCommand("update tbl_Record set Name=@name,State=@state where ID=@id", conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text);
                cmd.Parameters.AddWithValue("@contra", txtContra.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Informacion Actualizada correctamente");
                conexion.Close();
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("seleccione infromacion a modificar");
            }
        }
        //Delete Record  
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete usuarios where ID=@id", conexion);
                conexion.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Informacion eliminada correctamente !");
                DisplayData();
                ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }
    }
}
