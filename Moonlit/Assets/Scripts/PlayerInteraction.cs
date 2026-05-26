using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float interactDistance = 3.0f; // Distância mínima (em metros)
    [SerializeField] private LayerMask interactableLayer;   // Camada dos objetos interativos
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    void Update()
    {
        // Cria um raio que parte do centro da câmera para a frente
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Desenha o raio no modo de edição/Scene para você conseguir enxergar o laser (ajuda no teste!)
        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.yellow);

        // Dispara o "laser" de física
        if (Physics.Raycast(ray, out hit, interactDistance, interactableLayer))
        {
            // Se o laser bateu em algo, tentamos pegar o script 'Interactable' dele
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                // Mostra na tela ou console que você está olhando para o item
                // Aqui no futuro você pode fazer um texto "Aperte E para pegar" aparecer na tela

                // Se o jogador apertar a tecla de interação
                if (Input.GetKeyDown(interactKey))
                {
                    interactable.Interact();
                }
            }
        }
    }
}