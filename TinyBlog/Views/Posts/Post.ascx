<%@ Control Language="C#" Inherits="MvcContrib.FluentHtml.ModelViewUserControl<Post>" %>
<%@ Import Namespace="TinyBlog.Core" %>
<%@ Import Namespace="TinyBlog.Objects"%>


<% if (Model != null) { %>
        <h1 class="postTitle"><%= Html.RouteLink(Model.Title, RouteNames.Post, new {postSlug = Model.Slug} )%></h1>
        <div class="postDate"><%= Model.Published.ToString("dddd, MMMM d, yyyy") %></div>
        <div class="span-19 last append-bottom postBody">
            <%= Model.Body %>
        </div>
        <%if (HttpContext.Current.User.HasRole(UserRole.Admin)){%>
           <div class="span-19 last"><%=Html.RouteLink("Edit", RouteNames.EditPost, new { postSlug = Model.Slug}) %></div>
        <%} %>
<%}%>

