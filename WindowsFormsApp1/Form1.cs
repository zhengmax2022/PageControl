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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private void LoadDataIntoDataGridView()
        {
            DataTable dataTable = new DataTable();

            // 添加列
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));

            // 填充数据
            for (int i = 1; i <= 100; i++)
            {
                dataTable.Rows.Add(i, "Name " + i, "email" + i + "@example.com");
            }

            // 绑定数据到DataGridView
            dataGridView1.DataSource = dataTable;

            //设置控制分页的DataGridView
            pageControl1.DataGridView = dataGridView1;

            //设置数据源，必须为DataTable
            pageControl1.SetDataSource(dataTable);
        }

        public Form1()
        {
            InitializeComponent();

            LoadDataIntoDataGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {       
           

        }
    }
}
