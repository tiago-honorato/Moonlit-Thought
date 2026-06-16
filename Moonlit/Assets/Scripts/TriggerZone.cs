using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    // Cria um campo no inspetor identico aos botoes do UI da Unity
    [SerializeField] private UnityEvent onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se quem entrou foi o jogador (colocar Tag "Player" no player)
        if (other.CompareTag("Player"))
        {
            // Dispara o evento e todo mundo que tiver "ouvindo" vai agir
            onPlayerEnter?.Invoke();
        }
    }
}