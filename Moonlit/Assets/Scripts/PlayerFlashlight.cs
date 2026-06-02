using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private GameObject flashlightLightSource; // Arraste a sua Spot Light aqui

    private bool hasFlashlight = false; // Começa o jogo sem a lanterna
    private bool isOn = false;          // Começa desligada

    void Start()
    {
        // Garante que a luz começa apagada no início do jogo
        if (flashlightLightSource != null)
        {
            flashlightLightSource.SetActive(false);
        }
    }

    void Update()
    {
        // Só permite ligar/desligar se o jogador já tiver COLETADO a lanterna
        if (hasFlashlight && Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    // Método público que o Unity Event do item vai chamar ao interagir
    public void CollectFlashlight()
    {
        hasFlashlight = true;
        Debug.Log("Lanterna coletada! Agora você pode apertar F.");

        // Opcional: Liga a lanterna automaticamente ao pegar
        ToggleFlashlight();
    }

    private void ToggleFlashlight()
    {
        if (flashlightLightSource != null)
        {
            isOn = !isOn;
            flashlightLightSource.SetActive(isOn);
        }
    }
}