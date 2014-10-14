using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biz4CMS.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Biz4CMS.ViewModels
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Folder { get; set; }
        public string Avatar { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public bool Active { get; set; }
        public string ImagesJson { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ProductDto(Product model)
        {
            ProductId = model.ProductId;
            CategoryId = model.CategoryId;
            Description = model.Description;
            Content = model.Content;
            Avatar = model.Avatar;
            Tags = model.Tags;
            Folder = model.Folder;
            ImagesJson = model.ImagesJson;
            Name = model.Name;
            Active = model.Active;
        }
        public ProductDto()
        { }
    }
}