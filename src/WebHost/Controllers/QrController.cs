namespace WebHost.Controllers
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using QRCoder;

    [ApiController]
    [Route("[controller]")]
    public class QrController : ControllerBase
    {
        private readonly ILogger<QrController> _logger;

        public QrController(ILogger<QrController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("text")]
        [HttpGet("text/{message}")]
        public IActionResult Text(string message)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(message, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            var qr1CodeImage = new byte[0];

            using (var ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                qr1CodeImage = ms.ToArray();
            }

            return File(qr1CodeImage, "image/png");
        }

        [HttpGet("url/{url}")]
        public IActionResult QrUlr(string url)
        {
            var generator = new PayloadGenerator.Url(HttpUtility.UrlDecode(url));
            var payload = generator.ToString();

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            var qr1CodeImage = new byte[0];

            using (var ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                qr1CodeImage = ms.ToArray();
            }

            return File(qr1CodeImage, "image/png");
        }
    }
}
