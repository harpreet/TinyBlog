<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<List<Links.Link>>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Links" %>

<html>
<head>
    <title>edit</title>
    <script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js'></script>
    <link rel="stylesheet" type="text/css" href="http://yui.yahooapis.com/combo?3.1.1/build/cssreset/reset-min.css&3.1.1/build/cssfonts/fonts-min.css&3.1.1/build/cssgrids/grids-min.css"></link>
    <style type="text/css">
        body { text-align: left; }
        td { vertical-align: bottom; }
    </style>
</head>
<body>
<div>

<% Html.RenderPartial("Links", Model); %>
<%--    <table>
        <thead>
            <tr>
                <th>Id</th>
                <th>LinkText</th>
                <th>Url</th>
            </tr>
        </thead>
        <tbody>
            <% foreach (Link link in Model){ %>
            <% Html.RenderPartial("NewLink", link); %> 
            <%}%>
        </tbody>

    </table>--%>
</div>
</body>
</html>