using System.Collections.Generic;

namespace UPC.Talentos.BL.BE
{
    public class GeneralResultBE
    {
        public string institution { get; set; }
        public string testDate { get; set; }
        public List<GeneralTalentBE> lstTalentsSpecifics { get; set; }
        public List<GeneralTalentBE> lstTalentsMostDe { get; set; }
    }
}
