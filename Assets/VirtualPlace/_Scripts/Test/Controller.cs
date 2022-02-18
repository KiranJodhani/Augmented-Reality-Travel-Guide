using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour 
{

    public float smooth = 0.4f;
    //public Text inputValue;
    public float newRotation;
    public float sensitivity = 6;
    private Vector3 currentAcceleration, initialAcceleration;
    void Start()
    {
        initialAcceleration = Input.acceleration;
        currentAcceleration = Vector3.zero;
    }

    void Update()
    {
        //pre-processing

        currentAcceleration = Vector3.Lerp(currentAcceleration, Input.acceleration - initialAcceleration, 
                                           Time.deltaTime / smooth);

        newRotation = Mathf.Clamp(currentAcceleration.y * sensitivity, -1, 1);
        transform.Rotate(-newRotation,0, 0);
    }


}
