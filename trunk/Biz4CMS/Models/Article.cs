﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Biz4CMS.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        [StringLength(200)]
        public String Title { get; set; }
        [StringLength(1000)]
        public String Description { get; set; }
        [AllowHtml]
        public String Content { get; set; }
        [StringLength(200)]
        public String Keyword { get; set; }
        public bool Active { get; set; }
        public Int16 Order { get; set; }
        [StringLength(200)]
        public String Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublicDate { get; set; }
        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
    }
}