using ENPDotNetCore.Shared;
using ENPDotNetCore.WinFormsApp.Queries;

namespace ENPDotNetCore.WinFormsApp;

public partial class FrmBlog : Form
{
    private readonly DapperService _dapperService;

    public FrmBlog()
    {
        InitializeComponent();
        _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
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
            int result = _dapperService.Execute(BlogQuery.BlogCreate,blog);
            string message = result > 0 ? "Saving successful." : "Saving Failed";
            var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            MessageBox.Show(message,"Blog",MessageBoxButtons.OK, messageBoxIcon);
            if(result > 0)
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
}
