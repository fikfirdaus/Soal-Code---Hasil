using System.ComponentModel.DataAnnotations;

namespace Backend_BPKB.Models
{
    public class tr_bpkb
    {
        [Key]
        public string agreement_number { get; set; }
        public string bpkb_no { get; set; }
        public string branch_id { get; set; }
        public DateTime bpkb_date { get; set; }
        public string faktur_no { get; set; }
        public DateTime faktur_date { get; set; }
        public string location_id { get; set; }
        public string police_no { get; set; }
        public DateTime bpkb_date_id { get; set; }
        public string created_by { get; set; }
        public DateTime created_on { get; set; }
        public string last_updated_by { get; set; }
        public DateTime last_updated_on { get; set; }

    }
}
