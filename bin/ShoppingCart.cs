using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetShoppingSite;
public class ShoppingCart {
    public List<Item> ItemsInCart = new List<Item>();
    public ShoppingCart() {
    }
    public static ShoppingCart Instance {
        get {
            if (HttpContext.Current.Session["shoppingcart"] == null) {
                ShoppingCart cart = new ShoppingCart();
                HttpContext.Current.Session["shoppingcart"] = cart;
            }
            return (ShoppingCart)HttpContext.Current.Session["shoppingcart"];
        }
    }
}
