using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.API.ViewModel.Responses
{
    public class ProductResponse
    {
        public ProductResponse(DateTime startRequest, List<Domain.Entities.Product> products)
        {
            ResponseTime = (DateTime.Now - startRequest).ToString();
            Items = products.Count();
            Products = products;
        }

        public string ResponseTime { get; private set; }
        public int Items { get; private set; }
        public List<Domain.Entities.Product> Products { get; private set; } 
    }
}