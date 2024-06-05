using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
   public interface IRecommendationService
    {
        public Task<BookRecommendation[]> GetRecommendations(string bookName, int numRecommendations = 5);
    }
}
