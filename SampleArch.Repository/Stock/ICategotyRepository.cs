using SampleArch.Model;
using SampleArch.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleArch.Repository.Stock
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
         IEnumerable<Category> ListForSearch();
    }
}
