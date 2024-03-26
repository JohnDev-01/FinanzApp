using FinanzApp.Service;
using FinanzApp.Views.Login.Model;
using Firebase.Auth;
using Firebase.Database.Query;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzApp.Views.Login.ViewModels
{
	public class VMuser
	{
		public static async Task<string> CreateUserAuth(MuserAuth muser)
		{
			try
			{
				var clientAuth = AuthenticationCredential.authClient;


				var userCredential = await clientAuth.CreateUserWithEmailAndPasswordAsync(muser.Email, muser.Password);
				Preferences.Set("TokenAuth", await userCredential.User.GetIdTokenAsync());
				var iduser = userCredential.User.Uid;

				return iduser;
			}
			catch (Exception ex)
			{
				return "EmailExists";
			}
		}
		public static async Task<bool> CreateUserTable(Muser muser)
		{
			try
			{
				var iduser = (await Conection.firebase
					.Child("Users")
					.PostAsync(muser)).Key;
				Preferences.Set("Iduser", iduser);
				return true;
			}
			catch (Exception ex)
			{
				var message = ex.Message;
				return false;
			}
		}
		public static async Task<bool> LoginWithCredential(string Email, string Password)
		{
			try
			{
				var clientAuth = AuthenticationCredential.authClient;
				var response = await clientAuth.SignInWithEmailAndPasswordAsync(Email, Password);
				Preferences.Set("Iduser", response.User.Uid);

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public static string GetIduserLogin()
		{
			return Preferences.Get("Iduser", "NOT FOUND");
		}
		public static async Task<Muser> GetMuser(string Iduser)
		{
			try
			{
				var userModel = (await Conection.firebase
					.Child("Users")
					.OrderByKey()
					.OnceAsync<Muser>())
					.Where(a => a.Object.UIDusuario == Iduser)
					.FirstOrDefault();

				return userModel.Object;

			}
			catch (Exception ex)
			{
				return null;
			}

		}
		private static Color HexToColor(string hexColor)
		{
			hexColor = hexColor.Replace("#", ""); // Elimina el carácter '#' si está presente

			byte red = Convert.ToByte(hexColor.Substring(0, 2), 16);
			byte green = Convert.ToByte(hexColor.Substring(2, 2), 16);
			byte blue = Convert.ToByte(hexColor.Substring(4, 2), 16);

			return new Color(red / 255f, green / 255f, blue / 255f);
		}
		public static async Task<Minitials> IconDrawing()
		{
			//Get UID 
			var idUser = GetIduserLogin();
			//Get User Model
			var userModel = await GetMuser(idUser);


			//Process information from client
			var partesNombre = userModel.Name.Split(' ');
			string initials = "";
			try
			{
				for (int i = 0; i < partesNombre.Length && i < 2; i++)
				{
					initials += partesNombre[i][0];
				}
				initials = initials.ToUpper();
			}
			catch (Exception)
			{

			}


			var modelInitials = new Minitials()
			{
				BackgroundColor = HexToColor(userModel.Color),
				Initials = initials
			};
			return modelInitials;
		}
	}
}
