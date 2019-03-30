using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CMSShoppingCart.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<PagesDTO> Pages { get; set; }

        public DbSet<SidebarDTO> Sidebar { get; set; }

        public System.Data.Entity.DbSet<CMSShoppingCart.Models.ViewModels.PagesVM> PagesVMs { get; set; }

        public System.Data.Entity.DbSet<CMSShoppingCart.Models.ViewModels.Pages.SidebarVM> SidebarVMs { get; set; }
    }
}