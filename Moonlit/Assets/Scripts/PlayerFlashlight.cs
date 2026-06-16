using UnityEngine;

public class PlayerFlashlight : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private GameObject flashlightLightSource;

    private bool hasFlashlight = false;
    private bool isOn = false;

    void Start()
    {

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