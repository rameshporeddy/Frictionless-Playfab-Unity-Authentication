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
        fabAuthentication.LoginWithEmail(emailfield.text, passwordfield.text);
    }
    public void OnEmailAuthenticationFailed(string errormsg)
    {
        gameObject.SetActive(true);
        failReasonText.text = errormsg;
    }
}
