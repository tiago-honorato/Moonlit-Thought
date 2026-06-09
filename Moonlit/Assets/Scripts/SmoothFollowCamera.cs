using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [Header("Alvo para Seguir")]
    [SerializeField] private Transform cameraTarget; // Sua Câmera Principal

    [Header("Configurações de Posição na Mão (Offset)")]
    [Tooltip("O quanto a lanterna vai para a direita (positivo) ou esquerda (negativo).")]
    [SerializeField] private float forwardOffset = 0.2f; // Um pouco para a frente para não cortar a visão
    [SerializeField] private float rightOffset = 0.3f;   // Para a direita
    [SerializeField] private float upOffset = -0.25f;    // Para baixo (valores negativos vão para baixo)

    [Header("Configurações de Atraso (Smooth)")]
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float positionSpeed = 25.0f;

    void LateUpdate()
    {
        if (cameraTarget == null) return;

        // CALCULA A POSIÇÃO DA MÃO BASEADA NA ORIENTAÇÃO DA CÂMERA:
        // Usamos os vetores locais da câmera (forward, right, up) para que o offset 
        // acompanhe para onde o jogador está olhando, mesmo de ponta-cabeça.
        Vector3 targetPosition = cameraTarget.position
                               + (cameraTarget.forward * forwardOffset)
                               + (cameraTarget.right * rightOffset)
                               + (cameraTarget.up * upOffset);

        // 1. Acompanha a posição calculada com interpolação
        transform.position = Vector3.Lerp(transform.position, targetPosition, positionSpeed * Time.deltaTime);

        // 2. Acompanha a rotação da câmera com o atraso elegante
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraTarget.rotation, rotationSpeed * Time.deltaTime);
    }
}