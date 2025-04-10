using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float mouseSensitivity = 200f; // Fare hassasiyeti
    public Transform cameraTransform; // Kamera pozisyon bilgisi
    public Transform playerBody; // Ana obje pozisyon bilgisi
    public float movementSpeed = 5f; // Hareket hýzý
    public float jumpHeight = 1.5f; // Zýplama yüksekliði
    public float gravity = -9.81f; // Yerçekimi kuvveti

    private float xRotation = 0f; // Kamera için dikey dönme açýsý
    private CharacterController characterController; // Karakter hareketi kontrolü
    private Vector3 velocity; // Hýz vektörü (yerçekimi için)
    private bool isGrounded; // Zeminde olup olmadýðýný kontrol etmek için

    public LayerMask groundLayer; // Zemin katmaný
    public Transform groundCheck; // Zemin kontrolü için boþ bir nesne
    public float groundDistance = 0.4f; // Zemin kontrol mesafesi

    void Start()
    {
      
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("CharacterController bileþeni eksik!");
            }
        

        // Fare imlecini kilitler ve gizler.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // CharacterController bileþenini al.
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        // Zemin kontrolü yap.
        CheckGround();

        // Karakter hareketini iþleyin.
        HandleMovement();

        // Zýplama ve yerçekimi iþle.
        HandleGravityAndJump();

    }
    // Fiziksel iþlemlerle çakýþmamasý için Kamera iþlemlerini LateUpdate ile uygula
    void LateUpdate()
    {
        // Kamera iþlemlerini sonradan uygula
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        // Fare hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yukarý aþaðý bakýþ açýsýný sýnýrla (sadece kamera için)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kameranýn yukarý aþaðý bakýþýný kontrol et (sadece kamera döner)
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakteri saða sola döndür (playerBody nesnesi döner)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Hareket yönünü hesapla.
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        // Karakteri hareket ettir.
        characterController.Move(movementSpeed * Time.deltaTime * direction);
    }

    void HandleGravityAndJump()
    {
        // Eðer karakter zemindeyse, hýz vektörünü sýfýrla ve zýplama kontrolü yap.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Zemine yapýþýk kalmasýný saðlamak için hafif negatif deðer kullanýlýr.
        }

        // Zýplama kontrolü.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Zýplama fiziði formülü.
        }

        // Yerçekimi uygula.
        velocity.y += gravity * Time.deltaTime;

        // Hýzý karakterin hareketine ekle.
        characterController.Move(velocity * Time.deltaTime);
    }

    void CheckGround()
    {
        // Zemin kontrolü: groundCheck nesnesinden aþaðý doðru bir küre ýþýný gönder.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // Debugging için: Zemin kontrol küresini göster.
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.red);
    }
}