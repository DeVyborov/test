using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_vyborov.Resoursec;

namespace test_vyborov
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        SqlConnection str = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=dataTest;Integrated Security=True");
        string data;

        private void TestForm_Load(object sender, EventArgs e)
        {
            str.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM work", str);
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                data = reader[1].ToString();
            }
            label1.Text = Convert.ToDateTime(data).ToShortDateString();
            reader.Close();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM work", str);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            SqlDataAdapter a = new SqlDataAdapter("SELECT views AS 'W', id FROM work", str);
            DataSet d = new DataSet();
            a.Fill(d);
            comboBox1.DataSource = d.Tables[0];
            comboBox1.DisplayMember = "W";
            comboBox1.ValueMember = "id";

            pictureBox1.Image = image_list.img_4;

            str.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           MessageBox.Show(dataGridView1.CurrentRow.Cells[0].Value.ToString());

            Form1 form1 = new Form1();  
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            str.Open();
            SqlCommand command = new SqlCommand("INSERT INTO work (date, views, sub) VALUES ('12-12-2002', 123123, 123123)", str);
            command.ExecuteNonQuery();
            str.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            str.Open();
            SqlCommand command = new SqlCommand("UPDATE work SET views = 777 WHERE sub = 123123", str);
            command.ExecuteNonQuery();
            str.Close();
        }
    }
}
