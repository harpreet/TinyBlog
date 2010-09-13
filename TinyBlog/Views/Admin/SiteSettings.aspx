<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" Inherits="MvcContrib.FluentHtml.ModelViewPage<TinyBlog.Objects.Site>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SiteSettings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <% using (Html.BeginForm("Save", "Admin")) {%>
    <fieldset>
        <legend>Fields</legend>
        <table class="fieldtable">
        <thead>
        </thead>
        <tbody>
            <tr>
                <td><%= Html.Label("Site Title") %></td>
                <td><%= this.TextBox(model => model.Name) %></td>
                <td><%= this.ValidationMessage(model => model.Name) %></td>
            </tr>
            <tr>
                <td><%= Html.Label("Home Page Url") %></td>
                <td><%= this.TextBox(model => model.SiteHomeUrl) %></td>
                <td><%= this.ValidationMessage(model => model.SiteHomeUrl) %></td>
            </tr>
            <tr>
                <td><%= this.Label(model => model.Description) %></td>
                <td><%= this.TextBox(model => model.Description) %></td>
                <td><%= this.ValidationMessage(model => model.Description) %></td>
            </tr>
            <tr>
                <td><%= Html.Label("Time Zone Offset") %></td>
                <td><%= this.TextBox(model => model.TimeZoneOffset) %></td>
                <td><%= this.ValidationMessage(model => model.TimeZoneOffset) %></td>
            </tr>
            <tr>
                <td><%= Html.Label("Disable Comments") %></td>
                <td><%= this.CheckBox(model => model.CommentingEnabled) %></td>
                <td><%= this.ValidationMessage(model => model.CommentingEnabled) %></td>
            </tr>
            <tr>
                <td><%= Html.Label("Alternate Feed Url") %></td>
                <td><%= this.TextBox(model => model.AlternateFeedUrl) %></td>
                <td><%= this.ValidationMessage(model => model.AlternateFeedUrl) %></td>
            </tr>
            <tr>
                <td><%= Html.Label("Script to inject into all pages") %></td>
                <td><%= this.TextArea(model => model.PageFooterScript) %></td>
                <td><%= this.ValidationMessage(model => model.PageFooterScript) %></td>
            </tr>
            <tr>
                <td><%= Html.Label("Script to inject into all posts") %></td>
                <td><%= this.TextArea(model => model.PostFooterScript) %></td>
                <td><%= this.ValidationMessage(model => model.PostFooterScript) %></td>
            </tr>
        </tbody>
        </table>
                <%= this.Hidden(model => model.Id) %>
        <p>
            <input type="submit" value="Save" />
        </p>
    </fieldset>
<% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

