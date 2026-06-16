using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    [Header("Configurações de Rotação")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private float duration = 0.5f;

    private bool isRotating = false;
    private bool isOpened = false; //

    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {

        closedRotation = transform.rotation;

        openRotation = closedRotation * Quaternion.Euler(rotationAxis * -90f);
    }

    // Método que o UnityEvent vai chamar
    public void Rotate90Degrees()
    {
        // Ignora a interacao no meio da transicao
        if (!isRotating)
        {

            isOpened = !isOpened;

            Quaternion targetRotation = isOpened ? openRotation : closedRotation;

            StartCoroutine(RotateRoutine(targetRotation));
        }
    }

    private IEnumerator RotateRoutine(Quaternion targetRotation)
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentage = elapsed / duration;

            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, percentage);
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}