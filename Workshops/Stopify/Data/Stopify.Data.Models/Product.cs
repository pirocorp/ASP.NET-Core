﻿namespace Stopify.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime ManufacturedOn { get; set; }

        public int TypeId { get; set; }

        public virtual ProductType Type { get; set; }
    }
}