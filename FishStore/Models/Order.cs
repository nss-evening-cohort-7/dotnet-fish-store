using System;
using System.Collections.Generic;

namespace FishStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public IEnumerable<FishOrder> Fishes { get; set; }
    }
}