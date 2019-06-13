using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webStoreProject.Services
{
    public interface ICartService
    {
        double VisitorCartSum(List<double> prices);
        double MemberCartSum(List<double> prices);
    }
}
