<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TinyBlog.Objects.Site>" %>
<%@ Import Namespace="TinyBlog.Objects" %>
<div id="footer">
    <div id="logindisplay"><% Html.RenderPartial("LogOnUserControl"); %></div>
    <% if (HttpContext.Current.User.HasRole(UserRole.Admin)){ %>
    <ul>
        <li><%= Html.RouteLink("Create", RouteNames.Author)%></li>
        <li><%= Html.RouteLink("Admin", RouteNames.Admin)%></li>
    </ul>
    <%} %>
</div>
<%=Model.PageFooterScript %>
