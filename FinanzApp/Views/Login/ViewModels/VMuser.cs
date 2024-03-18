﻿using FinanzApp.Views.Login.Model;
using Firebase.Auth;
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
				var iduser  = await CrossCloudFirestore.Current
					.Instance
					.Collection("Users")
					.AddAsync(muser);
				//Preferences.Set("Iduser", iduser.Firestore.Document);
				return true;
			}
			catch (Exception ex )
			{
				var message  = ex.Message;
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
