using CMSShoppingCart.Models.Data;
using CMSShoppingCart.Models.ViewModels;
using CMSShoppingCart.Models.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSShoppingCart.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {

            List<PagesVM> pagesList;


            using(Db db = new Db())
            {
                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PagesVM(x)).ToList();


            }
            
            return View(pagesList);
        }
        
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPage(PagesVM model)
        {

            //check model state

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using(Db db = new Db())
            {
                string slug;

                PagesDTO dto = new PagesDTO();


                dto.Title = model.Title;


                if (String.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();

                }
                else
                {

                    slug = model.Slug.Replace(" ", "-").ToLower();
                }

                if(db.Pages.Any(x => x.Title == model.Title)||db.Pages.Any(x => x.Slug == model.Slug))
                {

                    ModelState.AddModelError("", "This title/slug already exists.");
                    return View(model);

                }


                //bind view model to data model

                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSideBar = model.HasSideBar;
                dto.Sorting = 100;


                //Save to db

                db.Pages.Add(dto);
                db.SaveChanges();

            }

            TempData["SM"] = "You have successfully added a new page";

            return RedirectToAction("AddPage");


      
        }


        [HttpGet]
        public ActionResult EditPage(int id)
        {
            PagesVM model;

            using (Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);

                if(dto == null)
                {

                    return Content("The page doesn't exist");
                }

                model = new PagesVM(dto);
            }

            return View(model);

        }

        [HttpPost]
        public ActionResult EditPage(PagesVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);

            }

            using(Db db =new Db())
            {

                int id = model.Id;
                
                string slug = "home";

                PagesDTO dto = db.Pages.Find(id);

                if(model.Slug != "home")
                {


                    if (String.IsNullOrWhiteSpace(model.Slug)){

                        slug = model.Title.Replace(" ", "-").ToLower();

                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }


                    if(db.Pages.Where(x => x.Id != id).Any( x => x.Title == model.Title) ||
                       db.Pages.Where(x => x.Id != id).Any(x => x.Slug == model.Slug))
                    {
                        ModelState.AddModelError("", "The title/Slug already exists");
                        return View(model);
                    }

                }


                dto.Title = model.Title;
                dto.HasSideBar = model.HasSideBar;
                dto.Slug = model.Slug;

             
                db.SaveChanges();


            }

            TempData["SM"] = "You have edited the page";
            return RedirectToAction("EditPage");
        }


        public ActionResult PageDetails(int id)
        {

            PagesVM model;

            using(Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);

                if(dto == null)
                {
                    return Content("The Page doesn't exist");
                }

                model = new PagesVM(dto);
            }

            return View(model);
        }

        
        public ActionResult DeletePage(int id)
        {

            using (Db db = new Db())
            {
                PagesDTO dto = db.Pages.Find(id);

                db.Pages.Remove(dto);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public void ReorderPages(int[] id)
        {
            using(Db db = new Db())
            {
                int count = 1;
                PagesDTO dto;

                foreach(var pageid in id)
                {

                    dto = db.Pages.Find(pageid);

                    dto.Sorting = count;

                    db.SaveChanges();
                    count++;
                    
                }

            }
            
        }

        [HttpGet]
        public ActionResult EditSidebar()
        {
            //declare model

            SidebarVM model;

            using (Db db = new Db())
            {

               SidebarDTO dto = db.Sidebar.Find(1);
               model = new SidebarVM(dto);

            }



            return View(model);
        }


    }
}