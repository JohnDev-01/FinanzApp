using FinanzApp.Service;
using FinanzApp.Views.Category.Model;
using FinanzApp.Views.Login.ViewModels;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzApp.Views.Category.ViewModel
{
	public  class VMcategory
	{
		public static async Task InsertCategory(Mcategory model)
		{
			try
			{
				await Conection.firebase
					.Child("Category")
					.Child(model.Iduser)
					.PostAsync(model);

			}
			catch (Exception ex)
			{
                await Console.Out.WriteLineAsync(ex.Message + ". In VMcategory.InsertCategory");
            }
		}
		public static async Task<List<Mcategory>> GetListCategoryFromIdUser()
		{
			List<Mcategory> listReturn=new List<Mcategory>();
			try
			{
				var iduser = VMuser.GetIduserLogin();
				var listCategory = (await Conection.firebase
					.Child("Category")
					.Child(iduser)
					.OrderByKey()
					.OnceAsync<Mcategory>()).Where(a=> a.Object.EnableView == true);
				foreach (var item in listCategory)
				{
					var model = item.Object;
					model.Key = item.Key;
					model.BorderColor = "Transparent";
					listReturn.Add(model);
				}
			}
			catch (Exception ex )
			{
                await Console.Out.WriteLineAsync(ex.Message + ". In VMcategory.GetCategoryFromIdUser");
            }
			return listReturn;

		}
		public static async Task DeleteCategory(string key)
		{
			try
			{
				var iduser = VMuser.GetIduserLogin();

				var categoryModel = (await Conection.firebase
					.Child("Category")
					.Child(iduser)
					.OnceAsync<Mcategory>())
					.Where(a => a.Key == key)
					.FirstOrDefault();

				categoryModel.Object.EnableView = false;

				await Conection.firebase
					.Child("Category")
					.Child(iduser)
					.Child(key)
					.PutAsync(categoryModel.Object);
			}
			catch (Exception ex)
			{
				await Console.Out.WriteLineAsync(ex.Message + ". In VMcategory.InsertCategory");
			}
		}
	}
}
