<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div>
    <%= Html.RouteLink("Site settings", RouteNames.Settings) %>
    <%= Html.RouteLink("Moderate comments", RouteNames.ModerateComments) %>

</div>