using Backend_BPKB.DataAccessLayer;
using Backend_BPKB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_BPKB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataBPKB : BaseController
    {
        private readonly ConnectionDB _db;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataBPKB(ConnectionDB db, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(configuration)
        {
            _db = db;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("CheckLogin")]
        public IActionResult CheckLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Username or password is empty.");
            }

            var user = _db.ms_user.FirstOrDefault(u => u.user_name == username && u.password == password);

            // Jika pengguna ditemukan, kembalikan respons OK
            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("username", username);
                return Ok();
            }

            // Jika tidak, kembalikan respons Unauthorized
            return Unauthorized("Invalid username or password.");
        }

        [HttpPost("InputNewBPKB")]
        public IActionResult InputNewBPKB(string agreementNumber, string branchId, string noBPKB, DateTime tglBPKBIn, DateTime tglBPKB, string noFaktur, DateTime tglFaktur, string noPolisi, string lokasiPenyimpanan)
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("username");

            DateTime tglBPKBInWithoutTime = tglBPKBIn.Date;
            DateTime tglBPKBWithoutTime = tglBPKB.Date;
            DateTime tglFakturWithoutTime = tglFaktur.Date;

            tr_bpkb newBPKB = new tr_bpkb
            {
                agreement_number = agreementNumber,
                branch_id = branchId,
                bpkb_no = noBPKB,
                bpkb_date_id = tglBPKBInWithoutTime,
                bpkb_date = tglBPKBWithoutTime,
                faktur_no = noFaktur,
                faktur_date = tglFakturWithoutTime,
                police_no = noPolisi,
                location_id = lokasiPenyimpanan,
                created_by = username,
                created_on = DateTime.Now,
                last_updated_by = username,
                last_updated_on = DateTime.Now
            };

            _db.tr_bpkb.Add(newBPKB);
            _db.SaveChanges();



            return Ok("Data BPKB baru berhasil disimpan");
        }
    }
}
