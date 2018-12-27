using SampleArch.Model;
using System;
using System.Data.Entity;
using System.Linq;


namespace SampleArch.Repository.Admin
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DbContext context)
            : base(context)
        {
        }


    }
}
