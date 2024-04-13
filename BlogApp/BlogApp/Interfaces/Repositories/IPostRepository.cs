using BlogApp.Entities;

namespace BlogApp.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Post Create(Post post);
        Post Update(Post post);
        void Delete(Post post);
        Post Get(Guid id);
        Post Get(string tittle);
        ICollection<Post> GetAllBlogPosts(Guid blogId);
    }
}
