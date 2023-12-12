﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="BravoHub.AdminModule.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Page</title>
    <link type="text/css" rel="stylesheet" href="AdminPageStyles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="RootContainer">
            <header id="HeaderPage">
                <h1 id="UserGreeting" runat="server"></h1>
            </header>
            <main id="Pannel">
                <h2 id="TabTitle" runat="server"></h2>
                <p id="TabDescription1" runat="server"></p>
                <asp:TextBox ID="SectionInputElement" runat="server"/>
                <asp:Button ID="SectionSearchBtn" runat="server" Text="Search" OnClick="btnSearchClicked"/>
                <p id="TabDescription2" runat="server"></p>
                <asp:TextBox ID="DeleteUserInputElement" runat="server"/>
                <asp:Button ID="DeleteUserBtn" runat="server" Text="Delete" OnClick="btnDeleteClicked"/> <br/>
                <asp:Label ID="PromotMsg" runat="server" ForeColor="Yellow"/>
            </main>
            <side id="Tabs">
                <section id="Features">
                    <asp:Button ID="UsersBtn" runat="server" Text="Users" CssClass="tab tab-selected" OnClick="UsersBtn_Click"/>
                    <asp:Button ID="MediasBtn" runat="server" Text="Medias" CssClass="tab" OnClick="MediasBtn_Click"/>
                </section>
                <asp:Button ID="LogOutBtn" runat="server" Text="log out" CssClass="btn" OnClick="LogOutBtn_Click"/>
            </side>
        </div>
    </form>
</body>
</html>
