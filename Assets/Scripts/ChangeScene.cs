using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas


public class MultiSceneChanger : MonoBehaviour
{
    [SerializeField] private string[] sceneNames;

    void Start()
    {
        // Si no hay datos de índice previos, inicializamos en 0
        if (SceneData.CurrentSceneIndex == -1)
        {
            SceneData.CurrentSceneIndex = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeToNextScene();
        }
    }

    void ChangeToNextScene()
    {
        SceneData.CurrentSceneIndex++;

        if (SceneData.CurrentSceneIndex >= sceneNames.Length)
        {
            SceneData.CurrentSceneIndex = 0;
        }

        SceneManager.LoadScene(sceneNames[SceneData.CurrentSceneIndex]);
    }
}

// Clase estática para almacenar datos persistentes
public static class SceneData
{
    public static int CurrentSceneIndex = -1;
}

