namespace WebHost.Controllers
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;

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

        [HttpGet]
        public IActionResult QrUrl()
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode("The text which should be encoded.", QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            var qr1CodeImage  = new byte[0];

            using (var ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, ImageFormat.Png);
                qr1CodeImage = ms.ToArray();
            }

            // var qr1Code = new PngByteQRCode(qrCodeData);
            // qr1CodeImage = qr1Code.GetGraphic(20);

            return File(qr1CodeImage, "image/png");
        }
    }
}
