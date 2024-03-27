using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string text { get; set; }

        //------------------------------

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        //---------------------
        public ApplicationUser User { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
