using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Linq;
using System.Web.UI.WebControls;

namespace InternetShoppingSite {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            var addBtn = ListView1.FindControl("InsertItemBtn");
            addBtn.Visible = User.IsInRole("admin");
        }
        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e) {
            switch (e.CommandName) {
                case "ShowInsertTemplate":
                    ListView1.InsertItemPosition = InsertItemPosition.LastItem;
                    ListView1.DataBind();
                    ListView1.InsertItem.DataBind();
                    break;

                case "HideInsertTemplate":
                    ListView1.InsertItemPosition = InsertItemPosition.None;
                    break;
                case "AddToCart":
                    ShoppingCart sc = ShoppingCart.Instance;
                    var id = int.Parse(e.CommandArgument.ToString());
                    var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
                    using (var itemDataContext = new ItemDataContext(cs)) {
                        var item = (from i in itemDataContext.Item
                                    where i.id == id
                                    select i).First();
                        sc.ItemsInCart.Add(item);
                    }
                    break;
            }
        }

        protected void ObjectDataSource1_Inserting(object sender, ObjectDataSourceMethodEventArgs e) {
            Item item = new Item() {
                name = ((TextBox)ListView1.InsertItem.FindControl("NameTextBox")).Text,
                description = ((TextBox)ListView1.InsertItem.FindControl("DescriptionTextBox")).Text,
                price = float.Parse(((TextBox)ListView1.InsertItem.FindControl("PriceTextBox")).Text),
                image = ((TextBox)ListView1.InsertItem.FindControl("ImgTextBox")).Text
            };
            e.InputParameters.Clear();
            e.InputParameters.Add("item", item);
        }

        protected void ObjectDataSource1_Inserted(object sender, ObjectDataSourceStatusEventArgs e) {
            ListView1.InsertItemPosition = InsertItemPosition.None;
            ListView1.DataBind();
        }

        protected void ObjectDataSource1_Updating(object sender, ObjectDataSourceMethodEventArgs e) {
            int id = (int)e.InputParameters["id"];
            Item item = new Item() {
                id = id,
                name = ((TextBox)ListView1.EditItem.FindControl("NameEditBox")).Text,
                description = ((TextBox)ListView1.EditItem.FindControl("DescriptionEditBox")).Text,
                price = float.Parse(((TextBox)ListView1.EditItem.FindControl("PriceEditBox")).Text),
                image = ((TextBox)ListView1.EditItem.FindControl("ImgEditBox")).Text
            };
            e.InputParameters.Clear();
            e.InputParameters.Add("item", item);
        }

        protected void ObjectDataSource1_Updated(object sender, ObjectDataSourceStatusEventArgs e) {
            ListView1.DataBind();
        }

        protected void ObjectDataSource1_Deleting(object sender, ObjectDataSourceMethodEventArgs e) {
            var ip = e.InputParameters;
            int id = (int)ip["id"];
            Item item = new Item() {
                id = id
            };
            e.InputParameters.Clear();
            e.InputParameters.Add("item", item);
        }

        protected void ObjectDataSource1_Deleted(object sender, ObjectDataSourceStatusEventArgs e) {
            ListView1.DataBind();
        }

        protected void LogoutBtn_Click(object sender, EventArgs e) {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("Default.aspx");
        }

        protected void SortBtn_Click(object sender, EventArgs e) {
            DropDownList lv = (DropDownList)ListView1.FindControl("SortDownList");
            LinkButton sortBtn = (LinkButton)ListView1.FindControl("SortBtn");
            var whichMethod = lv.SelectedItem.Text;
            sortBtn.CommandArgument = whichMethod;
        }
    }
}