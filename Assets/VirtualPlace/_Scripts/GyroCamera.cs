using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GyroCamera : MonoBehaviour
{
    private Gyroscope gyro;
    private bool gyroSupported;
    private Quaternion rotFix;
    float RotationSpeed=10;
    void Start()
    {

        #if UNITY_ANDROID
        gyroSupported = SystemInfo.supportsGyroscope;
        GameObject camParent = new GameObject("camParent");
        camParent.transform.position = transform.position;
        transform.parent = camParent.transform;


        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            camParent.transform.rotation = Quaternion.Euler(90f, 180f, 0f);
            rotFix = new Quaternion(0, 0, 1, 0);
        }
        #endif

        #if UNITY_IOS
        Input.compass.enabled = true;
#endif

    }

	void Update () 
    {
        #if UNITY_IOS
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0,Input.compass.magneticHeading, 0),
                                                               Time.deltaTime * 2);
#endif

#if UNITY_ANDROID
            if (gyroSupported)
            {
            transform.localRotation = gyro.attitude * rotFix;
            }
#endif

    }
}
