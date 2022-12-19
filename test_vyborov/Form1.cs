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
using test_vyborov.Class;

namespace test_vyborov
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // подключение к базе данных
        SqlConnection str = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=dataTest;Integrated Security=True");
        
        // объявление переменных
        string x_1 = "";
        string x_2 = "";
        string x_3 = "";
        string x_4 = "";

        // создание события при загрузке формы
        private void Form1_Load(object sender, EventArgs e)
        {
            this.workTableAdapter.Fill(this.dataTestDataSet.work); // метод заполнения combobox из базы данных
            try // обработка исключений
            {
                str.Open(); // открытие соединения с базой данных

                SqlCommand command = new SqlCommand("SELECT * FROM work WHERE id = 7", str); // запрос к базе данных
                SqlDataReader reader = command.ExecuteReader(); // объявление читателя для получения данных

                while (reader.Read()) // чтение данных из базы и запись их в переменную
                {
                    x_1 = reader[0].ToString();
                    x_2 = reader[1].ToString();
                    x_3 = reader[2].ToString();
                    x_4 = reader[3].ToString();
                }
                reader.Close(); // закрытие читателя

                // вывод значений
                label2.Text = x_1;
                label1.Text = Convert.ToDateTime(x_2).ToShortDateString(); // установка формата данных даты (без 00:00.00)             
                label3.Text = x_3;
                label4.Text = x_4;

                GetDataGrid(""); // вызов метода по отображению данных в таблице


                SqlDataAdapter a = new SqlDataAdapter("SELECT views AS 'name_table', id FROM work", str); // запрос к базе данных

                DataSet ds = new DataSet(); // объявление компонента
                a.Fill(ds); // фильтрация данных

                comboBox2.DataSource = ds.Tables[0]; // установка данных
                comboBox2.DisplayMember = "name_table"; // вывод данных из указанной таблицы
                comboBox2.ValueMember = "id"; // счетчик

                pictureBox1.Load(@"C:\Users\User\OneDrive\Рабочий стол\test_vyborov\test_vyborov\image\img_1.png"); // получение фотографий из файлов
                pictureBox2.Image = image_list.img_5; // получение фотографий из ресурсов
            }
            catch (Exception ex) // обработка исключения
            {
                MessageBox.Show(ex.Message);
            }           
        }

        public void GetDataGrid(string type) // метод для вывода данных в таблицу
        {
            SqlDataAdapter adapter = new SqlDataAdapter($"SELECT * FROM work {type}", str); // объявление адаптера и запроса к базе
            DataSet ds = new DataSet(); // объявление dataset

            adapter.Fill(ds); // фильтрация
            dtView.DataSource = ds.Tables[0]; // вывод данных          
        }

        private void btn1_CheckedChanged(object sender, EventArgs e)
        {
            GetDataGrid("WHERE views > 100000"); // фильтрация таблицы (значение просмотров больше 100к)
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GetDataGrid("WHERE views < 100000"); // фильтрация таблицы (значение просмотров меньше 100к)
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // открытие новой формы
            newform nfr = new newform(x_4);
            nfr.Show(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // получение данных из класса в папке
            TestClass cls = new TestClass();
            MessageBox.Show(cls.GetBalls());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // получение данных из выбранной строки в таблице
            MessageBox.Show(dtView.CurrentRow.Cells[0].Value.ToString());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // закрытие соединения с бд при выходе из приложения
            str.Close();
        }
    }
}
