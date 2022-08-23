using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using System;

namespace BlazorApp.Api
{
    public class DrawText
    {
        private readonly ILogger<DrawText> _logger;

        public DrawText(ILogger<DrawText> log)
        {
            _logger = log;
        }

        [FunctionName("DrawText")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "s", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **s** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "image/png", bodyType: typeof(string), Description = "The Image response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "DrawText/{width:int}/{height:int}")] HttpRequest req, int width, int height)
        {
            _logger.LogInformation("DrawText function processed a request.");
            
            var contentType = "image/png";
            var imageWidth = width;
            var imageHeight = height;

            StringBuilder s = new StringBuilder();
            using var ms = new MemoryStream();

            using Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(Color.Gray), 0, 0, imageWidth, imageHeight);

            using (Font font = new Font(FontFamily.GenericMonospace, 32, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Pixel))
            {
                Rectangle rect1 = new Rectangle(0, 0, imageWidth, imageHeight);

                var stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                graphics.DrawString($"{imageWidth}*{imageHeight}", font, Brushes.White, rect1, stringFormat);
                graphics.DrawRectangle(Pens.Black, rect1);
            }

            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            return new FileContentResult(ms.ToArray(), contentType)
            {
                FileDownloadName = Guid.NewGuid() + ".png"
            };
        }
    }
}
