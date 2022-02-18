using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassManager : MonoBehaviour {

    void Start()
    {
        Input.compass.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, 
                                              Quaternion.Euler(0, Input.compass.magneticHeading, 0), 
                                              Time.deltaTime * 2);
    }
}
