using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Tradeguard2.Helpers
{
    public class ImageHelper 
    {

        static public string GetImageLink(string ImageName, string FolderName = "")
        {
           
            if (ImageName == null)
            {
                ImageName = "no-image.png";
                FolderName = "";
            }
            else
            {
                FolderName += FolderName != "" ? "/" : "";
            }
            return "~/Imagens/" + FolderName + ImageName;
        }

    }
}
