using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTest : MonoBehaviour
{
    private List<string> allScenesActive = new List<string>();  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SceneLoadTrigger")
        {
            LoadScene(this.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SceneLoadTrigger")
        {
            UnloadScene(this.name);
        }
    }

    private void LoadScene(string sceneToLoad)
    {
        bool alreadyLoaded = false;
        foreach (var scene in allScenesActive)
        {
            if (scene == sceneToLoad)
                alreadyLoaded = true;
        }

        if (!alreadyLoaded)
        {
            SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            allScenesActive.Add(sceneToLoad);
        }
    }

    private void UnloadScene(string sceneToLoad)
    {
        SceneManager.UnloadSceneAsync(sceneToLoad);
        allScenesActive.Remove(sceneToLoad);
    }
}
