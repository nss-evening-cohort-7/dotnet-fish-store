namespace FishStore.Models
{
    public class FishOrder
    {
        public int FishId { get; set; }
        public int Quantity { get; set; }
        public string FishName { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int OrderId { get; set; }
    }
}