using System;
using System.Collections.Generic;
using System.Text;

namespace Mesbla.Sales.Orders.Queries.Results
{
    public class OrderFlat
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        //Pode ser usado tbm o ValueText
        public string State { get; set; }
    }
}
