using ecommerce.Models;
using Microsoft.AspNetCore.SignalR;

namespace ecommerce.Hubs
{
    public class CommentHub : Hub
    {
        protected readonly Context context;
        public CommentHub(Context context)
        {
            this.context = context;
        }
        public void SendComment(ApplicationUser User, string Text, int ProductId)
        {

            Comment comment = new Comment() { User = User, text = Text, ProductId = ProductId };
            context.Comments.Add(comment);
            context.SaveChanges();

            Clients.All.SendAsync("ReciveComment", User, Text, ProductId);
        }



    }
}
