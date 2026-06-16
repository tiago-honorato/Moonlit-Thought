using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("Eventos")]
    [SerializeField] private UnityEvent onInteract;

    // Function chamada pelo laser do Player
    public void Interact()
    {
        onInteract?.Invoke();
    }
}