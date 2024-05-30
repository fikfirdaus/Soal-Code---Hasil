using System.ComponentModel.DataAnnotations;

namespace Backend_BPKB.Models
{
    public class ms_user
    {
        [Key]
        public long user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public bool is_active { get; set; }
    }
}
