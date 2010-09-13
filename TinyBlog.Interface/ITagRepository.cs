using System.Collections.Generic;
using TinyBlog.Objects;

namespace TinyBlog.Interface
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAllTags();
        IEnumerable<Tag> SearchForTags(IEnumerable<Tag> tagsToFind);
    }
}
