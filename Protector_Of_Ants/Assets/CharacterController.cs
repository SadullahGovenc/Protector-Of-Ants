using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Ýleri-geri hareket hýzý
    public float rotationSpeed = 200f;  // Saða-sola dönüþ hýzý
    public float jumpForce = 8f;  // Zýplama gücü
    public float gravity = -9.81f;  // Yerçekimi
    public float climbSpeed = 3f;  // Týrmanma hýzý

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isClimbing = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Eðer CharacterController eksikse hata mesajý verelim
        if (controller == null)
        {
            Debug.LogError("CharacterController bileþeni eksik! Lütfen ekleyin.");
            enabled = false; // Scripti devre dýþý býrak
        }
    }

    void Update()
    {
        if (controller == null) return;

        // Yere temas kontrolü
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // **Ýleri - Geri Hareket (W-S)**
        float moveZ = Input.GetAxis("Vertical");  // W ve S tuþlarý
        Vector3 move = transform.forward * moveZ;

        if (!isClimbing) // Týrmanmýyorsa normal hareket
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        // **Olduðu Yerde Saða - Sola Dönme (A-D)**
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // **Zýplama**
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        // **Aðaca Týrmanma**
        if (isClimbing)
        {
            float climbInput = Input.GetAxis("Vertical"); // W-S ile yukarý/aþaðý
            Vector3 climbMove = new Vector3(0, climbInput * climbSpeed, 0);
            controller.Move(climbMove * Time.deltaTime);
        }
        else
        {
            // Yerçekimi uygula
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // **Aðaca Týrmanma Alaný**
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = false;
        }
    }
}
