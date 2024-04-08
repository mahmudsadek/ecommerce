using ecommerce.ViewModels.Comments;

namespace ecommerce.ViewModels.Product
{
    public class ProductWithCatNameAndComments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string Color { get; set; }
        public decimal? Rating { get; set; }

        public List<CommentWithUserNameViewModel> Comments { get; set; }

        public string CateName { get; set; }

    }
}
