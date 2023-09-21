using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Windows.Forms;

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
            if (idtxt.Text == "")
            {
                MessageBox.Show("Select record to update");
                return;
            }

            string sql = "ToDoListDb '" + idtxt.Text + "','" + textBox1.Text + "','" + dateTimePicker1.Value + "','" + comboBox1.SelectedItem.ToString() + "','" + textBox3.Text + "','UPDATE'";
            DataAccessDb.fillDataSet(sql);
            Reset();
            showAll();
        }

        private void button1_Click(object sender, EventArgs e) //save btn
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
            string sql = "ToDoListDb '','" + textBox1.Text + "','" + dateTimePicker1.Value + "','" + comboBox1.SelectedItem.ToString() + "','" + textBox3.Text + "','ADD'";
            DataAccessDb.fillDataSet(sql);
            showAll();
        }
        public void Delete()
        {

        }


        DataGridViewCheckBoxColumn check;
        DataGridViewButtonColumn button;
        public void showAll()
        {
            check = new DataGridViewCheckBoxColumn();
            check.Name = "Check";
            check.HeaderText = "Select";

            button = new DataGridViewButtonColumn();
            button.Name = "Edit";
            button.HeaderText = "Edit";
            button.Text = "Edit";
            button.UseColumnTextForButtonValue = true;

            string sql = "ToDoListDb '','','','','','VIEW'";
            DataSet ds = DataAccessDb.fillDataSet(sql);
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(check);

           // dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns.Add(button);
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

        List<string> checkedIn = new List<string>();

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            dataGridView1.EndEdit();
            try
            {
                if (e.ColumnIndex == 0)
                {

                    string? comp = dataGridView1["Check", e.RowIndex].Value.ToString();
                    string? id = dataGridView1["Id", e.RowIndex].Value.ToString();
                    if (comp == "True")
                    {
                        checkedIn.Add(id);
                    }
                    else
                    {
                        if (checkedIn.Contains(id))
                        {
                            checkedIn.Remove(id);
                        }
                    }

                }

                if (e.ColumnIndex == button.Index)
                {

                    string? id2 = dataGridView1["Id", e.RowIndex].Value.ToString();
                    string? title = dataGridView1["Title", e.RowIndex].Value.ToString();
                    string? description = dataGridView1["Description", e.RowIndex].Value.ToString();
                    string? date = dataGridView1["Date", e.RowIndex].Value.ToString();
                    string? comp2 = dataGridView1["Completed", e.RowIndex].Value.ToString();
                    idtxt.Text = id2;
                    textBox1.Text = title;
                    textBox3.Text = description;
                    dateTimePicker1.Value = DateTime.Parse(date);
                    comboBox1.SelectedItem = comp2;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnreset_Click(object sender, EventArgs e)
        {


        }
        void ResetOnly()
        {
            textBox3.Text = "";
            idtxt.Text = "";
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void Reset()
        {
            ResetOnly();

            showAll();
        }



        private void resetbtn_Click(object sender, EventArgs e)
        {
            Reset();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            if (checkedIn.Count == 0)
            {
                MessageBox.Show("Select record(s) to delete");
                return;
            }
            foreach (string element in checkedIn)
            {
                string sql = "ToDoListDb '" + element + "','','','','', 'DELETE' ";
                DataAccessDb.fillDataSet(sql);
            }

            Reset();
            MessageBox.Show("Information deleted successfully");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string search = textBox2.Text;
            if (search == "")
            {
                showAll();
            }
            else
            {
                string? search_criteria = comboBox1.SelectedIndex == 0 ? "TITLE" : "DATE";
                string sql = "ToDoListDb '','','','','" + search + "','" + search_criteria + "'";
                DataSet ds = DataAccessDb.fillDataSet(sql);
                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
    public class DataAccessDb
    {
        public static SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        public static DataSet fillDataSet(string sql)
        {
            try
            {
                DataSet dset = new DataSet();                                   //usp_todoapp
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dset);
                return dset;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            return null;

        }
    }

}