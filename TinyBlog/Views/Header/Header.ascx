<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TinyBlog.Objects.Site>" %>
<div class="column span-24 last" id="header">
    <a href="<%= Model.SiteHomeUrl %>"><%=Model.Name %></a>
</div>
<hr />
