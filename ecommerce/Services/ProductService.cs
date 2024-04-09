using ecommerce.Models;
using ecommerce.Repository;
using ecommerce.ViewModels;
using ecommerce.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productrepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductService(IProductRepository _repository , ICategoryRepository categoryRepository)
        {
            productrepository = _repository;
            this.categoryRepository = categoryRepository;
        }

		public List<Product> GetAll(string include = null)
		{
			if (include == null)
			{
				return productrepository.GetAll();
			}
			return productrepository.GetAll(include);
		}

		public List<Product> GetPageList(int skipstep, int pageSize)
        {
           return productrepository.GetPageList(skipstep, pageSize);
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


        public async Task<ProductWithCatNameAndComments> WithCatNameAndComments(int id)
        {
            Product p = productrepository.Get(id);
            ProductWithCatNameAndComments product = new() 
            { Id = p.Id, Name = p.Name , ImageUrl = p.ImageUrl , Color = p.Color, 
                Description = p.Description, Price = p.Price , Quantity = p.Quantity , Rating = p.Rating};
            
            Category c = categoryRepository.Get(p.CategoryId);
            product.CateName = c.Name;
            return product;
        }

        public void Insert(ProductWithListOfCatesViewModel p)
        {
            Product product = new()
            { Id = p.Id, Name = p.Name, Color = p.Color , Description = p.Description ,
                ImageUrl = p.ImageUrl , Price = p.Price , Quantity = p.Quantity ,
                Rating = p.Rating , CategoryId = p.CategoryId};
            
            productrepository.Insert(product);
            productrepository.Save();
        }

        public ProductWithListOfCatesViewModel GetViewModel(int id)
        {
            Product p =  productrepository.Get(id);
            ProductWithListOfCatesViewModel prd = new()
            {
                Id = p.Id, Name = p.Name, Color = p.Color , Description = p.Description ,
                ImageUrl = p.ImageUrl , Price = p.Price , Quantity = p.Quantity ,
                Rating = p.Rating , CategoryId = p.CategoryId
            };
            prd.categories = categoryRepository.GetAll();
            return prd;
        }

        public void Update(ProductWithListOfCatesViewModel p)
        {
            Product product = productrepository.Get(p.Id);
            product.Name = p.Name;
            product.Color = p.Color;
            product.Description = p.Description;
            product.ImageUrl = p.ImageUrl;
            product.Price = p.Price;
            product.Quantity = p.Quantity;
            product.Rating = p.Rating;
            product.CategoryId = p.CategoryId;
            productrepository.Update(product);
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
