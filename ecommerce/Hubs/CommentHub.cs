﻿using ecommerce.Models;
using ecommerce.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ecommerce.Hubs
{
    public class CommentHub : Hub
    {
        protected readonly ICommentRepository commentRepository;
        protected readonly UserManager<ApplicationUser> userManager;

        public CommentHub(ICommentRepository commentRepository,UserManager<ApplicationUser> userManager)
        {
            this.commentRepository= commentRepository;
            this.userManager= userManager;
        }
        public async void SendComment(string userId, string Text, int ProductId)
        {

            ApplicationUser applicationUser = await userManager.FindByIdAsync(userId);
            Comment comment = new Comment() { User = applicationUser, text = Text, ProductId = ProductId };
            commentRepository.Insert(comment);
            commentRepository.Save();
            Clients.All.SendAsync("ReciveComment", applicationUser.UserName , Text, ProductId);
        }



    }
}
