using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productrepository;

        public ProductService(IProductRepository _repository)
        {
            productrepository = _repository;
        }

		public List<Product> GetAll(string include = null)
		{
			if (include == null)
			{
				return productrepository.GetAll();
			}
			return productrepository.GetAll(include);
		}


		public Product Get(int Id)
		{
			return productrepository.Get(Id);
		}


		public List<Product> Get(Func<Product, bool> where)
		{
			return productrepository.Get(where);
		}

		public void Insert(Product product)
        {
            productrepository.Insert(product);
        }


        public void Update(Product product)
        {
            productrepository.Update(product);
        }


        public void Delete(Product product)
        {
            productrepository.Delete(product);
        }


        public void Save()
        {
            productrepository.Save();
        }


        ////////////////////////////////////////////////
        // Maher : need to check up again after implement comment repo 
        //public ProductWithCommentsViewModel ProductWithComments(Product product)
        //{
        //    ProductWithCommentsViewModel provm = new ProductWithCommentsViewModel();
        //    List<Comment> comments = new List<Comment>();
        //    foreach(Comment c in comments)
        //    {
        //        if(c.ProductId==product.Id)
        //            comments.Add(c);
        //    }
        //    provm.Id =product.Id;
        //    provm.Name=product.Name;
        //    provm.Description=product.Description;
        //    provm.Price = product.Price;
        //    provm.Quantity = product.Quantity;
        //    provm.ImageUrl= product.ImageUrl;
        //    provm.CategoryId= product.CategoryId;
        //    provm.Rating = product.Rating;
        //    provm.Color = product.Color;
        //    provm.Comments = comments;
        //    return provm;

        //}
    }
}
