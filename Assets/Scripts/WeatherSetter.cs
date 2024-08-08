using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSetter : MonoBehaviour
{
    public enum MyEnumeratedType
    {
        SporeWind,Rain
    }
    public MyEnumeratedType activeWeather;
    // Start is called before the first frame update
    void Start()
    {
        
        if (activeWeather == MyEnumeratedType.Rain)
        {
            WeatherHandler.Instance.ActivateRain();
        } else if (activeWeather == MyEnumeratedType.SporeWind)
        {
            WeatherHandler.Instance.ActivateSpore();
        }
    }

}
