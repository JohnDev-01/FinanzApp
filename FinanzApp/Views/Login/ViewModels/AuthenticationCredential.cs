using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanzApp.Views.Login.ViewModels
{
	public class AuthenticationCredential
	{
		public static FirebaseAuthClient authClient = new FirebaseAuthClient(new FirebaseAuthConfig()
		{
			ApiKey = "AIzaSyA0CNu_iB3hTCq6kGxnxZGEsbGoBhrtRNc",
			AuthDomain = "finanzapp-5bbca.firebaseapp.com",
			Providers = new FirebaseAuthProvider[]
						{new EmailProvider()}
		});

	}
}
