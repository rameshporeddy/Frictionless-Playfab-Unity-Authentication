using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmailForm : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField emailfield;
    public InputField passwordfield;
    public Text failReasonText;
    public PlayFabAuthentication fabAuthentication;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickedLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = emailfield.text };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        gameObject.SetActive(false);
        fabAuthentication.updateUserprofile();
    }

    private void OnLoginFailure(PlayFabError error)
    {
        failReasonText.text = error.ErrorMessage;
    }
}
