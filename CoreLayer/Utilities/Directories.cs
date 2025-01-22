using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Utilities
{
    public class Directories
    {
        public const string PostImage = "wwwroot/images/posts";
        public static string GetPostImage(string imageName)=>$"{PostImage.Replace("wwwroot","")}/{imageName}";
    }
}
