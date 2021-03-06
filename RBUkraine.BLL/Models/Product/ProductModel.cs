namespace RBUkraine.BLL.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public Image Image { get; set; }
    }
}
