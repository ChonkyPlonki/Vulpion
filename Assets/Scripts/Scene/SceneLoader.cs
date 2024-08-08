using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D PlayerSceneLoadBoundary;

    void Start()
    {
       //SceneController.m_staticRef.Setu
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(SceneManager.sceneCount);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        SceneManager.UnloadSceneAsync(1);
        // Destroy(gameObject);
    }
}
