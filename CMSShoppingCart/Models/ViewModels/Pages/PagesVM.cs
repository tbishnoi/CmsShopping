using CMSShoppingCart.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSShoppingCart.Models.ViewModels
{
    public class PagesVM
    {
        public PagesVM()
        {

        }
        public PagesVM(PagesDTO row)
        {
            this.Id = row.Id;
            this.Title = row.Title;
            this.Slug = row.Slug;
            this.Body = row.Body;
            this.Sorting = row.Sorting;
            this.HasSideBar = row.HasSideBar;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 2)]
        public string Title { get; set; }
        public string Slug { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [AllowHtml]
        public string Body { get; set; }
        public int Sorting { get; set; }
        public bool HasSideBar { get; set; }

    }
}