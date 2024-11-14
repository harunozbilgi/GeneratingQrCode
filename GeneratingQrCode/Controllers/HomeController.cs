using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GeneratingQrCode.Models;
using QRCoder;

namespace GeneratingQrCode.Controllers;

public class HomeController() : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateQrCode(QrCodeModel qrCodeModel)
    {
        using var qrCodeGenerator = new QRCodeGenerator();
        using var qrCodeData = qrCodeGenerator.CreateQrCode(qrCodeModel.QrCodeText ?? "", QRCodeGenerator.ECCLevel.Q);
        var qrCodePng = new PngByteQRCode(qrCodeData).GetGraphic(20);
        
        var convertToBase64 = Convert.ToBase64String(qrCodePng);
        
        ViewData["QrCodeUri"] = $"data:image/png;base64,{convertToBase64}";

        return View("Index", qrCodeModel);
    }

    public IActionResult DownloadQrCode(string qrCodeText, string qrCodeName)
    {
        using var qrCodeGenerator = new QRCodeGenerator();
        using var qrCodeData = qrCodeGenerator.CreateQrCode(qrCodeText ?? "", QRCodeGenerator.ECCLevel.Q);
        var qrCodeSvg = new SvgQRCode(qrCodeData).GetGraphic(30);

        return File(System.Text.Encoding.UTF8.GetBytes(qrCodeSvg), "image/svg+xml", $"{qrCodeName}.svg");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}