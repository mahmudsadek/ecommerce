﻿using ecommerce.Models;

namespace ecommerce.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(Context _context) : base(_context)
        {
        }

        public List<Comment> Take(int num)
        {
            return Context.Comments.Take(num).ToList();
        }
    }
}