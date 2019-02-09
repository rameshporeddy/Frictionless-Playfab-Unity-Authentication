using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFabAuthentication : MonoBehaviour
{
    // Start is called before the first frame update
    public UserProfile userprofile;
    public GameObject freshstartconfiramtionwindow;
    public GameObject emailform;


    private void Awake()
    {
        freshstartconfiramtionwindow.SetActive(false);
        emailform.SetActive(false);
    }
    void Start()
    {
        if (userprofile.email != null)
        {
            LoginWithEmail();
        }
        else
        {
            ShowFreshStartConfirmationWindow();
        }

    }

    private void ShowFreshStartConfirmationWindow()
    {
        freshstartconfiramtionwindow.SetActive(true);
    }
    public void RegisterNewUser()
    {
#if UNITY_EDITOR
        var request = new LoginWithCustomIDRequest { CustomId = "Ramesh3", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#elif UNITY_ANDROID
        
#endif
    }

    public void LoginWithEmail()
    {
        var request = new LoginWithEmailAddressRequest { Email = userprofile.email, Password = userprofile.password };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        emailform.SetActive(true);
        emailform.GetComponent<EmailForm>().failReasonText.text = error.ErrorMessage;
    }

    private void OnLoginSuccess(LoginResult obj)
    {
        if (obj.NewlyCreated)
        {
            var request = new UpdateUserTitleDisplayNameRequest { DisplayName = "New Player" };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnUserDisplayNameUpdateSuccessfull, OnUserDisplayNameUpdateFail);
        }
        updateUserprofile();
    }

    private void OnUserDisplayNameUpdateFail(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
    }

    private void OnUserDisplayNameUpdateSuccessfull(UpdateUserTitleDisplayNameResult obj)
    {
        updateUserprofile();
    }

    private void onGetAccountInfoFail(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
    }

    private void onGetAccountInfoSuccess(GetAccountInfoResult obj)
    {
        Debug.Log("nae : " + obj.AccountInfo.TitleInfo.DisplayName);

    }

    private void OnEmailUpdateSuccess(AddOrUpdateContactEmailResult obj)
    {
        Debug.Log("Congratulations, Email update Successfull ");
    }

    private void onEmailUpdateFail(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
    }
    public void updateUserprofile()
    {
        var request = new GetAccountInfoRequest { };
        PlayFabClientAPI.GetAccountInfo(request, onGetAccountInfoSuccess, onGetAccountInfoFail);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
