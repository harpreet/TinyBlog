<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        <b><%= Html.Encode(Page.User.Identity.Name) %></b>
        <%= Html.RouteLink("Log off", RouteNames.LogOff) %>
<%
    }
    else {
%> 
         <%= Html.RouteLink("Log on", RouteNames.LogOn) %> 
<%
    }
%>
