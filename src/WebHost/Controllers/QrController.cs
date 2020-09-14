namespace WebHost.Controllers
{
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web;

    using Microsoft.AspNetCore.Mvc;
    using QRCoder;

    [ApiController]
    [Route("[controller]")]
    public class QrController : ControllerBase
    {
        [HttpGet("text")]
        [HttpGet("text/{message}")]
        public IActionResult Text(string message)
        {
            var payload = Decode(message);
            return CreateQrImage(payload);
        }

        [HttpGet("url/{url}")]
        public IActionResult QrUlr(string url)
        {
            var generator = new PayloadGenerator.Url(Decode(url));
            var payload = generator.ToString();
            return CreateQrImage(payload);
        }

        [HttpGet("mail/{to}/{subject}/{message}")]
        [HttpGet("email/{to}/{subject}/{message}")]
        public IActionResult QrUlr(string to, string subject, string message)
        {
            var generator = new PayloadGenerator.Mail(Decode(to), Decode(subject), Decode(message));
            var payload = generator.ToString();
            return CreateQrImage(payload);
        }

        private static string Decode(string input)
        {
            return HttpUtility.UrlDecode(input);
        }

        private IActionResult CreateQrImage(string payload)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new QRCode(qrCodeData);
            using var qrCodeImage = qrCode.GetGraphic(20);
            using var ms = new MemoryStream();
            qrCodeImage.Save(ms, ImageFormat.Png);
            return File(ms.ToArray(), "image/png");
        }
    }
}
