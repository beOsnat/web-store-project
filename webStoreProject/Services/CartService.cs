using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webStoreProject.Models;

namespace webStoreProject.Services
{
    public class CartService : ICartService
    {
        
        public double MemberCartSum(List<double> prices)
        {
            double totalPrice = default(double);
            foreach (var item in prices)
            {
                totalPrice += item;
            }
            return totalPrice * ConstDefinitions.MembersDiscount;
        }

        public double VisitorCartSum(List<double> prices)
        {
            double totalPrice = default(double);
            foreach (var item in prices)
            {
                totalPrice += item;
            }
            return totalPrice ;
        }
    }
}
