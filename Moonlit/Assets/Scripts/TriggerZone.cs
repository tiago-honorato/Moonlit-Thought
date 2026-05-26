using UnityEngine;
using UnityEngine.Events; // IMPORTANTE: Necessário para usar UnityEvents

public class TriggerZone : MonoBehaviour
{
    // Isso vai criar um campo no seu Inspetor idêntico aos botões do UI do Unity
    [SerializeField] private UnityEvent onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se quem entrou foi o jogador (certifique-se de colocar a Tag "Player" no seu boneco)
        if (other.CompareTag("Player"))
        {
            // Dispara o evento! Todo mundo que estiver "ouvindo" vai agir.
            onPlayerEnter?.Invoke();
        }
    }
}