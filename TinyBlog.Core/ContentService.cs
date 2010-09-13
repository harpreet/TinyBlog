using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TinyBlog.Objects;
using TinyBlog.Interface;
using System.ServiceModel.Syndication;

namespace TinyBlog.Core
{

    public class ContentService : IContentService 
    {
        private readonly ISiteSettingsService _siteSettingsService;
        public IPostRepository PostRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public ITagRepository TagRepository { get; set; }
        public IMembershipService MembershipService { get; set; }
        public HttpContextBase Context { get; set; }
        private const int PAGE_SIZE = 10;

        public ContentService(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository, ISiteSettingsService siteSettingsService, IMembershipService membershipService, HttpContextBase context)
        {
            _siteSettingsService = siteSettingsService;
            PostRepository = postRepository;
            CommentRepository = commentRepository;
            TagRepository = tagRepository;
            MembershipService = membershipService;
            Context = context;
        }

        public int GetPageSize()
        {
            return PAGE_SIZE; 
        }
        private const string filePathExtension = @"\content\media\";

        //TODO: move details to lower layer ?
        public string SaveFileReturnUrl(HttpContextBase context, byte[] file, string fileName)
        {
            fileName = fileName.Substring(fileName.LastIndexOf(@"\") + 1);

            string directory = Path.GetDirectoryName(context.Server.MapPath(filePathExtension));

            DirectoryInfo rootDirectory = new DirectoryInfo(directory);

            if (!rootDirectory.Exists) rootDirectory.Create();

            string fullFileName = Path.Combine(rootDirectory.FullName, fileName);

            using (var fs = new FileStream(fullFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                int bufferSize = 102400;
                var buffer = new byte[bufferSize];
                int bytes;

                var content = new MemoryStream(file);

                while ((bytes = content.Read(buffer, 0, bufferSize)) > 0)
                {
                    fs.Write(buffer, 0, bytes);
                }
            }

            return VirtualPathUtility.ToAbsolute(filePathExtension + fileName);
        }

        public SyndicationFeed GetFeed()
        {
            return new FeedGenerator(Context).Generate(GetPostsPage(1));
        }

        public IList<Comment> GetUnapprovedComments()
        {
            return CommentRepository.GetUnapprovedComments();
        }

        public OperationStatus ApproveComment(int id)
        {
            try
            {
                CommentRepository.ApproveComment(id);
            }
            catch (Exception ex)
            {
                return new Failure(ex.Message);
            }

            return new Success();
        }

        public OperationStatus DeleteComment(int id)
        {
            try
            {
                CommentRepository.DeleteComment(id);
            }
            catch (Exception ex)
            {
                return new Failure(ex.Message);
            }

            return new Success();
        }



        public IList<Post> GetPages()
        {
            return PostRepository.GetPages();
        }

        public bool PostTitleExists(string title)
        {
            return PostRepository.PostTitleExists(title);
        }

        public void SavePost(Post post)
        {
            if (post == null) return;

            if (post.Tags.HasData())
            {
                IEnumerable<Tag> tagsThatAlreadyExist = TagRepository.SearchForTags(post.Tags);

                var tagsToReplace = (from newTag in post.Tags
                                    join t in tagsThatAlreadyExist on newTag.Name equals t.Name
                                    select newTag).ToList();

                foreach (Tag t in tagsToReplace)
                {
                    post.Tags.Remove(t);
                }

                tagsThatAlreadyExist.ToList().ForEach(tagThatExists => post.Tags.Add(tagThatExists));
            }


            //When testing the context doesn't have all this user info.  Could be mocked but isn't yet);
            if (post.Author == null && Context != null && Context.User != null && Context.User.Identity.Name != null)
            {
                post.Author = MembershipService.GetUser(Context.User.Identity.Name);
            }

            PostRepository.SavePost(post);
        }

        public void UpdatePost(int postid, Post post)
        {
            var existingPost = GetPost(postid);
            existingPost.Author = post.Author;
            existingPost.Body = post.Body;
            existingPost.Published = post.Published;
            existingPost.Slug = post.Slug;
            existingPost.Tags = post.Tags ?? existingPost.Tags;
            existingPost.Title = post.Title;
            
            PostRepository.SavePost(existingPost);
        }

        public IList<Comment> GetApprovedCommentsForPost(Post post)
        {
            return CommentRepository.GetApprovedCommentsForPost(post);
        }

        public IList<Comment> GetApprovedCommentsForPost(int parentPostId)
        {
            return CommentRepository.GetApprovedCommentsForPost(parentPostId);
        }

        public OperationStatus SaveComment(Comment comment)
        {
            try
            {
                comment.IsApproved = !_siteSettingsService.GetSiteSettings().ModerateComments;
                comment.Created = DateTime.Now;
                CommentRepository.SaveComment(comment);
            }
            catch(Exception ex)
            {
                return new Failure(ex.Message);
            }

            return new Success();
        }

        public Post GetPost(int id)
        {
            return PostRepository.GetPost(id);
        }

        public Post GetPostWithSlug(string slug)
        {
            return PostRepository.GetPostWithSlug(slug);
        }

        public int GetPostCount()
        {
            return PostRepository.GetPostCount();
        }

        public int GetPostCount(string tag)
        {
            if (string.IsNullOrEmpty(tag)) return GetPostCount();

            return PostRepository.GetPostCount(tag);
        }

        public IList<Post> GetRecentPublishedPosts(int numberOfRecentPosts)
        {
            return PostRepository.GetRecentPosts(numberOfRecentPosts, DateTime.Now);
        }

        public IList<Post> GetPostsPage(int pageNumber)
        {
            var posts = new List<Post>(PostRepository.GetPostsPage(pageNumber, PAGE_SIZE, DateTime.Now));

            posts.Sort((p1, p2) => (-1) * p1.Published.CompareTo(p2.Published));

            return posts;
        }

        public IList<Post> GetPostsWithCategory(string category, int pageNumber)
        {
            return PostRepository.GetPostsWithCategory(category.ToLower(), pageNumber, PAGE_SIZE, DateTime.Now);
        }

        public Post CreateNewPost()
        {
            return new Post {Published = DateTime.Now, CommentsOpen = _siteSettingsService.CommentingEnabled()};
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return TagRepository.GetAllTags();
        }

        public bool DeletePost(int postid)
        {
            return PostRepository.DeletePost(postid);
        }
    }
}
