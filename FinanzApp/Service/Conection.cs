using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzApp.Service
{
	public class Conection
	{
		static string urlRealtimeDatabase = "https://finanzapp-5bbca-default-rtdb.firebaseio.com";
		public static FirebaseClient firebase = new FirebaseClient(urlRealtimeDatabase);
	}
}
