using System.Drawing.Imaging;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using TagsCloud.Visualization.LayouterCores;
using TagsCloud.WebApi.Services;

namespace TagsCloud.WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class ImageController : Controller
    {
        private readonly ILayouterCore layouterCore;
        private readonly SimpleTextReader simpleTextReader;

        public ImageController(ILayouterCore layouterCore, SimpleTextReader simpleTextReader)
        {
            this.layouterCore = layouterCore;
            this.simpleTextReader = simpleTextReader;
        }
        
        [HttpGet("image")]
        public IActionResult GetImage(string text)
        {
            simpleTextReader.SetText(text);
            
            using var image = layouterCore.GenerateImage();
            
            using var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            
            return File(ms.ToArray(), "image/png");
        }
    }
}