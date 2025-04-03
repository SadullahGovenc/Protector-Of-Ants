using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset = new Vector3(0, 5, -5);  // Kameranýn karaktere uzaklýðý
    public float smoothSpeed = 5f;  // Yumuþak takip hýzý

    void LateUpdate()
    {
        if (target == null) return;

        // Kameranýn hedef pozisyonunu hesapla
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;
        transform.LookAt(target);  // Kamerayý karaktere çevir
    }
}
/* //BU DENENEBÝLÝR
 * 
 
 using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Takip edilecek karakter
    public Vector3 offset = new Vector3(0, 3, -5);  // Kameranýn karaktere uzaklýðý
    public float rotationSpeed = 3f;  // Fare hareketine duyarlýlýk
    public float smoothSpeed = 5f;  // Yumuþatma hýzý

    private float currentX = 0f;  // Yatay hareket için deðiþken
    private float currentY = 0f;  // Dikey hareket için deðiþken
    public float minYAngle = -20f;  // Kamera aþaðý sýnýrý
    public float maxYAngle = 50f;  // Kamera yukarý sýnýrý

    void Update()
    {
        // Fare hareketini al
        currentX += Input.GetAxis("Mouse X") * rotationSpeed;
        currentY -= Input.GetAxis("Mouse Y") * rotationSpeed;

        // Dikey açýyý sýnýrla
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Yeni kamera dönüþ açýsýný hesapla
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 desiredPosition = target.position + rotation * offset;

        // Kamerayý yumuþak bir þekilde hareket ettir
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.LookAt(target.position + Vector3.up * 1.5f);  // Karakterin biraz üstüne bak
    }
}

 
 
 
 */