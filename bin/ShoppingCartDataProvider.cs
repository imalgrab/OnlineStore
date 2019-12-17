using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetShoppingSite;

public class ShoppingCartDataProvider {
    public ShoppingCartDataProvider() {

    }
    public List<Item> Retrieve(string OrderBy, int StartRow, int RowCount) {
        if (RowCount > 0) {
            return ShoppingCart.Instance.ItemsInCart.GetRange(StartRow, SelectItemsCount() - StartRow);
        } else {
            return new List<Item>(new Item[] { ShoppingCart.Instance.ItemsInCart[StartRow] });
        }
    }
    public int SelectItemsCount() {
        return ShoppingCart.Instance.ItemsInCart.Count;
    }
}
