<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InternetShoppingSite.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="Style.css?_=ixdrt8789r7t89dr7tgtubgt" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="navbar">
                <nav>
                    <ul>
                        <li class="solid">ISS</li>
                        <li id="active">
                            <asp:LinkButton ID="HomeBtn" runat="server" PostBackUrl="~/Default.aspx" CssClass="linkbtn">Strona główna</asp:LinkButton>
                        </li>
                        <li>
                            <asp:LinkButton ID="CartBtn" runat="server" PostBackUrl="~/ShoppingCartPage.aspx" CssClass="linkbtn">Koszyk</asp:LinkButton>
                        </li>
                        <%if (User.Identity.IsAuthenticated) {%>
                        <li class="solid">Witaj <%=User.Identity.Name%></li>
                        <li>
                            <asp:LinkButton ID="LogoutBtn" runat="server" OnClick="LogoutBtn_Click" CssClass="linkbtn">Wyloguj</asp:LinkButton>

                        </li>
                        <%} else {%>
                        <li>
                            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/LoginPage.aspx" CssClass="linkbtn">Zaloguj się</asp:LinkButton>
                        </li>
                        <%}%>
                    </ul>
                </nav>
            </div>
            <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="id"
                ItemPlaceholderID="PlaceHolder1" OnItemCommand="ListView1_ItemCommand">
                <LayoutTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Sortuj według:" CssClass="sort"></asp:Label>
                    <asp:DropDownList ID="SortDownList" runat="server" CssClass="sort">
                        <asp:ListItem>Cena (malejąco)</asp:ListItem>
                        <asp:ListItem>Cena (rosnąco)</asp:ListItem>
                        <asp:ListItem>Nazwa (malejąco)</asp:ListItem>
                        <asp:ListItem>Nazwa (rosnąco)</asp:ListItem>
                    </asp:DropDownList>
                    <asp:LinkButton ID="SortBtn" runat="server" OnClick="SortBtn_Click" CssClass="linkbtn" CommandName="Sort" CommandArgument="">Sortuj</asp:LinkButton>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="6">
                        <Fields>
                            <asp:NumericPagerField ButtonCount="5" ButtonType="Link" />
                        </Fields>
                    </asp:DataPager>
                    <asp:LinkButton ID="InsertItemBtn" runat="server" CssClass="linkbtn" CommandName="ShowInsertTemplate">Dodaj</asp:LinkButton>
                </LayoutTemplate>
                <ItemTemplate>
                    <table class="itemtmplt">
                        <tr>
                            <td>
                                <img src="<%# ((InternetShoppingSite.Item)Container.DataItem).image %>" style="width: 150px; height: 150px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b><%#((InternetShoppingSite.Item)Container.DataItem).name %></b>
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
                        <%if (User.Identity.IsAuthenticated) {%>
                        <tr>
                            <td>
                                <asp:LinkButton ID="AddToCartBtn" CommandName="AddToCart" CssClass="linkbtn" CommandArgument="<%# ((InternetShoppingSite.Item)Container.DataItem).id %>" runat="server">Dodaj do koszyka</asp:LinkButton>
                            </td>
                        </tr>
                        <%}%>
                        <%if (Roles.IsUserInRole(User.Identity.Name, "admin")) {%>
                        <tr>
                            <td>
                                <asp:LinkButton ID="EditItemBtn" CommandName="Edit" CssClass="linkbtn" CommandArgument="<%# ((InternetShoppingSite.Item)Container.DataItem).id %>"
                                    Text="Edytuj" runat="server" />
                                <asp:LinkButton ID="DeleteItemBtn" CommandName="Delete" CssClass="linkbtn" CommandArgument="<%# ((InternetShoppingSite.Item)Container.DataItem).id %>"
                                    Text="Usuń" runat="server" />
                            </td>
                        </tr>
                        <%}%>
                    </table>
                </ItemTemplate>
                <InsertItemTemplate>
                    <asp:Table ID="Table1" runat="server">
                        <asp:TableRow>
                            <asp:TableCell>Nazwa przedmiotu:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="NameTextBox" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Opis:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="DescriptionTextBox" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Cena:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="PriceTextBox" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Adres obrazka:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="ImgTextBox" TextMode="Url" runat="server"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:LinkButton ID="Akceptuj" runat="server" Text="Akceptuj" CssClass="linkbtn" CommandName="Insert" />
                                <asp:LinkButton ID="Anuluj" runat="server" Text="Anuluj" CssClass="linkbtn" CommandName="HideInsertTemplate" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </InsertItemTemplate>
                <EditItemTemplate>
                    <asp:Table ID="Table2" runat="server" CssClass="itemtmplt">
                        <asp:TableRow>
                            <asp:TableCell>Nazwa przedmiotu:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="NameEditBox" runat="server" Text="<%# ((InternetShoppingSite.Item)Container.DataItem).name %>"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Opis:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="DescriptionEditBox" runat="server" Text="<%# ((InternetShoppingSite.Item)Container.DataItem).description %>"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Cena:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="PriceEditBox" runat="server" Text="<%# ((InternetShoppingSite.Item)Container.DataItem).price %>"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Adres obrazka:</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="ImgEditBox" TextMode="Url" runat="server" Text="<%# ((InternetShoppingSite.Item)Container.DataItem).image %>"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <asp:LinkButton ID="Akceptuj" runat="server" CssClass="linkbtn" Text="Akceptuj" CommandName="Update" />
                                <asp:LinkButton ID="Anuluj" runat="server" CssClass="linkbtn" Text="Anuluj" CommandName="Cancel" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </EditItemTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" InsertMethod="Insert"
                MaximumRowsParameterName="RowCount" SelectCountMethod="SelectItemsCount" SelectMethod="Retrieve"
                SortParameterName="OrderBy" StartRowIndexParameterName="StartRow" TypeName="ItemDataProvider"
                UpdateMethod="Update" DeleteMethod="Delete" OnDeleting="ObjectDataSource1_Deleting" OnDeleted="ObjectDataSource1_Deleted" OnUpdating="ObjectDataSource1_Updating"
                OnUpdated="ObjectDataSource1_Updated" OnInserted="ObjectDataSource1_Inserted"
                OnInserting="ObjectDataSource1_Inserting"></asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
