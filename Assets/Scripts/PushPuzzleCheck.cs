using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPuzzleCheck : MonoBehaviour
{
    public GameObject puzzle;
    
    // Start is called before the first frame update
    void Start()
    {
        puzzle.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ObjectForPush"))
        {
            puzzle.SetActive(false);
        }
    }
}
