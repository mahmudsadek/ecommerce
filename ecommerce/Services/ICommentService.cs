using ecommerce.Models;
using ecommerce.ViewModels.Comments;

namespace ecommerce.Services
{
    public interface ICommentService
    {
        Comment GetComment(int id);
        List<Comment> GetComments(Func<Comment, bool> where);
        public Task<List<CommentWithUserNameViewModel>> GetCommentWithUserName(int pid);
        public Task<List<CommentWithUserNameViewModel>> GetCommentWithUserNameTake(int num);
    }
}
