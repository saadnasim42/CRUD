using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;

namespace CRUD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-LJMJC40\SQLEXPRESS;Initial Catalog=CRUD;Integrated Security=True");
        public int studentid;

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getstudentsrecord();
        }

        private void getstudentsrecord()
        {

            SqlCommand cmd = new SqlCommand("Select * from StudentTb", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            studentrecordgridview.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sname.Text == string.Empty || Mnumber.Text == string.Empty)
            {
                MessageBox.Show("all fields are required to enter data");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentTb values(@nm,@qy,@pe)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nm", sname.Text);
                cmd.Parameters.AddWithValue("@qy", Mnumber.Text);
                cmd.Parameters.AddWithValue("@pe", pb.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("new student is successfully saved in the database", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getstudentsrecord();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            sname.Text = string.Empty;
            Mnumber.Text = string.Empty;
            pb.Text = string.Empty;
            sname.Focus();
        }

        
        public void studentrecordgridview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)

            {

                DataGridViewRow row = this.studentrecordgridview.Rows[e.RowIndex];



               int studentid = Convert.ToInt32(row.Cells["STUDENTID"].Value);
                sname.Text = row.Cells["NAME"].Value.ToString();

                Mnumber.Text = row.Cells["QUANTITY"].Value.ToString();

                pb.Text = row.Cells["PRICE"].Value.ToString();

                sidhelper.Text = row.Cells["STUDENTID"].Value.ToString();


            }


        }

        private void Update_Click(object sender, EventArgs e) 
        {
            
            


                SqlCommand cmd = new SqlCommand("UPDATE StudentTb SET NAME = @name, QUANTITY = @qty,PRICE = @price WHERE STUDENTID = @id", con);
             
                cmd.Parameters.AddWithValue("@name", sname.Text);
                cmd.Parameters.AddWithValue("@qty", Mnumber.Text);
                cmd.Parameters.AddWithValue("@price", pb.Text);
                cmd.Parameters.AddWithValue("@id", sidhelper.Text);

            
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
               
                MessageBox.Show("information is updated", "saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            reset();
            getstudentsrecord();
             
               
            

        }

        void reset()
        {
            sname.Text = string.Empty;
            Mnumber.Text = string.Empty;
            pb.Text = string.Empty;
            sname.Focus();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM StudentTb WHERE STUDENTID = @id", con);

         
            cmd.Parameters.AddWithValue("@id", sidhelper.Text);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("it is deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
            reset();
            getstudentsrecord();
        }
    }
}
