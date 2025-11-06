<%@ Page Title="Home Page"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="PersonRegistration.aspx.cs"
    Inherits="VCadApp.PersonRegistration"
    ResponseEncoding="utf-8"
    Culture="pt-BR"
    UICulture="pt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <div class="centered-container">
        <div class="card">
            <h2>Cadastro de Usuário</h2>

            <asp:ValidationSummary runat="server" ID="vsSummary" CssClass="validation-summary" />

            <div class="form-row">
                <label for="txtName">Nome</label>
                <asp:TextBox ID="txtName" runat="server" CssClass="input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtName"
                    ErrorMessage="Nome é obrigatório." CssClass="validator" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label for="txtBirthDate">Data de Nascimento</label>
                <asp:TextBox ID="txtBirthDate" runat="server" TextMode="Date" CssClass="input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqBirth" runat="server" ControlToValidate="txtBirthDate"
                    ErrorMessage="Data de nascimento é obrigatória." CssClass="validator" Display="Dynamic" />
                <asp:CustomValidator ID="CvBirthDate" runat="server" ControlToValidate="txtBirthDate"
                    OnServerValidate="CvBirthDate_ServerValidate" ErrorMessage="Data inválida ou futura."
                    CssClass="validator" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label for="txtEmail">E-mail</label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="input"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="E-mail é obrigatório." CssClass="validator" Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="E-mail inválido."
                    ValidationExpression="^[^\s@]+@[^\s@]+\.[^\s@]+$" CssClass="validator" Display="Dynamic" />
            </div>

            <div class="form-row">
                <label for="ddlMarital">Estado Civil</label>
                <asp:DropDownList ID="ddlMarital" runat="server" CssClass="input">
                    <asp:ListItem Text="-- Selecione --" Value="" />
                    <asp:ListItem Text="Solteiro(a)" Value="Solteiro(a)" />
                    <asp:ListItem Text="Casado(a)" Value="Casado(a)" />
                    <asp:ListItem Text="Divorciado(a)" Value="Divorciado(a)" />
                    <asp:ListItem Text="Viúvo(a)" Value="Viúvo(a)" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqMarital" runat="server" ControlToValidate="ddlMarital"
                    InitialValue="" ErrorMessage="Selecione o estado civil." CssClass="validator" Display="Dynamic" />
            </div>

            <div class="form-actions">
                <asp:Button ID="btnSubmit" runat="server" Text="Salvar" CssClass="btn-primary" OnClick="BtnSubmit_Click" />
                <asp:Label ID="lblMessage" runat="server" CssClass="success-message" />
            </div>

            <hr />

            <h3>Cadastros Recentes</h3>
            <asp:GridView ID="gvPersons" runat="server" AutoGenerateColumns="false" CssClass="grid">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="#" />
                    <asp:BoundField DataField="Name" HeaderText="Nome" />
                    <asp:BoundField DataField="BirthDate" HeaderText="Nascimento" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Email" HeaderText="E-mail" />
                    <asp:BoundField DataField="MaritalStatus" HeaderText="Estado Civil" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
