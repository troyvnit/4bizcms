using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Biz4CMS.ViewModels;
using System.Configuration;

namespace Biz4CMS.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/
        Biz4Db db = new Biz4Db(); 
        public ActionResult Index(int? categoryid, int? page)
        {
            int pageSize =0 ;
            int.TryParse(ConfigurationManager.AppSettings["PageSize"].ToString(), out pageSize);
            if (pageSize == 0) { pageSize = 8; }
            int index = page.HasValue ? page.Value : 1;
            ViewBag.PageIndex = index;
            
            var articles = db.Articles.Where(p => (!categoryid.HasValue || p.CategoryId == categoryid) && p.Active).OrderByDescending(p => p.ArticleId).Skip((index -1)* pageSize).Take(pageSize).Select(p => new BriefArticleDto()
            { 
               ArticleId =p.ArticleId , 
               Avatar =p.Avatar ,
               Description = p.Description ,
               Title = p.Title

            }).ToList();
            if (articles == null || articles.Count==0) return RedirectToAction("index", "home");
            
            if (categoryid.HasValue){
                ViewBag.Title = db.Categorys.Where(p => p.CategoryId == categoryid).FirstOrDefault().Name;
                ViewBag.CategoryID = categoryid.Value; 
            }else{
                ViewBag.Title ="Tin tức";
                ViewBag.CategoryID = 0;
            }
            ViewBag.TotalPage = (int)Math.Ceiling(((double)db.Articles.Where(p => (!categoryid.HasValue || p.CategoryId == categoryid) && p.Active).Count()) / pageSize);
            return View(articles);
        }
        public ActionResult Details(int id)
        {
            var article = db.Articles.Where(p => p.ArticleId == id &&  p.Active).FirstOrDefault();
            if (article == null) return  RedirectToAction("index", "home");
            ViewBag.RelatedArticles = db.Articles.Where(p => p.CategoryId == article.CategoryId && p.Active && p.ArticleId < id).OrderByDescending(p=>p.ArticleId).Take(30).AsEnumerable().Select(p => new LinkDto() { Title = p.Title, Link = string.Format("/article/details/{0}", p.ArticleId) }).ToList();
            return View(article);
        }

    }
}
