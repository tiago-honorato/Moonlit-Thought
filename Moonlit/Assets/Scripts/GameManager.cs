using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Start()
    {

        if(SceneManager.GetActiveScene().name == "GameScene")
        {

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }

    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}