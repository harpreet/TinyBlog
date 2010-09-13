<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<TinyBlog.Models.PostListViewModel>" %>
<%@ Import Namespace="TinyBlog.Objects"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
      <% if (Model != null && Model.IsForSinglePost){%>
            <%=Model.FirstPost.Title %>
        <%}%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="span-19 last append-bottom"></div>
    <%if (Model != null) {
        foreach (var post in Model.Posts)
        {%>
        <div class="span-19 last append-bottom">
            <% if (post != null) Html.RenderPartial("Post", post);%>
            <hr />
        </div>
        <%}%>
          

        <div class="span-2">
        <% if (Model.HasPreviousPage) {%>
            <a href="<%=Model.PreviousPageLink %>">Previous</a>
        <%} %>
        </div>
        <div class="span-2 prepend-17 last">
        <% if (Model.HasNextPage) {%>
            <a href="<%=Model.NextPageLink %>">Next</a>
        <%} %>
        </div>

    <% }%>



      <% if (Model != null && Model.IsForSinglePost) {%>
        <div class="span-19 last">
            <% Html.RenderAction("Index", "Comments", new { postId = Model.FirstPost.Id }); %>
        </div>
        <div class="span-19 last">
            <% Html.RenderAction("Create", "Comments", new { postId = Model.FirstPost.Id }); %>
        </div>
    <% } %>
</asp:Content>
