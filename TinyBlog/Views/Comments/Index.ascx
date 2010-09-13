<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<TinyBlog.Objects.Comment>>" %>

<%if (Model.HasData()) {%> 
    <h4>Comments</h4>


    <% foreach (var comment in Model) { %>
                <% Html.RenderPartial("Comment", comment);%>
    <% } %>
<% }%>



