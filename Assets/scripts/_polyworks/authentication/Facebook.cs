using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using Facebook.Unity;

namespace Polyworks {
	public class Facebook : Singleton<Facebook>
	{
		/*
		public delegate void InitializationHandler(bool success);
		public event InitializationHandler OnFacebookInitialized;
		
		public delegate void LoginHandler(bool success, string userId);
		public event LoginHandler OnFacebookLogin; 
		
		public delegate void UnityHider(bool isGameShown); 
		public event UnityHider OnHideUnity; 
		
		public List<string> DEFAULT_PERMISSIONS = new List<string>() { "public_profile", "email", "user_friends" };
		
		private bool _isInitialized = false; 
		
		public bool GetIsInitialized() { 
			return _isInitialized;
		}
		
		public void LogIn() {
			FB.LogInWithReadPermissions(DEFAULT_PERMISSIONS, AuthCallback);
		}
		
		void Awake () {
		    if (!FB.IsInitialized) {
		        // Initialize the Facebook SDK
				FB.Init(InitCallback, _onHideUnity);
		    } else {
				_isInitialized = true;
		        // Already initialized, signal an app activation App Event
		        FB.ActivateApp();
		    }
		}

		private void InitCallback () {
			bool success = false; 
			
		    if (FB.IsInitialized) {
				success = true;
				_isInitialized = true;
		        // Signal an app activation App Event
		        FB.ActivateApp();
		        // Continue with Facebook SDK
		        // ...
		    } else {
		        Debug.Log("Failed to Initialize the Facebook SDK");
		    }
			if(OnFacebookInitalized != null) {
				OnFacebookInitialized(success);
			}
		}

		private void _onHideUnity (bool isGameShown) {
			if(OnHideUnity != null) {
				OnHideUnity(isGameShown);
			}
		    // if (!isGameShown) {
		    //     // Pause the game - we will need to hide
		    //     Time.timeScale = 0;
		    // } else {
		    //     // Resume the game - we're getting focus again
		    //     Time.timeScale = 1;
		    // }
		}
		
		private void AuthCallback(ILoginResult result) {
			bool success = false; 
			string id; 
			
		    if (FB.IsLoggedIn) {
				success = true;
		        // AccessToken class will have session details
		        var accessToken = Facebook.Unity.AccessToken.CurrentAccessToken;
		        // Print current access token's User ID
				id = accessToken.UserId;
		        Debug.Log(accessToken.UserId);
		        // Print current access token's granted permissions
		        foreach (string perm in accessToken.Permissions) {
		            Debug.Log(perm);
		        }
		    } else {
		        Debug.Log("User cancelled login");
		    }
			if(OnFacebookLogin != null) {
				OnFacebookLogin(success, id);
			}
		}
			*/
	}
}
// https://developers.facebook.com/docs/unity/examples