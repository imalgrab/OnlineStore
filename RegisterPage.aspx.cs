using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

namespace InternetShoppingSite {
    public partial class RegisterPage : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void RegisterBtn_Click(object sender, EventArgs e) {
            var username = NameTextBox.Text;
            var email = EmailTextBox.Text;
            var password = PasswdTextBox.Text;
            CreateAccount(username, email, password);
            Response.Redirect("LoginPage.aspx");
        }

        private void CreateAccount(string username, string email, string password) {
            var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
            using (var userDataContext = new UserDataContext(cs))
            using (var pwdDataContext = new PasswordDataContext(cs))
            using (var roleDataContext = new RoleDataContext(cs))
            using (var urDataContext = new UserRoleDataContext(cs)) {
                var users = userDataContext.User.ToList();
                //check if username or email already exist
                bool userAlreadyExists = users.Any(_ => _.name == username);
                if (userAlreadyExists) {
                    ErrorLbl.Text = "Użytkownik o podanej nazwie już istnieje!";
                    return;
                }
                bool emailAlreadyExists = users.Any(_ => _.email == email);
                if (emailAlreadyExists) {
                    ErrorLbl.Text = "Użytkownik o podanym adresie e-mail już istnieje!";
                    return;
                }
                var user = new User() {
                    name = username,
                    email = email
                };
                userDataContext.User.InsertOnSubmit(user);
                userDataContext.SubmitChanges();
                //hash pwd
                var rng = new RNGCryptoServiceProvider();
                byte[] salt = new byte[15];
                rng.GetBytes(salt);
                var stringSalt = Encoding.ASCII.GetString(salt);
                password += stringSalt;
                byte[] pwd = Encoding.ASCII.GetBytes(password);
                var md5 = MD5.Create();
                var iterations = 15;
                for (int i = 0; i < iterations; i++) {
                    pwd = md5.ComputeHash(pwd);
                }
                //store hashed password
                var passwd = new Password() {
                    hash = Encoding.ASCII.GetString(pwd),
                    salt = stringSalt,
                    iterations = iterations,
                    setDate = DateTime.Now
                };
                pwdDataContext.Password.InsertOnSubmit(passwd);
                pwdDataContext.SubmitChanges();

                var roles = roleDataContext.Role.ToList();
                var rID = (from r in roles
                           where r.name == "user"
                           select r.id).First();
                UserRole ur = new UserRole() {
                    uid = user.id,
                    rid = rID
                };
                urDataContext.UserRole.InsertOnSubmit(ur);
                urDataContext.SubmitChanges();
            }
        }
    }
}