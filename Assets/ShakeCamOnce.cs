using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamOnce : MonoBehaviour
{
    public GameObject mainCameraHolder;
    public float smallDuration;
    public float smallMagnitude;

    public void SmallSingleCamShake()
    {
        StartCoroutine(Shake(smallDuration, smallMagnitude));
    }

    IEnumerator Shake(float duration, float magnitude)
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = mainCameraHolder.transform.localPosition;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            mainCameraHolder.transform.localPosition = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        mainCameraHolder.transform.localPosition = originalCamPos;
    }
}
