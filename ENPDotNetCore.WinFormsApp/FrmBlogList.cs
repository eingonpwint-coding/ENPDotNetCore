using Dapper;
using ENPDotNetCore.Shared;
using ENPDotNetCore.WinFormsApp.Models;
using ENPDotNetCore.WinFormsApp.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Tracing;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ENPDotNetCore.WinFormsApp
{
    public partial class FrmBlogList : Form
    {
        private readonly DapperService _dapperService;
        //private const int _edit = 1;
        //private const int _delete = 2;
        public FrmBlogList()
        {
            InitializeComponent();
            //dgvData.AutoGenerateColumns = false; // select * , 1 result from table => no include 1 result
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }
        
        private void FrmBlogList_Load(object sender, EventArgs e)
        {
            BlogList();
        }

        private void BlogList()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>(BlogQuery.BlogList);
            dgvData.DataSource = lst;//bind data
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int columnIndex = e.ColumnIndex;
           // int rowIndex = e.RowIndex;
            //sometimes when user click block place [] in front of the Id , index will be -1 / but real index start from O ,need to check
            if (e.RowIndex == -1) return;

            #region If Case
            var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);
            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                FrmBlog frm = new FrmBlog(blogId); // call the frm blog (include create form)
                frm.ShowDialog();//need id (add constructor)

                BlogList();

            }
            else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                //write confirm box
                var dialogResult = MessageBox.Show("Are you sure want to delete?","",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialogResult != DialogResult.Yes) return;
                DeleteBlog(blogId);
            }

            #endregion

            #region Switch Case

            int index = e.ColumnIndex;
            EnumFormControlType enumFormControlType = (EnumFormControlType)index;
            switch (enumFormControlType)
            {
          
                case EnumFormControlType.Edit:
                    FrmBlog frm = new FrmBlog(blogId); // call the frm blog (include create form)
                    frm.ShowDialog();//need id (add constructor)
                    BlogList();
                    break;
                case EnumFormControlType.Delete:
                    var dialogResult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult != DialogResult.Yes) return;
                    DeleteBlog(blogId);
                    break;
                case EnumFormControlType.None:
                default:
                MessageBox.Show("Invalid case ");
                    break;
            }

            #endregion
            //if use enum, more confortable with switch
            //firstly declare EnumFormControlType
            /*EnumFormControlType enumFormControlType = EnumFormControlType.None;
            switch (enumFormControlType)
            {
                case EnumFormControlType.None:
                    break;
                case EnumFormControlType.Edit:
                    break;
                case EnumFormControlType.Delete:
                    break;
                default:
                    break;
            }*/
            //string formControlType = "None";
            //switch (formControlType)
            //{
            //    case "asdfdfdfdj":
            //        break; // can write anything in case , it can cause error
            //    default:
            //        break;
            //}
        }

        private void DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                    WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new { BlogId = id });
            string message = result > 0 ? "Deleting Successful" : "Deleting Failed";
            MessageBox.Show(message);
            BlogList();
        }
    }
}
