<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShoppingCartPage.aspx.cs" Inherits="InternetShoppingSite.ShoppingCartPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="Style.css" />
    <title>Koszyk</title>
    <style>
        #active {
            background-color: #ff6a00;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar">
            <nav>
                <ul>
                    <li class="solid">ISS</li>
                    <li>
                        <asp:LinkButton ID="HomeBtn" runat="server" PostBackUrl="~/Default.aspx" CssClass="linkbtn">Strona główna</asp:LinkButton>
                    </li>
                    <%if (User.Identity.IsAuthenticated) {%>
                    <li class="solid">Witaj <%=User.Identity.Name%></li>
                    <%}%>
                    <li id="active">
                        <asp:LinkButton ID="CartBtn" runat="server" PostBackUrl="~/ShoppingCartPage.aspx" CssClass="linkbtn">Koszyk</asp:LinkButton>
                    </li>
                </ul>
            </nav>
        </div>
        <asp:ListView ID="ListView2" runat="server" DataSourceID="ObjectDataSource2" ItemPlaceholderID="PlaceHolder2" DataKeyNames="id">
            <LayoutTemplate>
                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListView2" PageSize="10">
                    <Fields>
                        <asp:NumericPagerField ButtonCount="5" ButtonType="Link" />
                    </Fields>
                </asp:DataPager>
            </LayoutTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td>
                            <img src="<%# ((InternetShoppingSite.Item)Container.DataItem).image %>" style="width: 150px; height: 150px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# ((InternetShoppingSite.Item)Container.DataItem).name %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# ((InternetShoppingSite.Item)Container.DataItem).description %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%# ((InternetShoppingSite.Item)Container.DataItem).price %> PLN
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:ListView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" MaximumRowsParameterName="RowCount"
            SelectCountMethod="SelectItemsCount" SelectMethod="Retrieve" SortParameterName="OrderBy" EnablePaging="True"
            StartRowIndexParameterName="StartRow" TypeName="ShoppingCart_DataProvider"></asp:ObjectDataSource>
    </form>
</body>
</html>
