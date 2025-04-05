/*using UnityEngine;

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
*/
/*
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;  // Normal yürüme hýzý
    public float runSpeed = 9f;   // Shift basýnca hýzlý koþma hýzý
    public float slowSpeed = 2f;  // Ctrl basýnca yavaþ yürüme hýzý
    public float rotationSpeed = 200f;  // Saða-sola dönüþ hýzý
    public float jumpForce = 8f;  // Zýplama gücü
    public float gravity = -9.81f;  // Yerçekimi
    public float climbSpeed = 3f;  // Týrmanma hýzý

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isClimbing = false;
    private Transform climbableObject; // Týrmanýlabilir objeyi sakla

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController bileþeni eksik! Lütfen ekleyin.");
            enabled = false;
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

        // ?? Hýz belirleme (Shift = Hýzlý, Ctrl = Yavaþ, Normal = Yürüme)
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed; // Shift basýnca hýzlý koþ
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = slowSpeed; // Ctrl basýnca yavaþ yürü
        }

        // **Ýleri - Geri Hareket (W-S)**
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveZ;

        if (!isClimbing) // Týrmanmýyorsa normal hareket
        {
            controller.Move(move * currentSpeed * Time.deltaTime);
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

        // **Eðer týrmanma aktif ama çok uzaklaþtýysak çýk**
        if (isClimbing && climbableObject != null && Vector3.Distance(transform.position, climbableObject.position) > 3f)
        {
            Debug.Log("Týrmanma alanýndan uzaklaþtýn, artýk týrmanamazsýn! ");
            isClimbing = false;
        }
    }

    // **Týrmanmayý Aç**
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("Týrmanma alanýna girdin! ");
            isClimbing = true;
            climbableObject = other.transform;
        }
    }

    // **Týrmanmayý Kapat**
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("Týrmanma alanýndan çýktýn! ");
            isClimbing = false;
        }
    }
}*/
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;  // Normal yürüme hýzý
    public float runSpeed = 9f;   // Shift basýnca hýzlý koþma hýzý
    public float slowSpeed = 2f;  // Ctrl basýnca yavaþ yürüme hýzý
    public float rotationSpeed = 200f;  // Saða-sola dönüþ hýzý
    public float jumpForce = 8f;  // Zýplama gücü
    public float gravity = -20f;  // Yerçekimi (Düþme hýzýný artýrdým)
    public float climbSpeed = 3f;  // Týrmanma hýzý
    public float fallMultiplier = 2f; // Düþme hýzýný artýrýcý çarpan

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isClimbing = false;
    private Transform climbableObject;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController bileþeni eksik! Lütfen ekleyin.");
            enabled = false;
        }
    }

    void Update()
    {
        if (controller == null) return;

        // **Týrmanma Kontrolü**
        if (isClimbing)
        {
            Climb();
        }
        else
        {
            Move();
        }

        // **Eðer týrmanmayý býraktýysa ve havadaysa düþüþü hýzlandýr**
        if (!isGrounded && !isClimbing)
        {
            velocity.y += gravity * fallMultiplier * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // **Normal Hareket Fonksiyonu**
    void Move()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // ?? Hýz belirleme (Shift = Hýzlý, Ctrl = Yavaþ, Normal = Yürüme)
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed; // Shift basýnca hýzlý koþ
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = slowSpeed; // Ctrl basýnca yavaþ yürü
        }

        // **Ýleri - Geri Hareket (W-S)**
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveZ * currentSpeed;

        controller.Move(move * Time.deltaTime);

        // **Olduðu Yerde Saða - Sola Dönme (A-D)**
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // **Zýplama**
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        // **Yerçekimi uygula**
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // **Týrmanma Fonksiyonu**
    void Climb()
    {
        isGrounded = false; // Týrmanýrken karakterin yerde olmadýðýný belirtiyoruz

        float climbInput = Input.GetAxis("Vertical"); // W-S ile yukarý/aþaðý
        Vector3 climbMove = new Vector3(0, climbInput * climbSpeed, 0);
        controller.Move(climbMove * Time.deltaTime);

        // **Týrmanmayý býraktýðýnda düþmeye baþla**
        if (climbInput == 0) // Eðer W veya S basýlmýyorsa düþüþ baþlasýn
        {
            Debug.Log("Týrmanmayý býraktýn, düþüyorsun! ??");
            isClimbing = false;
            velocity.y = 0; // Düþmeye baþlasýn
        }

        // **Týrmanmadan uzaklaþýnca çýk**
        if (climbableObject != null && Vector3.Distance(transform.position, climbableObject.position) > 3f)
        {
            Debug.Log("Týrmanma alanýndan uzaklaþtýn, artýk týrmanamazsýn! ??");
            isClimbing = false;
        }
    }

    // **Týrmanmayý Aç**
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("Týrmanma alanýna girdin! ?????");
            isClimbing = true;
            climbableObject = other.transform;
            velocity.y = 0; // Ýlk an yerçekimi sýfýrlanmasýn
        }
    }

    // **Týrmanmayý Kapat ve DÜÞÜÞ Baþlat**
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("Týrmanma alanýndan çýktýn, düþüyorsun! ??");
            isClimbing = false;
            velocity.y = -5f; // Düþmeye baþla
        }
    }
}
 