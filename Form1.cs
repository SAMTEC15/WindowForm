using System.Data;
using System.Data.SqlClient;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        DataAccessDb dataAccess = new DataAccessDb();
        public Form1()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            showAll();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add();
        }


        public void Add()
        {
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please input all fields");
                return;
            }
            string sql = "usp_todoapp '','" + textBox1.Text + "','" + dateTimePicker1.Value + "','N','" + textBox3.Text + "','ADD'";
            DataSet ds = dataAccess.fillDataSet(sql);
            showAll();
        }
        public void Delete()
        {

        }
        public void Update()
        {

        }
        public void showAll()
        {
            string sql = "usp_todoapp '','','','','','VIEW'";
            DataSet ds = dataAccess.fillDataSet(sql);
            dataGridView1.DataSource = ds.Tables[0];
        }


        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }


    public class DataAccessDb
    {
        public DataSet fillDataSet(string sql)
        {
            DataSet dset = new DataSet();
            SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=ToDoListDb;Trusted_Connection=True");
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dset);
            return dset;
        }
    }

}