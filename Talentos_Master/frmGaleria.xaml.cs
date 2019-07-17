using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace Talentos_Master
{
	public partial class frmGaleria : UserControl
	{
        List<TalentoUC> meidentifica = new List<TalentoUC>();

		public frmGaleria()
		{
			// Required to initialize variables
			InitializeComponent();

            for (int i = 0; i < 25; i++)
            {
              meidentifica.Add(new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones.",false,0));
               // meidentifica.Add(new TalentoUC(1, "images/talentos/images/Image1.png", "Analítico", "Las personas abiertas a los demás están dispuestas a establecer relaciones con personas distintas a ellas mismas. Son inclusivos a la hora de organizar sus reuniones, se preocupan por las personas que están aisladas o excluidas. Encuentran en cada persona una característica individual que las distingue y aprecia las relaciones."));
            }

            talentosListBox2.ItemsSource = meidentifica;
            
		}
    }
}