using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANTE: Necessário para trocar de cenas

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        // Garante que o mouse comece solto e visível no menu inicial
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        // Carrega a cena do jogo pelo nome exato dela
        // Substitua "Game" pelo nome exato da sua cena de gameplay
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}