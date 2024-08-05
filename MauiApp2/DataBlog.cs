using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2
{
    public class DataBlog
    {
        public string Id { get; set; } // normaal is het een int
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LikeCounter { get; set; }
        public int DislikeCounter { get; set; }
    }
}
