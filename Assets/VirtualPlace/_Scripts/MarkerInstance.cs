using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerInstance : MonoBehaviour
{
    public GameObject PlayerCam;
    // Use this for initialization
    public GameObject MarkerInfo;
    public bool CanLookAt;
    void Start () 
    {
		
	}


    private void OnMouseUp()
    {
        MarkerInfo.SetActive(!MarkerInfo.activeSelf);
    }

    private void Update()
    {
        //transform.LookAt(PlayerCam.transform);
        if (CanLookAt)
        {
            transform.LookAt(PlayerCam.transform);
        }
    }
}
