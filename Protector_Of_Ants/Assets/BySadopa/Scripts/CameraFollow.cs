using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset = new Vector3(0, 5, -5);  // Kameran�n karaktere uzakl���
    public float smoothSpeed = 5f;  // Yumu�ak takip h�z�

    void LateUpdate()
    {
        if (target == null) return;

        // Kameran�n hedef pozisyonunu hesapla
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
        transform.LookAt(target);  // Kameray� karaktere �evir
    }
}
/* //BU DENENEB�L�R
 * 
 
 using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset = new Vector3(0, 3, -5);  // Kameran�n karaktere uzakl���
    public float rotationSpeed = 3f;  // Fare hareketine duyarl�l�k
    public float smoothSpeed = 5f;  // Yumu�atma h�z�

    private float currentX = 0f;  // Yatay hareket i�in de�i�ken
    private float currentY = 0f;  // Dikey hareket i�in de�i�ken
    public float minYAngle = -20f;  // Kamera a�a�� s�n�r�
    public float maxYAngle = 50f;  // Kamera yukar� s�n�r�

    void Update()
    {
        // Fare hareketini al
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Dikey a��y� s�n�rla
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Yeni kamera d�n�� a��s�n� hesapla
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Kameray� yumu�ak bir �ekilde hareket ettir
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);  // Karakterin biraz �st�ne bak
    }
}

 
 
 
 */