using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using System.Threading;

namespace IT18259476
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(CultureInfo cl)
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = cl;
            Thread.CurrentThread.CurrentUICulture = cl;

            ResourceManager rmg = new ResourceManager("IT18259476.Form1", System.Reflection.Assembly.GetExecutingAssembly());

            this.Text = this.Text + "-" + DateTime.Now.ToLongDateString();
            button1.Text = rmg.GetString("button1.Text");
            button2.Text = rmg.GetString("button2.Text");
            button3.Text = rmg.GetString("button3.Text");
            button4.Text = rmg.GetString("button4.Text");
            button5.Text = rmg.GetString("button5.Text");
            button6.Text = rmg.GetString("button6.Text");
            button7.Text = rmg.GetString("button7.Text");
            button8.Text = rmg.GetString("button8.Text");

            label1.Text = rmg.GetString("label1.Text");
            label2.Text = rmg.GetString("label2.Text");
            label3.Text = rmg.GetString("label3.Text");
            label4.Text = rmg.GetString("label4.Text");
            label5.Text = rmg.GetString("label5.Text");
            label6.Text = rmg.GetString("label6.Text");
            label7.Text = rmg.GetString("label7.Text");

        }

        DataSet order_ds;

        private void button9_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'collegeDataSet.Courses' table. You can move, or remove it, as needed.
            this.coursesTableAdapter.Fill(this.collegeDataSet.Courses);
            pictureBox1.DataBindings.Add("ImageLocation", coursesBindingSource, "Image", false, DataSourceUpdateMode.Never);
            order_ds = new DataSet();
            DataTable ordertb = new DataTable("Order");
            DataColumn c1 = new DataColumn("CourseID", typeof(int));
            DataColumn c2 = new DataColumn("Duration", typeof(double));
            DataColumn c3 = new DataColumn("CourseFee", typeof(double));
            DataColumn c4 = new DataColumn("StudentName", typeof(string));
            DataColumn c5 = new DataColumn("StudentID", typeof(int));
            DataColumn c6 = new DataColumn("Total", typeof(double));

            
            c6.Expression = "Duration*CourseFee";

            ordertb.Columns.Add(c1);
            ordertb.Columns.Add(c2);
            ordertb.Columns.Add(c3);
            ordertb.Columns.Add(c4);
            ordertb.Columns.Add(c5);
            ordertb.Columns.Add(c6);

            ordertb.PrimaryKey = new DataColumn[] { c1 };
            order_ds.Tables.Add(ordertb);
            dataGridView2.DataSource = order_ds.Tables[0];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int id = (int)coursesTableAdapter.getMaxID();
            textBox1.Text = "" + (id + 1);

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            coursesTableAdapter.Insert(
                int.Parse(textBox1.Text),
                textBox2.Text,
                double.Parse(textBox3.Text),
                double.Parse(textBox4.Text),
                pictureBox1.ImageLocation);

            MessageBox.Show("New Course Added Sucessfully");
            this.coursesTableAdapter.Fill(this.collegeDataSet.Courses);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            coursesTableAdapter.Delete(int.Parse(textBox1.Text));
            MessageBox.Show("Course Removed Sucessfully");
            this.coursesTableAdapter.Fill(this.collegeDataSet.Courses);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            coursesTableAdapter.Update(
               textBox2.Text,
               decimal.Parse(textBox3.Text),
               decimal.Parse(textBox4.Text),
               pictureBox1.ImageLocation,
               int.Parse(textBox1.Text));

            MessageBox.Show("Course Updated Sucessfully");
            this.coursesTableAdapter.Fill(this.collegeDataSet.Courses);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataRow r = order_ds.Tables[0].NewRow();
           

            if(textBox1.Text==""||textBox3.Text==""|| textBox4.Text=="")
            {
                MessageBox.Show("You Must Select Course Row");
            }
            else if(textBox5.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("You Must Enter Student ID and Student Name");
            }
            else
            {
                r[0] = textBox1.Text;
                r[1] = double.Parse(textBox3.Text);
                r[2] = double.Parse(textBox4.Text);
                r[3] = textBox5.Text;
                r[4] = textBox6.Text;
                order_ds.Tables[0].Rows.Add(r);
                MessageBox.Show("Entrolled Sucessfully");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataRow r = order_ds.Tables[0].Rows.Find(
                dataGridView2.SelectedRows[0].Cells[0].Value
                );
            order_ds.Tables[0].Rows.Remove(r);
            MessageBox.Show("UnEntrolled Sucessfully");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double total = 0;
            String stuDes = "";

            foreach(DataRow r in order_ds.Tables[0].Rows)
            {
                stuDes = stuDes + "Student Name:" + r[3] + "," + "Studen Id:" + r[4] + "," + "Course ID:" + r[0];
                total = total + (double)r[5];
            }
            label8.Text = "" + total;
            orderTableAdapter1.Insert(
                textBox5.Text,
                int.Parse(textBox6.Text),
                stuDes,
                DateTime.Now,
                total
                );
            MessageBox.Show("Confirmed Sucessfully");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            order_ds.Tables[0].Rows.Clear();
            label8.Text = "";

        }
    }
}
