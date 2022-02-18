using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExampleScript : MonoBehaviour 
{
    // Faces for 6 sides of the cube
    private GameObject[] quads = new GameObject[6];

    // Textures for each quad, should be +X, +Y etc
    // with appropriate colors, red, green, blue, etc
    //public Texture[] labels;

    private Gyroscope gyro;
    private bool gyroSupported;

    public Text X;
    public Text Y;
    public Text Z;
    public Text W;
    void Start()
    {
        gyroSupported = SystemInfo.supportsGyroscope;

        if (gyroSupported)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
        }

       
        GetComponent<Camera>().transform.position = new Vector3(0, 0, 0);

    }

    // make a quad for one side of the cube
    //GameObject createQuad(GameObject quad, Vector3 pos, Vector3 rot, string name, Color col, Texture t)
    //{
    //    Quaternion quat = Quaternion.Euler(rot);
    //    GameObject GO = Instantiate(quad, pos, quat);
    //    GO.name = name;
    //    GO.GetComponent<Renderer>().material.color = col;
    //    GO.GetComponent<Renderer>().material.mainTexture = t;
    //    GO.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
    //    return GO;
    //}

    protected void Update()
    {
        GyroModifyCamera();
        X.text = transform.rotation.x.ToString();
        Y.text = transform.rotation.y.ToString();
        Z.text = transform.rotation.z.ToString();
        W.text = transform.rotation.w.ToString();
    }

    protected void OnGUI()
    {
        GUI.skin.label.fontSize = Screen.width / 40;

        GUILayout.Label("Orientation: " + Screen.orientation);
        GUILayout.Label("input.gyro.attitude: " + Input.gyro.attitude);
        GUILayout.Label("iphone width/font: " + Screen.width + " : " + GUI.skin.label.fontSize);
    }

    /********************************************/

    // The Gyroscope is right-handed.  Unity is left handed.
    // Make the necessary change to the camera.
    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        //return new Quaternion(q.x, q.y, -q.z, -q.w);
        return new Quaternion(q.x, 0, 0,0);
    }
}
