using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numberScenePersists = FindObjectsByType<ScenePersist>(FindObjectsInactive.Include).Length; //FindObjectsByType<GameSession>(FindObjectsSortMode.None) is deprecated.
        if (numberScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist()
    {
        DestroyImmediate(gameObject);
        SceneManager.LoadScene(0);
    }
}
