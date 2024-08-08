using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSceneLoad : MonoBehaviour
{
    private float distanceBetweenSceneCenters = 72.5f;
    public static Vector3[,] sceneLocations;
    private string[] letters = { "A", "B", "C", "D", "E", "F","G","H","J","I"};

    private void Start()
    {
        sceneLocations = new Vector3[10,10];
        for (int i = 0; i < 10; i++)
        {
            for (int y = 0; y < 10; y++)
            {
                sceneLocations[i,y] = new Vector3(distanceBetweenSceneCenters * (i+1), distanceBetweenSceneCenters * (i+1), 0);
            }
        }
    }

    private void Update()
    {
        Vector3 playerPos = PlayerRelated.Instance.playerMovingTransform.position;
        int closestCol = (int) (playerPos.x / distanceBetweenSceneCenters);
        int closestRow = (int) (playerPos.y / distanceBetweenSceneCenters);

        string sceneName = letters[closestRow] + closestCol.ToString();

        //Get all loaded scenes
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        bool alreadyLoaded = false;
        foreach (var scene in loadedScenes)
        {
            if (scene.name == sceneName)
            {
                alreadyLoaded = true;
            } 
        }
        if (!alreadyLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }
}
