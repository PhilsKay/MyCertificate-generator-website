using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using System.Web;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Font;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Utilities.IO;
using PdfDownload.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting.Internal;

namespace PdfDownload.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IWebHostEnvironment Environment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            Environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
     /*   [HttpGet]
        public IActionResult Download()
        {
            string filePath = "~/file/Certificate.pdf";
            // Response.Headers.Add("Content-Disposition", "inline; filename=Certificate.pdf");
            Response.Headers.Add("Content-Deposition", "inline; filname=Certificate.pdf");
            return File(filePath, "application/pdf");
        }*/
        [HttpPost]
        public IActionResult DownLoad(CertificateForm obj)
        {
            if (ModelState.IsValid)
            {
                //Get the wwwroot path
                var pd = this.Environment.WebRootPath;
                byte[] pdfBytes;
                FileInfo path = new FileInfo(pd + "\\file\\Certificate.pdf");
                using (var reader = new PdfReader(path))
                using (var os = new MemoryStream())
                using (var writer = new PdfWriter(os))
                using (var pdf = new PdfDocument(reader, writer))
                using (var doc = new Document(pdf))
                {
                    PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLDOBLIQUE);
                    Paragraph para = new Paragraph(obj.Name).SetFontSize(60).SetFont(font);
                    para.SetFixedPosition(0, 300, 850).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
                    doc.Add(para);
                    doc.Close();
                    doc.Flush();
                    pdfBytes = os.ToArray();
                }
                return new FileContentResult(pdfBytes, "application/pdf");
            }
            ModelState.AddModelError(obj.Name, "Invalid Name Format");
            return View("Index");
           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
