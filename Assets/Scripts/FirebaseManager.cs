using UnityEngine.Events;
using UnityEngine;

public static class FirebaseManager
{
    public static UnityAction OnInitialized;
    

    public static void Initialize()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                AuthManager.Initialize();
                OnInitialized?.Invoke();
                Debug.Log("Firebase dependencies are available");
            }
            else
            {
                Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
        
    }
    
    
}