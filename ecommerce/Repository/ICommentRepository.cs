﻿using ecommerce.Models;

namespace ecommerce.Repository
{
    public interface ICommentRepository: IRepository<Comment>
    {
        public List<Comment> Take(int num);
    }
}
