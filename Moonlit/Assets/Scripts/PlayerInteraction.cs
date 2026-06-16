using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float interactDistance = 3.0f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    [SerializeField] private GameObject interactionPrompt;

    void Start()
    {

        if(interactionPrompt != null)
        {

            interactionPrompt.gameObject.SetActive(false);

        }

    }

    void Update()
    {
        // Cria um raio que parte do centro da camera pra frente
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Desenha o raio no modo de edicao/Scene pra conseguir ver o laser
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.yellow);

        bool IsLooking = false;

        // Dispara o "laser" de fisica
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {

            IsLooking = true;

            // Se o laser bateu em algo, pega o script "Interactable" dele
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {

                if (Input.GetKeyDown(interactKey))
                {

                    interactable.Interact();
                }
            }
        }

        if (interactionPrompt != null)
        {
            // Mostra o "aperte E para interagir"
            interactionPrompt.gameObject.SetActive(IsLooking);
        }
    }
}