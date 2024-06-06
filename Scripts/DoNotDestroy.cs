using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    private static List<GameObject> dontDestroyObjects = new List<GameObject>();

    private void Awake()
    {
        foreach (var item in dontDestroyObjects)
        {
            Debug.LogError(item.GetInstanceID());
        }
        if (!(dontDestroyObjects.Contains(gameObject)) && dontDestroyObjects.Count < 2)
        {
            DontDestroyOnLoad(gameObject);
            dontDestroyObjects.Add(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject battleMusic = dontDestroyObjects[1];
        GameObject music = dontDestroyObjects[0];
        
        if (scene.name == "Arena")
        {
            if (music != null)
            {
                music.SetActive(false);
            }
            if (battleMusic != null)
            {
                battleMusic.SetActive(true);
            }
        }
        else 
        {
            if (music != null)
            {
                music.SetActive(true);
            }
            if (battleMusic != null)
            {
                battleMusic.SetActive(false);
            }
        }
    }
}
