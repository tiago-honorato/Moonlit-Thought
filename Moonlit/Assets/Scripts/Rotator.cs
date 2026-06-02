using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    [Header("Configurações de Rotação")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up; // Gira no eixo Y (como uma porta)
    [SerializeField] private float duration = 0.5f;             // Tempo que o giro demora (em segundos)

    private bool isRotating = false;
    private bool isOpened = false; // Controla se a porta está aberta ou fechada

    private Quaternion closedRotation; // Guarda a rotação inicial (fechada)
    private Quaternion openRotation;   // Guarda a rotação calculada (aberta)

    void Start()
    {
        // Salva a rotação exata de como a porta começou na cena (fechada)
        closedRotation = transform.rotation;

        // Calcula de antemão a rotação dela aberta (apenas +90 graus da inicial)
        openRotation = closedRotation * Quaternion.Euler(rotationAxis * -90f);
    }

    // Método público que o Unity Event vai chamar
    public void Rotate90Degrees()
    {
        // Se já estiver no meio de uma transição, ignora o clique
        if (!isRotating)
        {
            // Inverte o estado: se estava fechada (false), vai abrir (true) e vice-versa
            isOpened = !isOpened;

            // Escolhe o destino correto baseado no estado atual
            Quaternion targetRotation = isOpened ? openRotation : closedRotation;

            StartCoroutine(RotateRoutine(targetRotation));
        }
    }

    private IEnumerator RotateRoutine(Quaternion targetRotation)
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;

        // Faz o giro acontecer suavemente até o destino escolhido
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentage = elapsed / duration;

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, percentage);
            yield return null;
        }

        // Garante que terminou exatamente no ângulo correto
        transform.rotation = targetRotation;
        isRotating = false;
    }
}