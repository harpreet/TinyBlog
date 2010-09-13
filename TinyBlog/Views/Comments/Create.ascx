<%@ Control Language="C#" Inherits="MvcContrib.FluentHtml.ModelViewUserControl<TinyBlog.Controllers.CreateCommentModel>" %>

<div id="newComment"></div>
<div id="answers"></div>
<div id="errors"></div>
<script type="text/javascript" src="<%= Url.Content( "~/Scripts/MicrosoftAjax.js" ) %>"></script>
<script type="text/javascript" src="<%= Url.Content( "~/Scripts/MicrosoftMvcAjax.js" ) %>"></script>
<script type="text/javascript" src="<%= Url.Content( "~/Scripts/jquery.form.js" ) %>"></script>
<h4>Leave a comment</h4>
    <% using (Ajax.BeginForm("Create", "Comments", new AjaxOptions { HttpMethod = "Post", InsertionMode = InsertionMode.InsertBefore, UpdateTargetId = "newComment", OnSuccess = "function(){$('form').clearForm();}", OnFailure = "function(){alert('Captcha error or error saving comment. Try again');}"} )) 
    {%>
        <div class="span-12 last">
            <div class="span-2"><span>Name</span></div>
            <div class="span-4 append-1"><%= this.TextBox(model => model.Comment.AuthorName) %></div>
            <div class="span-4 last"><%= this.ValidationMessage(model => model.Comment.AuthorName)%></div>
        </div>
        <div class="span-12 last">
            <div class="span-2"><span>Email</span></div>
            <div class="span-5 append-1"><%= this.TextBox(model => model.Comment.AuthorEmail)%></div>
            <div class="span-4 last"><%= this.ValidationMessage(model => model.Comment.AuthorEmail)%></div>
        </div>

        <div class="span-12 last">
            <div class="span-2">Website http://</div>
            <div class="span-3 append-1"><%= this.TextBox(model => model.Comment.AuthorUrl)%></div>
            <div class="span-3 last"><%= this.ValidationMessage(model => model.Comment.AuthorUrl)%></div>
        </div>
   
        <div class="span-12 last">
            <div class="span-10 append-2 last"><textarea id="Body" name="Comment"><%= Model.Comment.Body%></textarea></div>
            <%= this.ValidationMessage(model => model.Comment.Body)%>
        </div>
        <div class="span-12 last">
            <% if (!ViewData.ModelState.IsValid){%>
                <span>Validation error</span>
            <%} %>
            <%= Html.GenerateCaptcha() %>
        </div>
        <%= this.Hidden(model => model.ParentPostId) %>
        <div class="span-12 last"><input type="submit" value="Submit" /></div>

    <% } %>