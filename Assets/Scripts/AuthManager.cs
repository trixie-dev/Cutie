using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.Events;

public static class AuthManager
{
    private static FirebaseAuth auth;
    private static FirebaseUser user;
    

    public static void Initialize()
    {
        auth = FirebaseAuth.DefaultInstance;
        CheckIfUserIsLoggedIn();

    }
    public static bool CheckIfUserIsLoggedIn()
    {
        user = auth.CurrentUser;
        if (user != null)
        {
            Debug.Log("User is logged in: " + user.Email);
            return true;
        }
        else
        {
            Debug.Log("No user is logged in.");
            return false;
        }
    }

    public static void RegisterUser(string email, string password, System.Action<bool, string> callback)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread<AuthResult>(task => {
            if (task.IsCanceled)
            {
                callback(false, "RegisterUser was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                callback(false, "RegisterUser encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        callback(true, null);

    }

    public static void LoginUser(string email, string password, System.Action<bool, string> callback)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread<AuthResult>(task => {
            if (task.IsCanceled)
            {
                callback(false, "LoginUser was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                callback(false, "LoginUser encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
        callback(true, null);
    }

    public static void SignOut()
    {
        auth.SignOut();
        Debug.Log("User signed out.");
    }
}