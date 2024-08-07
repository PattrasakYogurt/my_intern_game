using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public static class WebConnection
{
    public static void Login() //for login
    {

        WebConnectionInternal.LoginIc();

    }
    public static void GetNFT()
    {

        WebConnectionInternal.GetNFT();

    }
}

public static class WebConnectionInternal
{
    [DllImport("__Internal")]
    public static extern void LoginIc();
    [DllImport("__Internal")]
    public static extern void GetNFT();
    

    
}

