using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Biz4CMS.Areas.Admin.Controllers
{
   [Authorize]
    public class ArticleController : Controller
    {
        //
        // GET: /bo/Article/
        Biz4Db db = new Biz4Db(); 
        public ActionResult Index()
        {
                                return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request) {
            var articles = db.Articles.OrderByDescending(p=> p.ArticleId).ToList();
            return this.Json(articles.ToDataSourceResult(request),JsonRequestBehavior.AllowGet );
        
        }
        
        // GET: /bo/Article/Create

        public ActionResult Create()
        {
            var article = new Article();
            article.CreatedDate = DateTime.Now;
            article.PublicDate = DateTime.Now;
            article.Order = 0;
            article.Active = true;
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "Article").ToList();
            return View(article);
        }

        //
        // POST: /bo/Article/Create

        [HttpPost]
        public ActionResult Create(Article  model)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(model);
                db.SaveChanges(); 
            }
            return RedirectToAction("Index");
           
        }
        [HttpPost]
        public ActionResult Edit(Article model)
        {
            if (ModelState.IsValid)
            {
                var updateArticle = db.Articles.FirstOrDefault(p => p.ArticleId == model.ArticleId);
                if (updateArticle != null)
                {
                    TryUpdateModel(updateArticle);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /bo/Article/Edit/5

        public ActionResult Edit(int id)
        {
            var article = db.Articles.FirstOrDefault(p => p.ArticleId == id);
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "Article").ToList();
            return View("Create",article);
        }

        //
        

        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, Article  model)
        {
            var articleToDelete = db.Articles.First(p => p.ArticleId  == model.ArticleId );
            if (articleToDelete != null)
            {
                db.Articles.Remove(articleToDelete);
                db.SaveChanges();
            }
            return Json(new[] { articleToDelete }.ToDataSourceResult(request));
        }
    }
}
