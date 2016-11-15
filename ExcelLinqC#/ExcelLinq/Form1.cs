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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string cn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DatosExcel.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=0'";
        public DataTable Datos()
        {
            DataTable dt = new DataTable(); 
            dt.Columns.Add("Id");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Precio", typeof(int));
            using (OleDbConnection cnn = new OleDbConnection(cn))
            {
                string sql = "SELECT *FROM [Hoja1$]";

                OleDbCommand command = new OleDbCommand(sql, cnn);
                OleDbDataAdapter da = new OleDbDataAdapter(command);
                da.Fill(dt);
                return dt;
            }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "")
            {
                MessageBox.Show("Ingresar datos correctamente...","Error",MessageBoxButtons.OK);

            }
            else
            {
                using (OleDbConnection cnn = new OleDbConnection(cn))
                {

                    using (OleDbCommand cmd = cnn.CreateCommand())
                    {
                        cnn.Open();
                        cmd.CommandText = "INSERT INTO [Hoja1$] (Id,Nombre,Precio) values(@id,@nom,@pre)";
                        cmd.Parameters.AddWithValue("@id", textBox1.Text);
                        cmd.Parameters.AddWithValue("@nom", textBox2.Text);
                        cmd.Parameters.AddWithValue("@pre", textBox3.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Datos Ingresados Correctamente...");
                        cnn.Close();
                    }
                }
                CargarDatos();
                Limpiar();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = Datos();
            int precio = Convert.ToInt32(txtbuscar.Text);
            var query = from datos in dt.AsEnumerable() where datos.Field<int>("Precio") >= precio select datos;
            //var query = dt.AsEnumerable().Where(datos=> datos.Field<int>("Sueldo") >= sueldo);
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
