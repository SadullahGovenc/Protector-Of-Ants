/*using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // �leri-geri hareket h�z�
    public float rotationSpeed = 200f;  // Sa�a-sola d�n�� h�z�
    public float jumpForce = 8f;  // Z�plama g�c�
    public float gravity = -9.81f;  // Yer�ekimi
    public float climbSpeed = 3f;  // T�rmanma h�z�

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isClimbing = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // E�er CharacterController eksikse hata mesaj� verelim
        if (controller == null)
        {
            Debug.LogError("CharacterController bile�eni eksik! L�tfen ekleyin.");
            enabled = false; // Scripti devre d��� b�rak
        }
    }

    void Update()
    {
        if (controller == null) return;

        // Yere temas kontrol�
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // **�leri - Geri Hareket (W-S)**
        float moveZ = Input.GetAxis("Vertical");  // W ve S tu�lar�
        Vector3 move = transform.forward * moveZ;

        if (!isClimbing) // T�rmanm�yorsa normal hareket
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        // **Oldu�u Yerde Sa�a - Sola D�nme (A-D)**
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // **Z�plama**
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        // **A�aca T�rmanma**
        if (isClimbing)
        {
            float climbInput = Input.GetAxis("Vertical"); // W-S ile yukar�/a�a��
            Vector3 climbMove = new Vector3(0, climbInput * climbSpeed, 0);
            controller.Move(climbMove * Time.deltaTime);
        }
        else
        {
            // Yer�ekimi uygula
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // **A�aca T�rmanma Alan�**
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
    public float walkSpeed = 5f;  // Normal y�r�me h�z�
    public float runSpeed = 9f;   // Shift bas�nca h�zl� ko�ma h�z�
    public float slowSpeed = 2f;  // Ctrl bas�nca yava� y�r�me h�z�
    public float rotationSpeed = 200f;  // Sa�a-sola d�n�� h�z�
    public float jumpForce = 8f;  // Z�plama g�c�
    public float gravity = -9.81f;  // Yer�ekimi
    public float climbSpeed = 3f;  // T�rmanma h�z�

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isClimbing = false;
    private Transform climbableObject; // T�rman�labilir objeyi sakla

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (controller == null)
        {
            Debug.LogError("CharacterController bile�eni eksik! L�tfen ekleyin.");
            enabled = false;
        }
    }

    void Update()
    {
        if (controller == null) return;

        // Yere temas kontrol�
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // ?? H�z belirleme (Shift = H�zl�, Ctrl = Yava�, Normal = Y�r�me)
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed; // Shift bas�nca h�zl� ko�
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = slowSpeed; // Ctrl bas�nca yava� y�r�
        }

        // **�leri - Geri Hareket (W-S)**
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveZ;

        if (!isClimbing) // T�rmanm�yorsa normal hareket
        {
            controller.Move(move * currentSpeed * Time.deltaTime);
        }

        // **Oldu�u Yerde Sa�a - Sola D�nme (A-D)**
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // **Z�plama**
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        // **A�aca T�rmanma**
        if (isClimbing)
        {
            float climbInput = Input.GetAxis("Vertical"); // W-S ile yukar�/a�a��
            Vector3 climbMove = new Vector3(0, climbInput * climbSpeed, 0);
            controller.Move(climbMove * Time.deltaTime);
        }
        else
        {
            // Yer�ekimi uygula
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        // **E�er t�rmanma aktif ama �ok uzakla�t�ysak ��k**
        if (isClimbing && climbableObject != null && Vector3.Distance(transform.position, climbableObject.position) > 3f)
        {
            Debug.Log("T�rmanma alan�ndan uzakla�t�n, art�k t�rmanamazs�n! ");
            isClimbing = false;
        }
    }

    // **T�rmanmay� A�**
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("T�rmanma alan�na girdin! ");
            isClimbing = true;
            climbableObject = other.transform;
        }
    }

    // **T�rmanmay� Kapat**
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("T�rmanma alan�ndan ��kt�n! ");
            isClimbing = false;
        }
    }
}*/
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;  // Normal y�r�me h�z�
    public float runSpeed = 9f;   // Shift bas�nca h�zl� ko�ma h�z�
    public float slowSpeed = 2f;  // Ctrl bas�nca yava� y�r�me h�z�
    public float rotationSpeed = 200f;  // Sa�a-sola d�n�� h�z�
    public float jumpForce = 8f;  // Z�plama g�c�
    public float gravity = -20f;  // Yer�ekimi (D��me h�z�n� art�rd�m)
    public float climbSpeed = 3f;  // T�rmanma h�z�
    public float fallMultiplier = 2f; // D��me h�z�n� art�r�c� �arpan

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
            Debug.LogError("CharacterController bile�eni eksik! L�tfen ekleyin.");
            enabled = false;
        }
    }

    void Update()
    {
        if (controller == null) return;

        // **T�rmanma Kontrol�**
        if (isClimbing)
        {
            Climb();
        }
        else
        {
            Move();
        }

        // **E�er t�rmanmay� b�rakt�ysa ve havadaysa d����� h�zland�r**
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

        // ?? H�z belirleme (Shift = H�zl�, Ctrl = Yava�, Normal = Y�r�me)
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = runSpeed; // Shift bas�nca h�zl� ko�
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            currentSpeed = slowSpeed; // Ctrl bas�nca yava� y�r�
        }

        // **�leri - Geri Hareket (W-S)**
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveZ * currentSpeed;

        controller.Move(move * Time.deltaTime);

        // **Oldu�u Yerde Sa�a - Sola D�nme (A-D)**
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // **Z�plama**
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        // **Yer�ekimi uygula**
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // **T�rmanma Fonksiyonu**
    void Climb()
    {
        isGrounded = false; // T�rman�rken karakterin yerde olmad���n� belirtiyoruz

        float climbInput = Input.GetAxis("Vertical"); // W-S ile yukar�/a�a��
        Vector3 climbMove = new Vector3(0, climbInput * climbSpeed, 0);
        controller.Move(climbMove * Time.deltaTime);

        // **T�rmanmay� b�rakt���nda d��meye ba�la**
        if (climbInput == 0) // E�er W veya S bas�lm�yorsa d���� ba�las�n
        {
            Debug.Log("T�rmanmay� b�rakt�n, d���yorsun! ??");
            isClimbing = false;
            velocity.y = 0; // D��meye ba�las�n
        }

        // **T�rmanmadan uzakla��nca ��k**
        if (climbableObject != null && Vector3.Distance(transform.position, climbableObject.position) > 3f)
        {
            Debug.Log("T�rmanma alan�ndan uzakla�t�n, art�k t�rmanamazs�n! ??");
            isClimbing = false;
        }
    }

    // **T�rmanmay� A�**
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("T�rmanma alan�na girdin! ?????");
            isClimbing = true;
            climbableObject = other.transform;
            velocity.y = 0; // �lk an yer�ekimi s�f�rlanmas�n
        }
    }

    // **T�rmanmay� Kapat ve D���� Ba�lat**
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            Debug.Log("T�rmanma alan�ndan ��kt�n, d���yorsun! ??");
            isClimbing = false;
            velocity.y = -5f; // D��meye ba�la
        }
    }
}
 