using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeGrassWhenWalkOn : MonoBehaviour
{
    private Material ourMaterial;
    private Renderer ourRenderer;

    private float speedBefore;
    private float amplitudeBefore;
    private float freqBefore;

    private float speedLater;
    private float amplitudeLater;
    private float freqLater;

    private float randFreq;
    private float randAmp;

    private bool inFirst = false;
    private bool coroutingIsGoing = false;

    private AudioSource aud;

    // Start is called before the first frame update

    private void Awake()
    {
        ourRenderer = gameObject.GetComponent<Renderer>();
        speedBefore = ourRenderer.material.GetFloat("Vector1_F093834E");
        amplitudeBefore = ourRenderer.material.GetFloat("Vector1_C9BD77E0");
        freqBefore = ourRenderer.material.GetFloat("Vector1_39F451B1");

        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!aud.isPlaying)
            {
                AudioMasterHandler.Instance.playSoundEffect(aud);
                //aud.Play();
            }

            if (!coroutingIsGoing)
            {
                coroutingIsGoing = true;
                ShakePlant();
            }
        }    
    }

    private void ShakePlant()
    {
        StartPage();      
    }

    

    public void StartPage()
    {
        
        //AudioSource.PlayClipAtPoint((UnityEngine.AudioClip) Resources.Load("Sound/Sporegrass1"),transform.position, 0.2f);
        StartCoroutine(FinishFirst(1.5f));
        StartCoroutine(DoLast());
    }

    IEnumerator FinishFirst(float waitTime)
    {
        inFirst = true;

        float time0 = 0.5f;
        float elapsedTime0 = 0;

        randAmp = Random.Range(0.5f, 0.75f);
        randFreq = Random.Range(0.2f, 0.3f);

        while (elapsedTime0 < time0)
        {
            //Speed
            //ourRenderer.material.SetFloat("Vector1_F093834E", Random.Range(3f, 5f));
            //Amp
            ourRenderer.material.SetFloat("Vector1_C9BD77E0", Mathf.Lerp(amplitudeBefore, randAmp, elapsedTime0/time0));
            //Freq
            ourRenderer.material.SetFloat("Vector1_39F451B1", Mathf.Lerp(freqBefore, randFreq, elapsedTime0 / time0));
            elapsedTime0 += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        ourRenderer.material.SetFloat("Vector1_C9BD77E0", randAmp);
        ourRenderer.material.SetFloat("Vector1_39F451B1",randFreq);


        speedLater = ourRenderer.material.GetFloat("Vector1_F093834E");
        amplitudeLater = ourRenderer.material.GetFloat("Vector1_C9BD77E0");
        freqLater = ourRenderer.material.GetFloat("Vector1_39F451B1");
        yield return new WaitForSeconds(waitTime);
        inFirst = false;
    }

    IEnumerator DoLast()
    {

        while (inFirst)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        float time = 4f;
        float elapsedTime = 0;
        ourRenderer.material.SetFloat("Vector1_F093834E", speedBefore);

        while (elapsedTime < time)
        {
            //ourRenderer.material.SetFloat("Vector1_F093834E", Mathf.Lerp(speedLater, speedBefore, elapsedTime / time));
            ourRenderer.material.SetFloat("Vector1_C9BD77E0", Mathf.Lerp(amplitudeLater, amplitudeBefore, elapsedTime/time));
            ourRenderer.material.SetFloat("Vector1_39F451B1", Mathf.Lerp(freqLater, freqBefore, elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        ourRenderer.material.SetFloat("Vector1_39F451B1", freqBefore);
        //ourRenderer.material.SetFloat("Vector1_F093834E", speedBefore);
        ourRenderer.material.SetFloat("Vector1_C9BD77E0", amplitudeBefore);

        coroutingIsGoing = false;
    }
}
