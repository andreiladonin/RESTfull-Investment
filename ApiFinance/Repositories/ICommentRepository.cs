using ApiFinance.Models;

namespace ApiFinance.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment commentModel);
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> DeleteAsync(int id);
        Task<Comment?> UpdateAsync(int id ,Comment comment);
    }
}
