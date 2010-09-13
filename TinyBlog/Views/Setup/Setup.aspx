<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<TinyBlog.Models.SetupViewModel>" %>

<h2>Setup</h2>

<% using (Html.BeginForm("Save","Setup")) {%>
    <%: Html.ValidationSummary(true) %>
        <table>
            <tr>
                <td>Username</td>
                <td><%: Html.TextBoxFor(model => model.Username) %></td>
                <td><%: Html.ValidationMessageFor(model => model.Username) %></td>
            </tr>
            <tr>
                <td>Password</td>
                <td><%: Html.PasswordFor(model => model.Password) %></td>
                <td><%: Html.ValidationMessageFor(model => model.Password) %></td>
            </tr>
            <tr>
                <td>Confirm password</td>
                <td><%: Html.PasswordFor(model => model.ConfirmPassword) %></td>
                <td><%: Html.ValidationMessageFor(model => model.ConfirmPassword) %></td>
            </tr>
            <tr>
                <td>Email Address</td>
                <td><%: Html.TextBoxFor(model => model.EmailAddress) %></td>
                <td><%: Html.ValidationMessageFor(model => model.EmailAddress) %></td>
            </tr>
            <tr>
                <td>Full Name</td>
                <td><%: Html.TextBoxFor(model => model.FullName) %></td>
                <td><%: Html.ValidationMessageFor(model => model.FullName) %></td>
            </tr>
            <tr>
                <td>Site Title</td>
                <td><%: Html.TextBoxFor(model => model.SiteTitle) %></td>
                <td><%: Html.ValidationMessageFor(model => model.SiteTitle) %></td>
            </tr>
            <tr>
                <td>Alternate Feed Url</td>
                <td><%: Html.TextBoxFor(model => model.AlternateFeedUrl) %></td>
                <td><%: Html.ValidationMessageFor(model => model.AlternateFeedUrl) %></td>
            </tr>
            <tr>
                <td>Page footer script</td>
                <td><%: Html.TextBoxFor(model => model.PageFooterScript) %></td>
                <td><%: Html.ValidationMessageFor(model => model.PageFooterScript) %></td>
            </tr>
        </table>
        <p><input type="submit" value="Save" /></p>

<% } %>


