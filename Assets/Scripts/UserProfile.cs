using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RP/UserProfile")]
public class UserProfile : ScriptableObject
{
    public string email;
    public string displayname;
    public string password;
    public string playfabid;

}
