using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace OnlineShopping.Models.ViewModel
{
    public class BrandModelViewModel
    {
        public SelectList Brands { get; set; }

    }
}
