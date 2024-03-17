using FinanzApp.Views.Login.Model;
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
				var iduser  = userCredential.User.Uid;
			
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
				await CrossCloudFirestore.Current
					.Instance
					.Collection("Users")
					.AddAsync(muser);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		public static async Task<bool> LoginWithCredential(string Email, string Password)
		{
			try
			{
				var clientAuth = AuthenticationCredential.authClient;
				var response = await clientAuth.SignInWithEmailAndPasswordAsync(Email, Password);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
