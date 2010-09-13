<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TinyBlog.Objects.Comment>" %>
<div class="comment" id="<%= Model.Id %>">
    <div class="commentDate">
        <%= Model.Created.ToString("dddd, MMMM d, yyyy") %>
    </div>
    <div class="commentBody">
        <%= Model.Body %>
    </div>
    <div class="commentAuthorInfo">
        <a href="<%=Model.AuthorUrl %>" rel="nofollow"> <%= Model.AuthorName %></a>
    </div>
</div>



