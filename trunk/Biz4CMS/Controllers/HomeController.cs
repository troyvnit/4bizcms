using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Biz4CMS.ViewModels;

namespace Biz4CMS.Controllers
{
    public class HomeController : Controller
    {
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SinglePageIndex()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }


		public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Message model)
        {
            if (ModelState.IsValid)
            {
                db.Messages.Add(model);
                model.CreatedDate = DateTime.Now; 
                db.SaveChanges();
                //return RedirectToAction("index");
            }

            return Json("true", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Banner()
        {
            var banners = db.Banners.Where(p => p.Tag == "Top").OrderBy(p => p.BannerId).ToList();
            return PartialView("_Banner", banners);
        }
        public ActionResult MiddleBanner()
        {
            var banners = db.Banners.Where(p => p.Tag == "Middle").OrderBy(p => p.BannerId).ToList();
            return PartialView("MiddleBanner", banners);
        }
        public ActionResult RightBanner()
        {
            var banners = db.Banners.Where(p => p.Tag == "Right").OrderBy(p => p.BannerId).ToList();
            return PartialView("RightBanner", banners);
        }
        //public ActionResult RightMenu()
        //{
        //    var rightMenu = new List<BoxLinkDto>();
        //    var galerry = new BoxLinkDto();
        //    galerry.BoxName = "Gallery";
        //    galerry.BoxLink = "/Gallery";
        //    galerry.BoxType = "Gallery";
        //    galerry.Links = db.Categorys.Where(p => p.Tag == "Product").AsEnumerable().Select(p => new LinkDto() { Title = p.Name, Link = "/gallery/?categoryid=" + p.CategoryId   }).ToList(); ;
        //    rightMenu.Add(galerry);  
        //    var categorys = db.Categorys.Where(p => p.Tag == "Article");
        //    foreach (var item in categorys)
        //    {
        //        var links = db.Articles.Where(p => p.CategoryId == item.CategoryId && p.Active ).OrderByDescending(p => p.ArticleId).Take(10).AsEnumerable().Select(p => new LinkDto() { Title = p.Title, Link = string.Format("/article/details/{0}", p.ArticleId) }).ToList();
        //        var boxLink = new BoxLinkDto();
        //        boxLink.BoxName = item.Name;
        //        boxLink.BoxLink = Url.Action("Index", "Article", new { categoryId = item.CategoryId });
        //        boxLink.Links = links;
        //        rightMenu.Add(boxLink);
 
        //    }
        //    return PartialView("RightMenu",rightMenu);
        //}
        public ActionResult TopMenu()
        {
            var menus = db.Menus.Where(p => p.ParentId == 0 && p.Tag == "Top").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link }).ToList();
            return PartialView("TopMenu", menus);
        }
        public ActionResult BoxMenu(int id)
        {
            var menu = db.Menus.Where(p => p.MenuId == id && p.Tag == "Right").FirstOrDefault();
            
            ViewBag.MenuTitle = menu.Text;
            ViewBag.MenuLink =  string.IsNullOrEmpty(menu.Link)?"":menu.Link;
            var menus = db.Menus.Where(p => p.ParentId == id && p.Tag == "Right").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link }).ToList();
            return PartialView("BoxMenu", menus);
        }
        public ActionResult RightMenu()
        {
            
            var menus = db.Menus.Where(p => p.ParentId == 0 && p.Tag == "Right").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => p.MenuId).ToList();
            return PartialView("RightMenu", menus);
        }
        public ActionResult BottomMenu(int id)
        {
            ViewBag.MenuTitle = db.Menus.Where(p => p.MenuId == id && p.Tag == "Bottom").FirstOrDefault().Text;
            var menus = db.Menus.Where(p => p.ParentId == id && p.Tag == "Bottom").OrderByDescending(p => p.Order).ThenBy(p => p.MenuId).Select(p => new LinkDto() { Title = p.Text, Link = p.Link }).ToList();
            return PartialView("BottomMenu", menus);
        }

        public ActionResult UserComment()
        {
            var comments = db.Comments.OrderByDescending(p => p.Order).ThenByDescending(p => p.CommentId).Take(3).ToList();
            return PartialView("UserComment", comments);
        }
        public ActionResult TopQuickLink()
        {
            var toplinks = db.QuickLinks.Where(p => p.Tag == "Top").OrderByDescending(p => p.Order).ThenByDescending(p => p.QuickLinkId).Take(3).ToList();
            return PartialView("TopQuickLink", toplinks);
        }
        public ActionResult MiddleQuickLink()
        {
            var quicklinks = db.QuickLinks.Where(p => p.Tag == "Middle").OrderByDescending(p => p.Order).ThenByDescending(p => p.QuickLinkId).ToList();
            return PartialView("MiddleQuickLink", quicklinks);
        }
        public ActionResult RightQuickLink()
        {
            var quicklinks = db.QuickLinks.Where(p => p.Tag == "Right").OrderByDescending(p => p.Order).ThenByDescending(p => p.QuickLinkId).ToList();
            return PartialView("RightQuickLink", quicklinks);
        }
        public ActionResult Video()
        {
            var videos = db.Videos.OrderByDescending(p => p.VideoId).Take(5).ToList();
            return PartialView("Video", videos);
        }
        public ActionResult Feedback()
        {
            var comments = db.Comments.OrderByDescending(p => p.Order).ThenByDescending(p => p.CommentId).ToList();
            return PartialView("Feedback", comments);
        }
        public ActionResult Videos()
        {
            var videos = db.Videos.OrderByDescending(p => p.VideoId).ToList();
            return View(videos);
        }
        
    }
}
