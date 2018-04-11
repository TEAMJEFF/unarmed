using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toCredits : MonoBehaviour
{
    public ScreenFader fdr;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("In script");
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "Finish")
        {
            fdr.EndScene(3);
        }
    }
}