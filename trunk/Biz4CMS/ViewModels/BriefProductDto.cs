using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz4CMS.ViewModels
{
    public class BriefProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        //public string Tags { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int NumofViews { get; set; }
        //public virtual List<Picture> Pictures { get; set; }
        //public string Avatar { get; set; }
        //public string Folder { get; set; }
        //public string ImagesJson { get; set; }
        public string Folder { get; set; }
       
    }
}