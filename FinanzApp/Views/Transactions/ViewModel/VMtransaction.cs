using FinanzApp.Service;
using FinanzApp.Views.Transactions.Model;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzApp.Views.Transactions.ViewModel
{
	public class VMtransaction
	{
		public static async Task<bool> InsertTransaction(Mtransactions transaction)
		{
			try
			{
				await Conection.firebase
					.Child("Transactions")
					.Child(transaction.Iduser)
					.PostAsync(transaction);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
