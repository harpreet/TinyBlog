<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Links.Link>>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="System.Web.Routing" %>


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

<% using (Html.BeginForm("Save", "Links", FormMethod.Post)) {%>
    <div>
        <table>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>LinkText</th>
                    <th>Url</th>
                </tr>
            </thead>
            <%for (int i = 0; i < Model.Count; i++) { %>
                <tr>
                    <td>    
                        <%:Html.TextBoxFor(links => links[i].Id)%>
                    </td>
                    <td>
                        <%:Html.TextBoxFor(links => links[i].Name)%>
                    </td>
                    <td>
                        <%:Html.TextBoxFor(links => links[i].Url)%>
                    </td>
                    <td><a href="JavaScript:void(0)" onclick="deleteLink(<%=Model[i].Id %>, this)">Delete</a></td>
                    <td></td>
                </tr>
             <%}%>
        </table>
        <input type="submit" value="Save"></input>
    </div>
<%} %>