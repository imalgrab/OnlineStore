using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetShoppingSite;

public class ShoppingCart_DataProvider {
    public ShoppingCart_DataProvider() {
    }
    public List<Item> Retrieve(string OrderBy, int StartRow, int RowCount) {
        return ShoppingCart.Instance.ItemsInCart.GetRange(StartRow, SelectItemsCount() - StartRow);
    }
    public int SelectItemsCount() {
        return ShoppingCart.Instance.ItemsInCart.Count;
    }
}