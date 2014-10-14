using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.ViewModels;
using Biz4CMS.Models;

namespace Biz4CMS.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        Biz4Db db = new Biz4Db();
        public ActionResult Details(int id)
        {
            var pictures = db.Pictures.Where(p => p.ProductId == id).OrderByDescending(p => p.PictureId).Select(p => new PictureDto()
            {
                Name = p.Name,
                PictureId = p.PictureId,
                Tags = p.Tags,
                Src = p.Src, 
                ProductFolder = p.Product.Folder
            }).ToList();
            var Product = db.Products.FirstOrDefault(p => p.ProductId ==id);
            if (Product != null) { ViewBag.ProductName = Product.Name; ViewBag.ProductDetails = Product.Content; }
            return View(pictures);
        }
        public ActionResult Index(int? categoryid)
        {

            var Products = db.Products.Where(p=> !categoryid.HasValue || p.CategoryId == categoryid).Select(a => new BriefProductDto
                         {
                             Name = a.Name, // or pc.ProdId
                             ProductId = a.ProductId,
                             Avatar = a.Avatar == null ? "" : a.Avatar,
                             Description = a.Description == null ? "" : a.Description 
                             // other assignments
                         });
            if (categoryid.HasValue)
            {
                var gallery = db.Categorys.FirstOrDefault(p => p.CategoryId == categoryid);
                if (gallery != null) ViewBag.GalleryName = gallery.Name;
            }
            if (Products == null) return RedirectToAction("index", "home");
            return View(Products);
        }
    }
}
