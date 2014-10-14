using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Biz4CMS.ViewModels;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PictureController : Controller
    {
        //
        // GET: /Admin/Picture/
        Biz4Db db = new Biz4Db();
        public ActionResult Index(int ProductId)
        {
            //var pictures = db.Pictures.Where(p => p.ProductId == ProductId).OrderByDescending(p => p.PictureId).ToList();
            ViewBag.ProductId = ProductId;
            return View();
        }


        // GET: /Admin/Picture/Create

        public ActionResult Create()
        {
            //var Picture = new Picture();
            //ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "Picture").ToList();
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request, int ProductId)
        {
            var Pictures = db.Pictures.Where(p => p.ProductId == ProductId).OrderByDescending(p => p.PictureId).Select(p => new PictureDto()
            {
                Name = p.Name,
                PictureId = p.PictureId,
                Tags = p.Tags,
                Src = p.Src,
                ProductFolder = p.Product.Folder
            }).ToList();
            return this.Json(Pictures.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PictureDto> pictures, int ProductId)
        {
            var results = new List<PictureDto>();

            if (pictures != null && ModelState.IsValid)
            {
                foreach (var picture in pictures)
                {
                    var pic = new Picture(picture, ProductId);
                    db.Pictures.Add(pic);
                    db.SaveChanges();
                    results.Add(picture);
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult Edit([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<PictureDto> pictures, int Productid)
        {
            if (pictures != null && ModelState.IsValid)
            {
                foreach (var picture in pictures)
                {
                    var target = db.Pictures.FirstOrDefault(p => p.PictureId == picture.PictureId);
                    if (target != null)
                    {
                        target.Name = picture.Name;
                        target.Tags = picture.Tags;
                        target.Src = picture.Src;
                        db.SaveChanges();
                    }
                }
            }

            return Json(pictures.ToDataSourceResult(request, ModelState));
        }


        [HttpPost]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<Picture> pictures)
        {
            if (pictures.Any())
            {
                foreach (var picture in pictures)
                {
                    var PictureToDelete = db.Pictures.First(p => p.PictureId == picture.PictureId);
                    if (PictureToDelete != null)
                    {
                        db.Pictures.Remove(PictureToDelete);
                    }
                }
                db.SaveChanges();
            }

            return Json(pictures.ToDataSourceResult(request, ModelState));

        }
    }
}
