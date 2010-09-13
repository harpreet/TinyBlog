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
<script type="text/javascript">
    function deleteLink(id, rowElement) {
        $.ajax({
            type: "POST",
            url: "Delete/" + id,
            data: "{'args': '" + id + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () { $($(rowElement).closest('tr')).remove(); },
            error: function () { alert("error"); }
        });
    }
</script>

<%if (Model != null){%>
    <div>
        <table>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>LinkText</th>
                    <th>Url</th>
                </tr>
            </thead>

                <%for (int i = 0; i < Model.Count; i++){%>
                    <tr>
                        <td>    
                            <%=Model[i].Id%>
                        </td>
                        <td>
                            <%=Model[i].Name%>
                        </td>
                        <td>
                            <%=Model[i].Url%>
                        </td>
                        <td><a href="JavaScript:void(0)" onclick="deleteLink(<%=Model[i].Id%>, this)">Delete</a></td>
                        <td></td>
                    </tr>
                 <%}%>

        </table>

    </div>
<%}%>

<% Html.RenderPartial("NewLink", new Link()); %> 
</body>
</html>