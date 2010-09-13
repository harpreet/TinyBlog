<%@ Control Language="C#" Inherits="MvcContrib.FluentHtml.ModelViewUserControl<IList<TinyBlog.Objects.Post>>" %>
<%@ Import Namespace="TinyBlog.Objects" %>


<div class="widget">
    <div class="widgetHeader">Pages</div>
    <% if (Model.HasData()){%>
        <ul class="ulNoBullets">
        <% foreach (Post p in Model){ %>
            <li><%= Html.RouteLink(p.Title, "Post", new {postSlug = p.Slug}) %></li>
        <% } %>
        </ul>
    <% } %>
</div>



