using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBack : MonoBehaviour
{

    public void IsMobileTrue(int i)
    {
        if(i == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("lol");
        }
    }
     public void RequestNFT(string nfts)
    {
        JsonConvert.DeserializeObject<NFT>(nfts);
    }
}
public class NFT
{
    public  string collection;
    public  string nftName;
    
}
