<%@ Page Title="" Language="C#" MasterPageFile="Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TinyBlog.Objects.Comment>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Comments
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <h2>Comments</h2>

    <table>
        <tr>
            <th></th>
            <th>Body</th>
            <th>Created</th>
            <th>AuthorName</th>
            <th>AuthorEmail</th>
            <th>AuthorUrl</th>
            <th>AuthorIP</th>
        </tr>

    <% foreach (var comment in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Approve", "ApproveComment", new { id = comment.Id }, new { onclick = "$.post(this.href); $($(this).closest('tr')).remove(); return false;" })%> |
                <%: Html.ActionLink("Delete", "DeleteComment", new { id = comment.Id }, new { onclick = "$.post(this.href); $($(this).closest('tr')).remove(); return false; " })%>
            </td>
            <td><%: comment.Body %> </td>
            <td><%: String.Format("{0:g}", comment.Created) %></td>
            <td><%: comment.AuthorName %></td>
            <td><%: comment.AuthorEmail %></td>
            <td><%: comment.AuthorUrl %></td>
            <td><%: comment.AuthorIP %></td>
        </tr>
    
    <% } %>

    </table>

</asp:Content>

