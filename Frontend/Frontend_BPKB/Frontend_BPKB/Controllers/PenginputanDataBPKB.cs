using Microsoft.AspNetCore.Mvc;

namespace Frontend_BPKB.Controllers
{
    public class PenginputanDataBPKB : Controller
    {
        private readonly HttpClient _httpClient;

        public PenginputanDataBPKB(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InputNewBPKB(string agreementNumber, string branchId, string noBPKB, DateTime tglBPKBIn, DateTime tglBPKB, string noFaktur, DateTime tglFaktur, string noPolisi, string lokasiPenyimpanan)
        {
            var encodedAgreementNumber = System.Net.WebUtility.UrlEncode(agreementNumber);
            var encodedBranchId = System.Net.WebUtility.UrlEncode(branchId);
            var encodedNoBPKB = System.Net.WebUtility.UrlEncode(noBPKB);
            var encodedTglBPKBIn = System.Net.WebUtility.UrlEncode(tglBPKBIn.ToString("yyyy-MM-dd HH:mm:ss"));
            var encodedTglBPKB = System.Net.WebUtility.UrlEncode(tglBPKB.ToString("yyyy-MM-dd HH:mm:ss"));
            var encodedNoFaktur = System.Net.WebUtility.UrlEncode(noFaktur);
            var encodedTglFaktur = System.Net.WebUtility.UrlEncode(tglFaktur.ToString("yyyy-MM-dd HH:mm:ss"));
            var encodedNoPolisi = System.Net.WebUtility.UrlEncode(noPolisi);
            var encodedLokasiPenyimpanan = System.Net.WebUtility.UrlEncode(lokasiPenyimpanan);

            var url = $"https://localhost:7074/DataBPKB/InputNewBPKB?agreementNumber={encodedAgreementNumber}&branchId={encodedBranchId}&noBPKB={encodedNoBPKB}&tglBPKBIn={encodedTglBPKBIn}&tglBPKB={encodedTglBPKB}&noFaktur={encodedNoFaktur}&tglFaktur={encodedTglFaktur}&noPolisi={encodedNoPolisi}&lokasiPenyimpanan={encodedLokasiPenyimpanan}";

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // Menerima sertifikat apa pun

            HttpClient client = new HttpClient(clientHandler);
            var data = new Dictionary<string, string>
                      {
                          { "agreementNumber", agreementNumber },
                          { "branchId", branchId },
                          { "noBPKB", noBPKB },
                          { "tglBPKBIn", tglBPKBIn.ToString("yyyy-MM-dd HH:mm:ss") },
                          { "tglBPKB", tglBPKB.ToString("yyyy-MM-dd HH:mm:ss") },
                          { "noFaktur", noFaktur },
                          { "tglFaktur", tglFaktur.ToString("yyyy-MM-dd HH:mm:ss") },
                          { "noPolisi", noPolisi },
                          { "lokasiPenyimpanan", lokasiPenyimpanan }
                      };

            var content = new FormUrlEncodedContent(data);

            try
            {
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "PenginputanDataBPKB");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Request failed: " + response.ReasonPhrase);
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, "Request failed: " + ex.Message);
            }
        }
    }
}
