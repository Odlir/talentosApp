using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Talentos_Master.Web
{
    public partial class frmRadar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int c1 = Convert.ToInt16(Request.QueryString["color1"]);
            int c2 = Convert.ToInt16(Request.QueryString["color2"]);
            int c3 = Convert.ToInt16(Request.QueryString["color3"]);
            int c4 = Convert.ToInt16(Request.QueryString["color4"]);
            int c5 = Convert.ToInt16(Request.QueryString["color5"]);
            int c6 = Convert.ToInt16(Request.QueryString["color6"]);

            //int c1 = 20;
            //int c2 = 10;
            //int c3 = 30;
            //int c4 = 40;
            //int c5 = 27;
            //int c6 = 39;


            int total = c1 + c2 + c3 + c4 + c5 + c6;
            float p1 = (c1 / total)*100;
            double p2 = c2 / total; ;
            double p3 = c3 / total; ;
            double p4 = c4 / total; ;
            double p5 = c5 / total; ;
            double p6 = c6 / total; ;

            String url = "http://chart.apis.google.com/chart?cht=r&chd=t:" + c1.ToString() + "," + c2.ToString() + "," + c3.ToString() + "," + c4.ToString() + "," + c5.ToString() + "," + c6.ToString() + "&chs=650x400&chl=Orientado%20a%20la%20Ejecución%20(" + c1.ToString() + " puntos)|Orientado%20al%20Pensamiento%20(" + c2.ToString() + " puntos)|Orientado%20a%20la%20Innovación%20(" + c3.ToString() + " puntos)|Orientado%20al%20Liderazgo%20(" + c4.ToString() + " puntos)|Orientado%20a%20las%20Personas%20(" + c5.ToString() + " puntos)|Orientado%20a%20la%20Estructura%20(" + c6.ToString() + " puntos)";

            img.ImageUrl = url;

        }
    }
}
