using ENPDotNetCore.Shared;
using ENPDotNetCore.WinFormsApp.Models;
using ENPDotNetCore.WinFormsApp.Queries;
using System.Data.SqlClient;
using System.Data;

namespace ENPDotNetCore.WinFormsApp;

public partial class FrmBlog : Form
{
    private readonly DapperService _dapperService;
    private readonly int _blogId;
    public FrmBlog()
    {
        InitializeComponent();
        _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }

    public FrmBlog(int blogId)
    {
        InitializeComponent();
        _blogId = blogId;
        _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        //check for update or create
        var model = _dapperService.QueryFirstOrDefault<BlogModel>("select * from Tbl_Blog where BlogId=@BlogId", new { BlogId = _blogId });
        txtTitle.Text = model.BlogTitle;
        txtAuthor.Text = model.BlogAuthor;
        txtContent.Text = model.BlogContent;

        btnSave.Visible = false;
        btnUpdate.Visible = true;
    }
    private void btnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //trim delete space 
            BlogModel blog = new BlogModel();
            blog.BlogTitle = txtTitle.Text.Trim();
            blog.BlogAuthor = txtAuthor.Text.Trim();
            blog.BlogContent = txtContent.Text.Trim();
            int result = _dapperService.Execute(BlogQuery.BlogCreate, blog);
            string message = result > 0 ? "Saving successful." : "Saving Failed";
            var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            MessageBox.Show(message, "Blog", MessageBoxButtons.OK, messageBoxIcon);
            if (result > 0)
            {
                ClearControl();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    private void ClearControl()
    {
        txtTitle.Clear();
        txtAuthor.Clear();
        txtContent.Clear();

        txtTitle.Focus();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            var item = new BlogModel
            {
                BlogId = _blogId,
                BlogTitle = txtTitle.Text.Trim(),
                BlogAuthor = txtAuthor.Text.Trim(),
                BlogContent = txtAuthor.Text.Trim()
            };
            string query = @"UPDATE [dbo].[Tbl_Blog]
                SET [BlogTitle] = @BlogTitle
                ,[BlogAuthor] = @BlogAuthor
                ,[BlogContent] = @BlogContent
                 WHERE BLogId =@BlogId";
            int result = _dapperService.Execute(query, item);
            string message = result > 0 ? "Updating Successful" : "Updating Failed";
            MessageBox.Show(message);

            this.Close();
        }
        catch (Exception ex)
        {

            MessageBox.Show(ex.ToString());
        }
    }
}
