using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace Frontend_BPKB.Controllers
{
    public class Login : Controller
    {
        private readonly HttpClient _httpClient;

        public Login(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string inputUsername, string inputPassword)
        {
            try
            {
                if (inputUsername != null && inputPassword != null)
                {
                    var encodedUsername = System.Net.WebUtility.UrlEncode(inputUsername);
                    var encodedPassword = System.Net.WebUtility.UrlEncode(inputPassword);

                    var url = $"https://localhost:7074/DataBPKB/CheckLogin?username={encodedUsername}&password={encodedPassword}";

                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // Menerima sertifikat apa pun

                    HttpClient client = new HttpClient(clientHandler);
                    var data = new Dictionary<string, string>
                    {
                        { "username", inputUsername },
                        { "password", inputPassword }
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
                else
                {
                    return RedirectToAction("Username or password is null.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        public async Task<IActionResult> Logout()
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            return RedirectToAction("Index", "Login");
        }
    }
}
