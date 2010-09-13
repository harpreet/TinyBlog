<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Posts/Page.Master" Inherits="MvcContrib.FluentHtml.ModelViewPage<TinyBlog.Objects.Post>"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Create/Edit post</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript" src="<%=Url.Content("~/ckeditor/ckeditor.js") %>"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var validateUsername = $('#Title');
        $('#Title').keyup(function () {
            var t = this;
            if (this.value != this.lastValue) {
                $.ajax({
                    url: "CheckTitleExists/",
                    data: "{'title': '" + t.value + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    type: "post",
                    success: function (j) { validateUsername.html(j.msg); }
                });
            }
            this.lastValue = this.value;
        });
    });
</script>
    <h2>Edit</h2>

    <% using (Html.BeginForm("Save", "Posts", FormMethod.Post)) {%>

        <fieldset>
            <p>
                <%= Html.LabelFor(model => model.Title) %>
                <%= Html.TextBoxFor(model => model.Title) %>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </p>
            <p>
                <%= Html.LabelFor(model => model.Body) %>
                <%= this.TextArea(model => model.Body).Id("postTextArea")%>
                <script type="text/javascript">CKEDITOR.replace('postTextArea',
                {
                    filebrowserBrowseUrl: '/browser/browse/type/all',
                    filebrowserUploadUrl: '/Upload/up',
                    filebrowserImageBrowseUrl: '/browser/browse/type/image',
                    filebrowserImageUploadUrl: '/Upload/up',
                    filebrowserWindowWidth: 800,
                    filebrowserWindowHeight: 500
                }
                );</script>
           </p>
            <p>
                <%= this.Label(model => model.IsPage) %>
                <%= Html.CheckBoxFor(model => model.IsPage) %>
                <%= Html.LabelFor(model => model.Published) %>
                <%= this.TextBox(model => model.Published).Id("publishTime") %>
                <%= Html.ValidationMessageFor(model => model.Published) %>
            </p>
            <p>
                <%=this.Label(model => model.Tags) %>
                <%=Html.TextBox("Tags", Model.Tags.ToCommaDelimitedString()) %>
            </p>
            <p> 
                <%=Html.HiddenFor(model => model.Id) %>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>
    

</asp:Content>

