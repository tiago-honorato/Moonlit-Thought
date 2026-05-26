using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel; // Arraste seu painel de UI aqui
    private bool isPaused = false;

    void Start()
    {
        // Garante que o jogo começe despausado
        Time.timeScale = 1f;
        // Garante que o menu começa fechado e o mouse preso
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);

        // O Manager cuida do mouse
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Opcional: Pausa o tempo do jogo (física, animações, etc.)
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);

        // O Manager esconde o mouse de volta
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Despausa o tempo do jogo
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {

        SceneManager.LoadScene("MainMenu");

    }

}