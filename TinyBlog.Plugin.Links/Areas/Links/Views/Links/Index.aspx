<%@ Page Title= "" Language="C#" Inherits="System.Web.Mvc.ViewPage<List<Links.Link>>" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>
<%@ Import Namespace="TinyBlog.Core" %>
<%@ Import Namespace="TinyBlog.Objects" %>  

<div class="widget">

<div class="widgetHeader">Links</div>
<% if (Model != null)
   {%>
    <ul class="ulNoBullets">
    <%for (int i = 0; i < Model.Count; i++){%>
            <li><a href="<%=Model[i].Url%>"><%=Model[i].Name%></a></li>
        <%}%>
    </ul>
<% } %>
    <%if (HttpContext.Current.User.HasRole(UserRole.Admin)){%>
        <%=Html.ActionLink("Edit", "Edit", "Links")%>
       <%}%>
</div>