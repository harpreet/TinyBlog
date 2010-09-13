<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<List<Links.Link>>" %>
<%@ Import Namespace="System.Web.Mvc.Html" %>


<div>
hello
<% if (Model != null)
   {%>
    <ul>
    <%
       for (int i = 0; i < Model.Count; i++)
       {%>
        <li><a href="<%=Model[i].Url%>"><%=Model[i].Name%></a></li>
    <%
       }%>
    </ul>
    
    role: <%=User.IsInRole("Admin")%>

    <%
       if (User.IsInRole("Admin"))
       {%>
        <%=Html.ActionLink("Edit", "Edit", "Links")%>
    <%
       }%>
<% } %>
</div>