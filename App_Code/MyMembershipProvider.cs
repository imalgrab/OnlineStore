using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using InternetShoppingSite;
public class MyMembershipProvider : MembershipProvider {
    public override bool EnablePasswordRetrieval => throw new NotImplementedException();

    public override bool EnablePasswordReset => throw new NotImplementedException();

    public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

    public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

    public override int PasswordAttemptWindow => throw new NotImplementedException();

    public override bool RequiresUniqueEmail => throw new NotImplementedException();

    public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

    public override int MinRequiredPasswordLength => throw new NotImplementedException();

    public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

    public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

    public override bool ChangePassword(string username, string oldPassword, string newPassword) {
        throw new NotImplementedException();
    }

    public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer) {
        throw new NotImplementedException();
    }

    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status) {
        throw new NotImplementedException();
    }

    public override bool DeleteUser(string username, bool deleteAllRelatedData) {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords) {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords) {
        throw new NotImplementedException();
    }

    public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords) {
        throw new NotImplementedException();
    }

    public override int GetNumberOfUsersOnline() {
        throw new NotImplementedException();
    }

    public override string GetPassword(string username, string answer) {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline) {
        throw new NotImplementedException();
    }

    public override MembershipUser GetUser(string username, bool userIsOnline) {
        throw new NotImplementedException();
    }

    public override string GetUserNameByEmail(string email) {
        throw new NotImplementedException();
    }

    public override string ResetPassword(string username, string answer) {
        throw new NotImplementedException();
    }

    public override bool UnlockUser(string userName) {
        throw new NotImplementedException();
    }

    public override void UpdateUser(MembershipUser user) {
        throw new NotImplementedException();
    }

    public override bool ValidateUser(string username, string password) {
        var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
        using (var userDataContext = new UserDataContext(cs))
        using (var pwdDataContext = new PasswordDataContext(cs)) {
            //check if user exists
            var users = userDataContext.User.ToList();
            var userExists = from u in users
                             where u.name == username
                             select u;
            if (userExists.Any()) {
                User user = userExists.First();
                var passwords = pwdDataContext.Password.ToList();
                Password passwd = (from p in passwords
                                   where p.id == user.id
                                   select p).First();
                var passwdSalt = passwd.salt;
                var passwdIterations = passwd.iterations;
                var passwdHashedPassword = passwd.hash;
                //get salt bytes
                byte[] salt = new byte[15];
                salt = Encoding.ASCII.GetBytes(passwdSalt);
                password += passwdSalt;
                byte[] pwd = Encoding.ASCII.GetBytes(password);
                var md5 = MD5.Create();
                var iterations = 15;
                for (int i = 0; i < iterations; i++) {
                    pwd = md5.ComputeHash(pwd);
                }
                var pwdComputedHash = Encoding.ASCII.GetString(pwd);
                return pwdComputedHash == passwdHashedPassword;
            }
            return false;
        }
    }
}