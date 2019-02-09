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
        Debug.Log("ramesh");
        if (string.IsNullOrEmpty(userprofile.email) || string.IsNullOrEmpty(userprofile.password))
        {
            ShowFreshStartConfirmationWindow();
        }
        else
        {
            LoginWithEmail(userprofile.email, userprofile.password);
        }

    }

    private void ShowFreshStartConfirmationWindow()
    {
        freshstartconfiramtionwindow.SetActive(true);
    }
    public void LoginWithDeviceID()
    {
#if UNITY_EDITOR

        var request = new LoginWithCustomIDRequest { CustomId = "ramesh", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#elif UNITY_ANDROID
        
#endif
    }

    public void LoginWithEmail(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest { Email = email, Password = password };
        PlayFabClientAPI.LoginWithEmailAddress(request,
            (LoginResult obj) =>
            {
                userprofile.email = email;
                userprofile.password = password;
                userprofile.playfabid = obj.PlayFabId;
                if (obj.NewlyCreated)
                {

                    UpdateDisplaName();
                }
            },
            (PlayFabError error) =>
            {
                emailform.GetComponent<EmailForm>().OnEmailAuthenticationFailed(error.ErrorMessage);
            });
    }
    public void UpdateDisplaName()
    {
        var request = new UpdateUserTitleDisplayNameRequest { DisplayName = "New Player" };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            (UpdateUserTitleDisplayNameResult obj) =>
            {

            },
            (PlayFabError error) =>
            {
                emailform.GetComponent<EmailForm>().OnEmailAuthenticationFailed(error.ErrorMessage);
            });
    }



    private void onGetAccountInfoFail(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
    }

    private void onGetAccountInfoSuccess(GetAccountInfoResult obj)
    {

        userprofile.displayname = obj.AccountInfo.TitleInfo.DisplayName;



    }

    private void GetPlayerCombinedInfoRequestFail(PlayFabError error)
    {
        Debug.LogWarning(error.GenerateErrorReport());
    }

    private void GetPlayerCombinedInfoRequestSuccess(GetPlayerCombinedInfoResult obj)
    {

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
        var infoRequestParameters = new GetPlayerCombinedInfoRequestParams();
        infoRequestParameters.GetPlayerProfile = true;
        infoRequestParameters.GetTitleData = true;
        infoRequestParameters.GetUserAccountInfo = true;
        infoRequestParameters.GetUserData = true;
        infoRequestParameters.ProfileConstraints = new PlayerProfileViewConstraints { ShowDisplayName = true, ShowContactEmailAddresses = true };
        var request = new GetPlayerCombinedInfoRequest { InfoRequestParameters = infoRequestParameters };
        PlayFabClientAPI.GetPlayerCombinedInfo(request, GetPlayerCombinedInfoRequestSuccess, GetPlayerCombinedInfoRequestFail);
        /*var request = new GetAccountInfoRequest { };
        PlayFabClientAPI.GetAccountInfo(request, onGetAccountInfoSuccess, onGetAccountInfoFail);*/
    }

    // Update is called once per frame
    void Update()
    {

    }
}
