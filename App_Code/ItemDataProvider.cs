using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Web;
using InternetShoppingSite;

public class ItemDataProvider {
    public List<Item> Retrieve(string OrderBy, int StartRow, int RowCount) {
        List<Item> items = ItemModel.Instance.Items;
        if (OrderBy.Contains("Nazwa (malejąco)")) {
            items.Sort((x, y) => string.Compare(y.name, x.name));
        } else if (OrderBy.Contains("Nazwa (rosnąco)")) {
            items.Sort((x, y) => string.Compare(x.name, y.name));
        } else if (OrderBy.Contains("Cena (malejąco)")) {
            items.Sort((x, y) => ((double)y.price).CompareTo((double)x.price));
        } else if (OrderBy.Contains("Cena (rosnąco)")) {
            items.Sort((x, y) => ((double)x.price).CompareTo((double)y.price));
        }
        return items.GetRange(StartRow, SelectItemsCount() - StartRow);

    }
    public Item Insert(Item item) {
        var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
        using (var itemDataContext = new ItemDataContext(cs)) {
            itemDataContext.Item.InsertOnSubmit(item);
            itemDataContext.SubmitChanges();
        }
        return item;
    }
    public Item Delete(Item item) {
        var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
        using (var itemDataContext = new ItemDataContext(cs)) {
            var entity = (from i in itemDataContext.Item
                          where i.id == item.id
                          select i).First();
            itemDataContext.Item.DeleteOnSubmit(entity);
            itemDataContext.SubmitChanges();
            return entity;
        }
    }

    public Item Update(Item item) {
        var cs = ConfigurationManager.AppSettings["ShopCatalogDB"];
        using (var itemDataContext = new ItemDataContext(cs)) {
            var id = item.id;
            var actualItem = (from i in itemDataContext.Item
                              where i.id == id
                              select i).First();
            actualItem.name = item.name;
            actualItem.description = item.description;
            actualItem.price = item.price;
            actualItem.image = item.image;
            itemDataContext.SubmitChanges();
            return actualItem;
        }
    }
    public int SelectItemsCount() {
        return ItemModel.Instance.Items.Count;
    }
}
