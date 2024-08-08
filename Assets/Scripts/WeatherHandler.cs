using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WeatherHandler : MonoBehaviour
{

    private static WeatherHandler _instance;

    public static WeatherHandler Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public Light2D skyLight;
    public GameObject rainWeather;
    public Color rainSky;
    public float rainSkyIntsty;
    public GameObject rainSound;
    public GameObject sporeWeather;
    public Color sporeSky;
    public float sporeSkyIntsty;

    public void ActivateRain()
    {
        //sporeWeather.SetActive(false);
        ResetAllWeather();
        Footsteps.Instance.isRaining = true;
        rainSound.SetActive(true);
        rainWeather.SetActive(true);
        skyLight.color = rainSky;
        skyLight.intensity = rainSkyIntsty;
    }

    public void ActivateSpore()
    {
        ResetAllWeather();
        //rainSound.SetActive(false);
        //rainWeather.SetActive(false);
        sporeWeather.SetActive(true);
        skyLight.color = sporeSky;
        skyLight.intensity = sporeSkyIntsty;
    }

    private void ResetAllWeather()
    {
        Footsteps.Instance.isRaining = false;
        skyLight.color = new Color(1, 1, 1, 1);
        skyLight.intensity = 1;
        rainSound.SetActive(false);
        rainWeather.SetActive(false);
        sporeWeather.SetActive(false);
    }

}
