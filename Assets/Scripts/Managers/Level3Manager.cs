using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            SwitchScene("A");
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            SwitchScene("S");
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            SwitchScene("D");
        }
        else if (Input.GetKeyUp(KeyCode.T))
        {
            SwitchScene("Test");
        }
    }

    private void SwitchScene(string name)
    {
        Scene scene = SceneManager.GetSceneByName(name);
        if (scene != null && scene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(scene);
        }
        else
        {
            StartCoroutine(LoadSceneAsync(name));
        }
    }

    private IEnumerator LoadSceneAsync(string name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            Debug.Log($"scene {name} : "+operation.progress);
            yield return null;
        }
        Debug.Log($"scene {name} loaded");
    }
}
