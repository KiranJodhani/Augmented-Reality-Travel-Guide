using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManagerv2 : MonoBehaviour 
{
    public Text[] DistaneLables;
    public List<LocationDetails> LocationDetailsList = new List<LocationDetails>();
    public double MinDistance;

    [Header("****** EDITOR ONLY ********")]
    public double EditorLat;
    public double EditorLng;
    public GameObject CameraObj;


    [Header("****** DEVELOPER ONLY ********")]
    public Text CamPosx;
    public Text CamPosy;
    public Text CamPosz;

    public Text CamRotx;
    public Text CamRoty;
    public Text CamRotz;

    public Text PinPosx;
    public Text PinPosy;
    public Text PinPosz;

    public GameObject TestPin;

    void Start () 
    {
#if UNITY_EDITOR
        CalculateDistanceFromEachLocation();
        Vector2 _Location = new Vector2((float)EditorLat, (float)EditorLng);
        GPSEncoder.SetLocalOrigin(_Location);
#endif

#if !UNITY_EDITOR
        StartCoroutine(GetLocation());
#endif


    }

    public bool IsLocationFound;

    IEnumerator GetLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            CalculateDistanceFromEachLocation();
        }
    }

    double FinalLat;
    double FinalLong;

    void CalculateDistanceFromEachLocation()
    {
        for (int i = 0; i < LocationDetailsList.Count;i++)
        {
            Vector2 _Location = new Vector2((float)FinalLat, (float)FinalLong);
            GPSEncoder.SetLocalOrigin(_Location);

            double Dis = DistanceTo(FinalLat, FinalLong , LocationDetailsList[i].Lat, LocationDetailsList[i].Lng);
            LocationDetailsList[i].Distance = (float)Math.Round((double)Dis * 100) / 100f;
            Vector3 Pos = GPSEncoder.GPSToUCS((float)LocationDetailsList[i].Lat, (float)LocationDetailsList[i].Lng);
            LocationDetailsList[i].LocationContent.transform.localPosition = Pos;

            //Vector3 PosPlayer = GPSEncoder.GPSToUCS((float)FinalLat, (float)FinalLong);
            //CameraObj.transform.localPosition = PosPlayer;
            if (Dis < MinDistance)
            {
                LocationDetailsList[i].LocationContent.SetActive(true);
            }
            else
            {
                LocationDetailsList[i].LocationContent.SetActive(false);
            }
        }
        LocationDetailsList.Sort((p1, p2) => p1.Distance.CompareTo(p2.Distance));
        for (int i = 0; i < LocationDetailsList.Count;i++)
        {
            DistaneLables[i].text = LocationDetailsList[i].LocationName + ": " 
                + LocationDetailsList[i].Distance.ToString("f2") + " KM";

            if (LocationDetailsList[i].Distance < 0.02f)
            {
                if(LocationDetailsList[i].LocationContent.activeSelf)
                {
                    LocationDetailsList[i].LocationContent.GetComponent<MarkerInstance>().CanLookAt = false;
                }
            }
            else
            {
                if (LocationDetailsList[i].LocationContent.activeSelf)
                {
                    LocationDetailsList[i].LocationContent.GetComponent<MarkerInstance>().CanLookAt = true;
                }
            }
        }
        Invoke("CalculateDistanceFromEachLocation", 0.1f);
    }

    public void RotateRight()
    {
        CameraObj.transform.Rotate(Vector3.down * Time.deltaTime * 10);

    }
    private void Update()
    {
         #if UNITY_EDITOR
             FinalLat = EditorLat;
             FinalLong = EditorLng;
        #endif

        #if !UNITY_EDITOR
              FinalLat = Input.location.lastData.latitude;
              FinalLong = Input.location.lastData.longitude;
        #endif

        CamPosx.text = CameraObj.transform.localPosition.x.ToString();
        CamPosy.text = CameraObj.transform.localPosition.y.ToString();
        CamPosz.text = CameraObj.transform.localPosition.z.ToString();

        CamRotx.text = CameraObj.transform.localEulerAngles.x.ToString();
        CamRoty.text = CameraObj.transform.localEulerAngles.y.ToString();
        CamRotz.text = CameraObj.transform.localEulerAngles.z.ToString();

        PinPosx.text = TestPin.transform.localPosition.x.ToString();
        PinPosy.text = TestPin.transform.localPosition.y.ToString();
        PinPosz.text = TestPin.transform.localPosition.z.ToString();

    }


    public void RotateLeft()
    {
        CameraObj.transform.Rotate(Vector3.up * Time.deltaTime * 10);
    }
    public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
    {
        double rlat1 = Math.PI * lat1 / 180;
        double rlat2 = Math.PI * lat2 / 180;
        double theta = lon1 - lon2;
        double rtheta = Math.PI * theta / 180;
        double dist =
        Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
        Math.Cos(rlat2) * Math.Cos(rtheta);
        dist = Math.Acos(dist);
        dist = dist * 180 / Math.PI;
        dist = dist * 60 * 1.1515;

        switch (unit)
        {
            case 'K': //Kilometers -> default
                return dist * 1.609344;
            case 'N': //Nautical Miles 
                return dist * 0.8684;
            case 'M': //Miles
                return dist;
        }

        return dist;
    }
}

[System.Serializable]
public class LocationDetails
{
    public string LocationName;
    public GameObject LocationContent;
    public double Lat;
    public double Lng;
    public double Distance;
}


/*

Ponte 25 de Abril
38.6897538
-9.1780009

SBI

23.0754304,72.5054153

Galaxy
23.0758014
72.506413

 */


/*
Galexy : 23.0772941,72.5047733,17z
dia/@23.07608,72.5070156,17z


GD+Taneja+%26+Co./@23.0766282,72.5072354,21z/da

y+Signature/@23.0771871,72.5068324,21z/dat

Cross Road /Galaxy+Signature/@23.0768304,72.5068188,21z/d

38.7720615,-9.3729467,2

/West+Point+Honda/@23.0767885,72.5071914,21z/data=!


/GD+Taneja+%26+Co./@23.0765145,72.5073206,21z/data=

Quinta da Beloura @ 38.7721324,-9.373146,21z/dat

*/
