using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOnTrigger : MonoBehaviour {

    private float fadeTime = 2f;
    private Color defaultColor;
    private Color fadedColor;
    public SpriteRenderer myRenderer;
    public float speed = 1.0f;
    private float fadeLevel = 0.4f;
    private Color currentFadeLvlIn;
    private Color currentFadeLvlOut;

    // Use this for initialization
    void Start () {
        defaultColor = myRenderer.color;
        defaultColor = new Color(1,1,1,1);
        fadedColor = defaultColor;
        currentFadeLvlIn = defaultColor;
        currentFadeLvlOut = defaultColor;
        fadedColor.a = fadeLevel;
        currentFadeLvlOut.a = fadeLevel;     
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            StartCoroutine(FadeOut());
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(FadeIn());
        }
            
    }

    IEnumerator FadeOut()
    {
        for (float t = 0.01f; t < fadeTime; t += 0.1f)
        {
            myRenderer.color = Color.Lerp(currentFadeLvlIn, fadedColor, t / fadeTime);
            currentFadeLvlOut.a = myRenderer.color.a;

            yield return null;
        }
    }

    IEnumerator FadeIn()
    {
        for (float t = 0.01f; t < fadeTime; t += 0.1f)
        {
            myRenderer.color = Color.Lerp(currentFadeLvlOut, defaultColor, t / fadeTime);
            if (t/fadeTime >= 0.95)
            {
                myRenderer.color = defaultColor;
            }
            currentFadeLvlIn.a = myRenderer.color.a;

            yield return null;
        }
    }

}
