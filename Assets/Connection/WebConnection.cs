using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class WebConnection
{
    public static void Login() //for login
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ReacFunctionsInternal.LoginIc();
#endif
    }
    public static void GetNFT()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ReacFunctionsInternal.GetNFT();
#endif
    }
}

public static class ReacFunctionsInternal
{
    [DllImport("__Internal")]
    public static extern void LoginIc();
    [DllImport("__Internal")]
    public static extern void GetNFT();
    

    
}

