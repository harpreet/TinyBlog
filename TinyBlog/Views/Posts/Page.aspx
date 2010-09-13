<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Posts/Page.Master" Inherits="System.Web.Mvc.ViewPage<TinyBlog.Objects.Post>" %>
<%@ Import Namespace="TinyBlog.Objects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<% if (Model != null){%>
        <%=Model.Title %>
    <%}%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% if (Model != null) { %>
        <h1 class="postTitle"><%= Html.RouteLink(Model.Title, RouteNames.Post, new {postSlug = Model.Slug} )%></h1>
        <div class="postDate"><%= Model.Published.ToString("dddd, MMMM d, yyyy") %></div>
        <div class="span-24 last append-bottom postBody">
            <%= Model.Body %>
        </div>
        <%if (HttpContext.Current.User.HasRole(UserRole.Admin)){%>
           <div class="span-19 last"><%=Html.RouteLink("Edit", RouteNames.EditPost, new { postSlug = Model.Slug}) %></div>
        <%} %>
<%}%>
</asp:Content>
