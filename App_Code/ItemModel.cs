using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using InternetShoppingSite;

public class ItemModel {
    public List<Item> Items = new List<Item>();

    public ItemModel() {
        var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
        using (var itemDataContext = new ItemDataContext(cs)) {
            foreach (var item in itemDataContext.Item) {
                Items.Add(item);
            }
        }
    }
    public static ItemModel Instance {
        get {
            if (HttpContext.Current.Items["itemmodel"] == null) {
                ItemModel model = new ItemModel();
                HttpContext.Current.Items["itemmodel"] = model;
            }
            return (ItemModel)HttpContext.Current.Items["itemmodel"];
        }
    }
}
