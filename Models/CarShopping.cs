using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models
{
    public class CarShopping
    {
        public class Customer
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int RoleId { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Telephone { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
            public string Address { get; set; }


            public int OrderId { get; set; }

            public List<Order> orders { get; set; }
        }

      
        public class Category
        {
            public int ID { get; set; }
            public string CategoryName { get; set; }
            public string ImageUrl { get; set; }
            public List<Car> Cars { get; set; }
        }

        public class Brand
        {
            public int ID { get; set; }
            public string BrandName { get; set; }
            public List<Car> Cars { get; set; }
            public string ImageUrl { get; set; }
        }

        //public class Model
        //{

        //    public int ID { get; set; }
        //    public string ModelName { get; set; }
        //    public int BrandID { get; set; }
        //    public Brand Brand { get; set; }
        //    public List<Car> Cars { get; set; }

           
        //}

        public class Color
        {
            public int ID { get; set; }
            public string ColorName { get; set; }
            public string ColorNumber { get; set; }

            public List<Car> Cars { get; set; }
        }

        public class Car
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public DateTime CreateDate { get; set; }
            public string Price { get; set; }
            public string Description { get; set; }

            //public int ColorID { get; set; }
            //public Color Color { get; set; }

            public int BrandID { get; set; }
            public Brand Brand { get; set; }
            public int CategoryID { get; set; }
            public Category Category { get; set; }

            public string ImageUrl { get; set; }

            public LinkedList<OrderDetails> orderDetails { get; set; }
        }

        public class Order
        {
            public int ID { get; set; }
            public DateTime OrderDate { get; set; }

            public int CustomerID { get; set; }
            public Customer Customer { get; set; }

            public List<OrderDetails> OrderDetails { get; set; }

            
        }

        public class OrderDetails
        {
            public int ID { get; set; }
            public int Price { get; set; }
            public int Discount { get; set; }
            public int Quantity { get; set; }


            public int CarID { get; set; }
            public Car Car { get; set; }
            public int OrderID { get; set; }
            public Order Order { get; set; }
        }


    }
}
