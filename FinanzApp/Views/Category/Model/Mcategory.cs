using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzApp.Views.Category.Model
{
	public class Mcategory
	{
		public string Key { get; set; }
		public string Iduser { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
        public bool EnableView { get; set; }

		//To Know if category is selected in the view 
		public string BorderColor { get; set; }
    }
}
