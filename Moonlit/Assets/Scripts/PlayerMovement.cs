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
    [SerializeField][Range(0.0f, 0.5f)] float groundSmoothTime = 0.02f; // Resposta rapida no chao

    [Header("Inércia no Ar")]
    [SerializeField][Range(0.0f, 1.0f)] float airSmoothTime = 0.35f;    // Quanto maior, mais ele desliza no ar

    [Header("Física do Pulo Variável")]
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3.0f;   // Altura maxima do pulo
    [SerializeField] float lowJumpMultiplier = 3.0f;    // Gravidade extra aplicada se SOLTAR o botão cedo
    [SerializeField] float fallMultiplier = 2.0f;   // Gravidade ao cair

    [Header("Verificação de Chão")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

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

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        // Verifica se ta no chao
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        // Capturar Input de Movimento
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        float currentSmoothTime = isGrounded ? groundSmoothTime : airSmoothTime;
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, currentSmoothTime);

        if (velocityY > 0)
        {

            if (!Input.GetButton("Jump"))
            {
                velocityY += gravity * lowJumpMultiplier * Time.deltaTime;
            }
            else
            {

                velocityY += gravity * Time.deltaTime;
            }
        }
        else
        {

            velocityY += gravity * fallMultiplier * Time.deltaTime;
        }

        if (isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {

            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Vector3 moveVelocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * moveSpeed + Vector3.up * velocityY;
        controller.Move(moveVelocity * Time.deltaTime);
    }
}