using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;

namespace ExcelLinq
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        string cn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatosExcel.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=0'";
        public DataTable Datos()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("RUC");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Direccion");
            dt.Columns.Add("Correo");
            using (OleDbConnection cnn = new OleDbConnection(cn))
            {
                string sql = "SELECT *FROM [Hoja2$]";

                OleDbCommand command = new OleDbCommand(sql, cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
                return dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "")
            {
                MessageBox.Show("Rellenar Todos los campos...");
            }
            else
            {
                using (OleDbConnection cnn = new OleDbConnection(cn))
                {

                    using (OleDbCommand cmd = cnn.CreateCommand())
                    {
                        cnn.Open();
                        cmd.CommandText = "INSERT INTO [Hoja2$] (Codigo,RUC,Nombre,Direccion,Correo) values(@cod,@ruc,@nom,@dir,@email)";
                        cmd.Parameters.AddWithValue("@cod", textBox1.Text);
                        cmd.Parameters.AddWithValue("@ruc", textBox2.Text);
                        cmd.Parameters.AddWithValue("@nom", textBox3.Text);
                        cmd.Parameters.AddWithValue("@dir", textBox5.Text);
                        cmd.Parameters.AddWithValue("@email", textBox6.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Datos Ingresados Correctamente...");
                        cnn.Close();
                    }
                }
                CargarDatos();
                Limpiar();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        public void CargarDatos()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = Datos();
        }
        public void Limpiar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = Datos();
            string nombre = txtbuscar.Text;
            var query = from datos in dt.AsEnumerable() where datos.Field<string>("Nombre") == "%"+nombre+"%" select datos;
            if (query.Count() > 0)
            {
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = query.CopyToDataTable();
            }
            else
                MessageBox.Show("No Se Encontraron Registros");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
