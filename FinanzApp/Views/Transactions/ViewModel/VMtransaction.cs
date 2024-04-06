using FinanzApp.Service;
using FinanzApp.Views.Category.ViewModel;
using FinanzApp.Views.Login.ViewModels;
using FinanzApp.Views.Transactions.Model;
using Firebase.Auth.Requests;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Packaging;
using System.Linq;
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

		private static dynamic ProcessParametesFromGetTransactions(string? typeTransaction, int days)
		{
			//Validation date
			DateTime time = DateTime.Now;
			if (days == 30)
			{
				time = DateTime.Now.AddDays(-30);
			}
			if (days == 60)
			{
				time = DateTime.Now.AddDays(-60);
			}
			if (days == 90)
			{
				time = DateTime.Now.AddDays(-90);
			}
			//Validation type transaccions
			var type = typeTransaction == "Créditos" ? "Credit" :
				typeTransaction == "Débitos" ? "Debit" :
				typeTransaction == "Todas" ? "All" : "The type of transaction was not identified";

			var objeto = new {date = time, type = type };
			return objeto;
		}

		public static async Task<List<Mtransactions>> GetTransactions(string? typeTransaction, int days)
		{
			try
			{
				//Get user id 
				var userId  = VMuser.GetIduserLogin();

				//Get Categories used from user 
				var listCategories = await VMcategory.GetListCategoryFromIdUser_Transactions();


				var anonimousObject = ProcessParametesFromGetTransactions(typeTransaction, days);
				DateTime dateLast = anonimousObject.date;
				string type =  anonimousObject.type;


				var list = (await Conection.firebase
					.Child("Transactions")
					.Child(userId)
					.OnceAsync<Mtransactions>())
					.Where(t => Convert.ToDateTime(t.Object.Fecha) >= dateLast)
					.OrderByDescending(a => Convert.ToDateTime(a.Object.Fecha))
					.ToList();

				if (type != "All")
				{
					list = list.Where(a => a.Object.Tipo == type).ToList();
				}
				var listReturn  =  new List<Mtransactions>();
				foreach (var item in list)
				{
					var model = new Mtransactions();
					model = item.Object;
					model.ID = item.Key;
					listReturn.Add(model);
				}
				listReturn = (from d in listReturn
							 join ca in listCategories on d.CategoriaID equals ca.Key
							 select new Mtransactions
							 {
								 CategoriaID=ca.Name,
								 Descripcion = d.Descripcion,
								 ID=d.ID,
								 Fecha=d.Fecha,
								 Tipo=d.Tipo,	
								 Iduser = d.Iduser,
								 ImagenCategoria= ca.Icon,
								 Monto = d.Tipo == "Credit" ? d.Monto : d.Monto*-1,
								 TextColorAmount = d.Tipo == "Credit" ? "#2ECC71" : "Red"
							 }).ToList();


				return listReturn;
			}
			catch (Exception)
			{
				return null;
			}
		}
		public static async Task<double> GetBalance()
		{
			try
			{
				//Get user id 
				var userId = VMuser.GetIduserLogin();

				//Get Categories used from user 
				var listCategories = await VMcategory.GetListCategoryFromIdUser_Transactions();


				var allTransaction = (await Conection.firebase
					.Child("Transactions")
					.Child(userId)
					.OnceAsync<Mtransactions>())
					.ToList();

				double debtCalculated = allTransaction
					.Where(a => a.Object.Tipo == "Debit")
					.Sum(a => a.Object.Monto);

				double creditCalculated = allTransaction
					.Where(a => a.Object.Tipo == "Credit")
					.Sum(a => a.Object.Monto);

				double balanceCalculated = creditCalculated - debtCalculated;
				return balanceCalculated < 0 ? 0 : balanceCalculated;
			}
			catch (Exception)
			{
				return 0;
			}
		}
	}
}
