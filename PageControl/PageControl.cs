using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PageControl
{
    public partial class PageControl: UserControl
    {
        public PageControl()
        {
            InitializeComponent();

            // 添加一些页大小选项
            comboBoxPageSize.Items.AddRange(new object[] { 20, 50, 80, 100, 150 });         
            comboBoxPageSize.SelectedIndex = 0;  // 默认选择第二个选项（20）
        }

        private int currentPage = 1;
        private int pageSize = 20;
        private BindingSource bindingSource = new BindingSource();

        public DataGridView DataGridView { get; set; }

        [Description("分页属性：获取或设置每页显示的数据量"), Category("获取或设置每页显示的数据量")]
        public int PageSize
        {
            get => pageSize;
            set
            {
                if (value > 0)
                {
                    pageSize = value;
                    currentPage = 1; // 重置为首页
                    UpdateDataGridView();
                    UpdatePageInfo();
                }
            }
        }

        public int TotalPages
        {
            get
            {
                // 判断是否有正确的数据源
                if (bindingSource.DataSource != null && pageSize > 0)
                {
                    // 计算总页数
                    int totalRecords = ((DataTable)bindingSource.DataSource).Rows.Count;
                    return (totalRecords + pageSize - 1) / pageSize; // 使用整数除法计算页面
                }
                return 0; // 如果未设置数据源，则返回空值
            }
        } 

        private void NavigateTo(int page)
        {
            if (page >= 1 && page <= TotalPages)
            {
                currentPage = page;
                UpdateDataGridView();
                UpdatePageInfo();
            }
        }

        public void SetDataSource(DataTable dataTable)
        {
            bindingSource.DataSource = dataTable;
            NavigateTo(1); // 起始第一页
        }

        private void UpdateDataGridView()
        {
            if (DataGridView != null)
            {
                DataTable dt = (DataTable)bindingSource.DataSource;
                DataTable pageTable = dt.Clone();
                int startRecord = (currentPage - 1) * pageSize;

                for (int i = startRecord; i < startRecord + pageSize && i < dt.Rows.Count; i++)
                {
                    pageTable.ImportRow(dt.Rows[i]);
                }

                DataGridView.DataSource = pageTable;
            }
        }

        private void UpdatePageInfo()
        {
            // 确保DataSource已设置并且lblPageInfo已初始化
            if (bindingSource.DataSource != null && lblPageInfo != null)
            {
                int totalItems = ((DataTable)bindingSource.DataSource).Rows.Count;
                int totalPages = TotalPages;
                lblPageInfo.Text = $"第 {currentPage} 页 / 共 {totalPages} 页（共 {totalItems} 条数据）";
            }
            else
            {
                // 如果未初始化，则在调试时提供反馈
                Debug.WriteLine("DataSource 或 lblPageInfo 未初始化。");
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            NavigateTo(1);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            NavigateTo(currentPage - 1);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            NavigateTo(currentPage + 1);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            NavigateTo(TotalPages);
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtGoPage.Text, out int page))
            {
                NavigateTo(page);
            }
            else
            {
                MessageBox.Show("非法字符！");
            }
        }

        private void txtGoPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            //禁止输入非数字字符
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            //回车键
            if (e.KeyChar == 13)
            {
                BtnGo_Click(sender, e);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPageSize.SelectedItem != null)
            {
                // 更新PageControl的PageSize
                PageSize = Convert.ToInt32(comboBoxPageSize.SelectedItem);
                // 重新加载数据
                UpdateDataGridView();

            }
        }
    }
}
