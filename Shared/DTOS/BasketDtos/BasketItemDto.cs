﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = default!; 
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }  
        public int Quantity { get; set; }


    }
}
