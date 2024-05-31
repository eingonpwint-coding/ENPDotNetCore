using ENPDotNetCore.Shared;
using ENPDotNetCore.WinFormsApp;

namespace ENPDotNetCore.WinFormsAppSqlinjection;

public partial class Form1 : Form
{
    private readonly DapperService _dapperService;
    public Form1()
    {
        InitializeComponent();
        _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        //string query = $"select * from tbl_user where email = '{txtEmail.Text.Trim()}' and password = '{txtPassword.Text.Trim()}'";
        //var model = _dapperService.QueryFirstOrDefault<UserModel>(query);
        string query = $"select * from tbl_user where email = @Email and password = @Password";
        var model = _dapperService.QueryFirstOrDefault<UserModel>(query, new
        {
            Email = txtEmail.Text.Trim(),
            Password = txtPassword.Text.Trim(),
        }); // test password = ddd' or 1=1 + '
        if (model is null)
        {
            MessageBox.Show("User does not exit .");
            return;
        }
        MessageBox.Show("Is Admin :" + model.Email);
    }
}

public class UserModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsAdmin { get; set; }   
}
