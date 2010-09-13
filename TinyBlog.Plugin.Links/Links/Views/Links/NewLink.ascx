<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Links.Link>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<% using (Html.BeginForm("Save", "Links", FormMethod.Post)) {%>
    <tr>
        <td>
            <%: Html.TextBoxFor(c => c.Id)%>
        </td>
        <td>
            <%: Html.TextBoxFor(c => c.Name)%>
        </td>
        <td>
            <%: Html.TextBoxFor(c => c.Url)%>
        </td>
        <td><a href="JavaScript:void(0)" onclick="deleteLink(<%=Model.Id %>, this)">Delete</a></td>
        <td></td>
        <td><input type="submit" value="Save"/></td>
    </tr>
<% } %>

