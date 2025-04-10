using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public float mouseSensitivity = 200f; // Fare hassasiyeti
    public Transform cameraTransform; // Kamera pozisyon bilgisi
    public Transform playerBody; // Ana obje pozisyon bilgisi
    public float movementSpeed = 5f; // Hareket h�z�
    public float jumpHeight = 1.5f; // Z�plama y�ksekli�i
    public float gravity = -9.81f; // Yer�ekimi kuvveti

    private float xRotation = 0f; // Kamera i�in dikey d�nme a��s�
    private CharacterController characterController; // Karakter hareketi kontrol�
    private Vector3 velocity; // H�z vekt�r� (yer�ekimi i�in)
    private bool isGrounded; // Zeminde olup olmad���n� kontrol etmek i�in

    public LayerMask groundLayer; // Zemin katman�
    public Transform groundCheck; // Zemin kontrol� i�in bo� bir nesne
    public float groundDistance = 0.4f; // Zemin kontrol mesafesi

    void Start()
    {
      
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("CharacterController bile�eni eksik!");
            }
        

        // Fare imlecini kilitler ve gizler.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // CharacterController bile�enini al.
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {

        // Zemin kontrol� yap.
        CheckGround();

        // Karakter hareketini i�leyin.
        HandleMovement();

        // Z�plama ve yer�ekimi i�le.
        HandleGravityAndJump();

    }
    // Fiziksel i�lemlerle �ak��mamas� i�in Kamera i�lemlerini LateUpdate ile uygula
    void LateUpdate()
    {
        // Kamera i�lemlerini sonradan uygula
        HandleMouseLook();
    }

    void HandleMouseLook()
    {
        // Fare hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Yukar� a�a�� bak�� a��s�n� s�n�rla (sadece kamera i�in)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Kameran�n yukar� a�a�� bak���n� kontrol et (sadece kamera d�ner)
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakteri sa�a sola d�nd�r (playerBody nesnesi d�ner)
        playerBody.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Hareket y�n�n� hesapla.
        Vector3 direction = transform.right * horizontal + transform.forward * vertical;

        // Karakteri hareket ettir.
        characterController.Move(movementSpeed * Time.deltaTime * direction);
    }

    void HandleGravityAndJump()
    {
        // E�er karakter zemindeyse, h�z vekt�r�n� s�f�rla ve z�plama kontrol� yap.
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Zemine yap���k kalmas�n� sa�lamak i�in hafif negatif de�er kullan�l�r.
        }

        // Z�plama kontrol�.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Z�plama fizi�i form�l�.
        }

        // Yer�ekimi uygula.
        velocity.y += gravity * Time.deltaTime;

        // H�z� karakterin hareketine ekle.
        characterController.Move(velocity * Time.deltaTime);
    }

    void CheckGround()
    {
        // Zemin kontrol�: groundCheck nesnesinden a�a�� do�ru bir k�re ���n� g�nder.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // Debugging i�in: Zemin kontrol k�resini g�ster.
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, Color.red);
    }
}