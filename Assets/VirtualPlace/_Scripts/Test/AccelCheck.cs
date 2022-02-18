using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelCheck : MonoBehaviour
{
    // Move object using accelerometer
    float speed = 100.0f;
    public Text Ts;
    public Transform DummyObj;
    void Update()
    {
        Vector3 dir = Vector3.zero;

        dir.x = -Input.acceleration.y;
        dir.y = 0;//Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
        {
            dir.Normalize();
        }
        dir *= Time.deltaTime;
        if(DummyObj.localPosition.x<20 && DummyObj.localPosition.x > -20)
        {
            DummyObj.Translate(dir * speed);
            Vector3 XDir = transform.localEulerAngles;
            XDir.x = DummyObj.localPosition.x;
            transform.localEulerAngles = XDir;
        }
            
    }
}
