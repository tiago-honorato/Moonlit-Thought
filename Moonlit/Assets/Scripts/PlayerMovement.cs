using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Câmera e Olhar")]
    [SerializeField] Transform playerCamera;
    [SerializeField] float mouseSensitivity = 1.5f;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.02f;
    [SerializeField] bool cursorLock = true;

    [Header("Movimentação Base")]
    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField][Range(0.0f, 0.5f)] float groundSmoothTime = 0.02f; // Resposta rápida no chão

    [Header("Inércia no Ar")]
    [SerializeField][Range(0.0f, 1.0f)] float airSmoothTime = 0.35f;    // Quanto maior, mais ele "desliza" no ar

    [Header("Física do Pulo Variável")]
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3.0f;   // Altura máxima do pulo (segurando o botão)
    [SerializeField] float lowJumpMultiplier = 3.0f;    // Gravidade extra aplicada se você SOLTAR o botão cedo
    [SerializeField] float fallMultiplier = 2.0f;   // Gravidade ao cair (deixa a queda firme e natural)

    [Header("Verificação de Chão")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    // Variáveis de Controle Interno
    CharacterController controller;
    bool isGrounded;
    float velocityY;
    float cameraCap;

    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    Vector2 currentDir;
    Vector2 currentDirVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
        }
    }

    void Update()
    {

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            UpdateMouse();
        }

        UpdateMove();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Mantém a suavidade original que você gostava para a câmera
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        // 1. Verificar se está no chão
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        // 2. Capturar Input de Movimento
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        // FUNCIONALIDADE 2: Inércia Diferenciada (Chão vs Ar)
        // Se estiver no chão, usa resposta instantânea. No ar, o SmoothDamp demora para zerar o input (Gera Inércia).
        float currentSmoothTime = isGrounded ? groundSmoothTime : airSmoothTime;
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, currentSmoothTime);

        // 3. Sistema de Gravidade Dinâmica e Pulo Variável (FUNCIONALIDADE 1)
        if (velocityY > 0) // Jogador está SUBINDO no pulo
        {
            // Se ele SOLTAR o botão de pulo antes do topo, aplicamos uma gravidade muito maior (Corta o pulo)
            if (!Input.GetButton("Jump"))
            {
                velocityY += gravity * lowJumpMultiplier * Time.deltaTime;
            }
            else
            {
                // Se continuar segurando, aplica a gravidade normal do pulo alto
                velocityY += gravity * Time.deltaTime;
            }
        }
        else // Jogador está CAINDO
        {
            // Aplica a gravidade de queda (mais firme, sem trancos abruptos)
            velocityY += gravity * fallMultiplier * Time.deltaTime;
        }

        // Estabiliza a gravidade no chão
        if (isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        // Executa o Pulo Inicial
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Fórmula física exata para alcançar a altura desejada
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Aplicar o Movimento Final
        Vector3 moveVelocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY;
        controller.Move(moveVelocity * Time.deltaTime);
    }
}