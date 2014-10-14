using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Biz4CMS.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Biz4CMS.ViewModels;
using Newtonsoft.Json;

namespace Biz4CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        //
        // GET: /Admin/Product/
        Biz4Db db = new Biz4Db();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Get([DataSourceRequest]DataSourceRequest request)
        {
            var Products = db.Products.OrderByDescending(p => p.ProductId).Select(model => new ProductDto()
            { 
             ProductId = model.ProductId,
            CategoryId = model.CategoryId,
            Description = model.Description,
            Tags = model.Tags,
            Folder = model.Folder,
            ImagesJson = model.ImagesJson,
            Name = model.Name,
            CategoryName = model.Category.Name
            
            }).ToList();
            return this.Json(Products.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        // GET: /Admin/Product/Create

        public ActionResult Create()
        {
            var Product = new ProductDto();
            //Product.CreatedDate = DateTime.Now;
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "Product").ToList();

            return View(Product);
        }

        //
        // POST: /Admin/Product/Create

        [HttpPost]
        public ActionResult Create(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                Product Product = new Product(model);
                if (model.ImagesJson != null)
                {
                    List<FileInfo> images = JsonConvert.DeserializeObject<List<FileInfo>>(model.ImagesJson);
                    if (images.Any(p => p.EntryType == 0))
                    {
                        Product.Pictures = new List<Picture>();
                        foreach (var image in images.Where(p => p.EntryType == 0))
                        {
                            Picture pic = new Picture();
                            pic.Src = image.Name;
                            pic.Size = image.Size;
                            Product.Pictures.Add(pic);
                        }
                    }
                }
                db.Products.Add(Product);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var updateProduct = db.Products.FirstOrDefault(p => p.ProductId == model.ProductId);
                if (updateProduct != null)
                {
                    TryUpdateModel(updateProduct);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");

        }
        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            var ProductDto = new ProductDto(  db.Products.FirstOrDefault(p => p.ProductId == id));
            ViewBag.Categorys = db.Categorys.Where(p => p.Tag == "Product").ToList();
            return View("Create", ProductDto);
        }

        //


        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, ProductDto model)
        {
            var ProductToDelete = db.Products.First(p => p.ProductId == model.ProductId);

            if (ProductToDelete != null)
            {
                db.Products.Remove(ProductToDelete);
                db.SaveChanges();
            }

            return Json(new[] { ProductToDelete }.ToDataSourceResult(request));
        }
    }
}
