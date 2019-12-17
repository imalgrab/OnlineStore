using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using InternetShoppingSite;
public class MyRoleProvider : RoleProvider {
    public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
        throw new NotImplementedException();
    }

    public override void CreateRole(string roleName) {
        throw new NotImplementedException();
    }

    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
        throw new NotImplementedException();
    }

    public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
        throw new NotImplementedException();
    }

    public override string[] GetAllRoles() {
        throw new NotImplementedException();
    }

    public override string[] GetRolesForUser(string username) {
        var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
        using (var userDataContext = new UserDataContext(cs))
        using (var roleDataContext = new RoleDataContext(cs))
        using (var urDataContext = new UserRoleDataContext(cs)) {
            var users = userDataContext.User.ToList();
            var user = from u in users
                       where u.name == username
                       select u;
            if (!user.Any()) {
                throw new Exception($"User ${username} does not exist!");
            } else {
                var uID = user.First().id;
                var userRoles = urDataContext.UserRole.ToList();
                var userRoleIDs = from ur in userRoles
                                  where ur.uid == uID
                                  select ur.rid;
                var roleNames = new List<string>();
                foreach (var rID in userRoleIDs) {
                    var roles = roleDataContext.Role.ToList();
                    var rName = (from role in roles
                                 where role.id == rID
                                 select role.name).First();
                    roleNames.Add(rName);
                }
                return roleNames.ToArray();
            }
        }
    }

    public override string[] GetUsersInRole(string roleName) {
        throw new NotImplementedException();
    }

    public override bool IsUserInRole(string username, string roleName) {
        var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
        using (var userDataContext = new UserDataContext(cs))
        using (var roleDataContext = new RoleDataContext(cs))
        using (var urDataContext = new UserRoleDataContext(cs)) {
            var uid = from user in userDataContext.User
                      where user.name == username
                      select user.id;
            if (!uid.Any()) {
                return false;
            } else {
                var uID = uid.First();
                var uidRIDs = from ur in urDataContext.UserRole
                              where ur.uid == uID
                              select ur.rid;
                var roleNames = new List<string>();
                var roles = roleDataContext.Role.ToList();
                foreach (var rID in uidRIDs) {
                    var rName = (from role in roles
                                 where role.id == rID
                                 select role.name).First();
                    roleNames.Add(rName);
                }
                foreach (var name in roleNames) {
                    if (name == roleName) {
                        return true;
                    }
                }
                return false;
            }
        }
    }

    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
        throw new NotImplementedException();
    }

    public override bool RoleExists(string roleName) {
        throw new NotImplementedException();
    }
}