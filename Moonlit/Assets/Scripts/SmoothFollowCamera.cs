using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    [Header("Alvo para Seguir")]
    [SerializeField] private Transform cameraTarget;

    [Header("Configurações de Posição na Mão (Offset)")]
    [Tooltip("O quanto a lanterna vai para a direita (positivo) ou esquerda (negativo).")]
    [SerializeField] private float forwardOffset = 0.2f;
    [SerializeField] private float rightOffset = 0.3f;
    [SerializeField] private float upOffset = -0.25f;

    [Header("Configurações de Atraso (Smooth)")]
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float positionSpeed = 25.0f;

    void LateUpdate()
    {
        if (cameraTarget == null) return;

        Vector3 targetPosition = cameraTarget.position
                               + (cameraTarget.forward * forwardOffset)
                               + (cameraTarget.right * rightOffset)
                               + (cameraTarget.up * upOffset);

        transform.position = Vector3.Lerp(transform.position, targetPosition, positionSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, cameraTarget.rotation, rotationSpeed * Time.deltaTime);
    }
}