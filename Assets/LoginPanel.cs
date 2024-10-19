using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailInput;
    [SerializeField] private TMP_InputField _passwordInput;

    [SerializeField] private TextMeshProUGUI _errorText;

    public void Start()
    {
        Initialize();
    }
    
    public void Initialize()
    {
        if (!_emailInput)
            _emailInput = transform.Find("EmailInput").GetComponent<TMP_InputField>();
        if (!_passwordInput)
            _passwordInput = transform.Find("PasswordInput").GetComponent<TMP_InputField>();
        if (!_errorText)
            _errorText = transform.Find("ErrorText").GetComponent<TextMeshProUGUI>();
        
    }
    
    public void SignIn()
    {
        AuthManager.LoginUser(_emailInput.text, _passwordInput.text, (success, message) =>
        {
            if (success)
            {
                _errorText.text = "Logged in successfully.";
                Debug.Log("User logged in successfully.");
            }
            else
            {
                _errorText.text = "Failed to log in user: " + message;
                Debug.LogError("Failed to log in user: " + message);
            }
        });
    }
    
    public void Register()
    {
        AuthManager.RegisterUser(_emailInput.text, _passwordInput.text, (success, message) =>
        {
            if (success)
            {
                _errorText.text = "Registered successfully.";
                Debug.Log("User registered successfully.");
            }
            else
            {
                _errorText.text = "Failed to register user: " + message;
                Debug.LogError("Failed to register user: " + message);
            }
        });
    }
}
