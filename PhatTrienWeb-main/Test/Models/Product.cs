﻿using PhatTrienWeb.Data;

namespace PhatTrienWeb.Models
{
    public class Product
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
        public double? Price { get; set; }
        public string? Image { get; set; }
        public ICollection<OrderProduct>? OrderProducts { get; set; }
    }
}
