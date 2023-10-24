using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Urun
{
    public class UrunViewModel
    {
        public int Urunid { get; set; }
        public string UrunAd { get; set; }
        public string Marka { get; set; }
        public int Stok { get; set; }
    }
}
