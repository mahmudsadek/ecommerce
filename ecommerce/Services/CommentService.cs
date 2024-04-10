using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.ViewModels.Comments;
using Microsoft.AspNetCore.Identity;

namespace ecommerce.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentService(ICommentRepository commentRepository, UserManager<ApplicationUser> userManager)
        {
            this.commentRepository = commentRepository;
            this.userManager = userManager;
        }

        public Comment GetComment(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetComments(Func<Comment, bool> where)
        {
            return commentRepository.Get(where);
        }

        public async Task<List<CommentWithUserNameViewModel>> GetCommentWithUserName(int pid)
        {
            List<CommentWithUserNameViewModel> withUserNameVM = new();
            List<Comment> comments = commentRepository.Get(c => c.ProductId == pid);
            foreach (Comment comment in comments)
            {
                ApplicationUser user = await userManager.FindByIdAsync(comment.UserId);
                CommentWithUserNameViewModel c = new() { Id = comment.Id, text = comment.text, userName = user.UserName };
                withUserNameVM.Add(c);
            }
            return withUserNameVM;
        }

        public async Task<List<CommentWithUserNameViewModel>> GetCommentWithUserNameTake(int num)
        {
            List<CommentWithUserNameViewModel> withUserNameVM = new();
            List<Comment> comments = commentRepository.Take(num);
            foreach (Comment comment in comments)
            {
                ApplicationUser user = await userManager.FindByIdAsync(comment.UserId);
                CommentWithUserNameViewModel c = new() { Id = comment.Id, text = comment.text, userName = user.UserName };
                withUserNameVM.Add(c);
            }
            return withUserNameVM;
        }
    }
}
