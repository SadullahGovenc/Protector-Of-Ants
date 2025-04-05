using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform fpsCamTransform;  // FPS konumu (karakterin kafasý)
    public Transform tpsCamTransform;  // TPS konumu (karakterin arkasý)
    public Transform character;        // Takip edilecek karakter

    public float followSpeed = 10f;    // Takip hýzý
    public float rotationSpeed = 5f;   // Dönüþ hýzý

    private Camera mainCamera;
    private bool isFPS = false;        // Baþlangýçta TPS modu açýk

    void Start()
    {
        mainCamera = Camera.main;
        SwitchToTPS();  // Baþlangýçta TPS aktif
    }

    void Update()
    {
        // FPS/TPS Geçiþi (C tuþuna basýnca deðiþtir)
        if (Input.GetKeyDown(KeyCode.C))
        {
            isFPS = !isFPS;

            if (isFPS)
                SwitchToFPS();
            else
                SwitchToTPS();
        }
    }

    void LateUpdate()
    {
        // Hedef kameranýn olmasý gereken pozisyon
        Transform targetCamTransform = isFPS ? fpsCamTransform : tpsCamTransform;

        // Kamerayý düzgün bir þekilde takip ettir
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCamTransform.position, followSpeed * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetCamTransform.rotation, rotationSpeed * Time.deltaTime);
    }

    void SwitchToFPS()
    {
        Debug.Log("FPS Modu Aktif");
        mainCamera.transform.position = fpsCamTransform.position;
        mainCamera.transform.rotation = fpsCamTransform.rotation;
    }

    void SwitchToTPS()
    {
        Debug.Log("TPS Modu Aktif");
        mainCamera.transform.position = tpsCamTransform.position;
        mainCamera.transform.rotation = tpsCamTransform.rotation;
    }
}

/// Burasý ilk kullandýðým ayar
/// 


/*using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform fpsCamTransform;  // FPS kamerasý pozisyonu (karakterin kafasýna yakýn)
    public Transform tpsCamTransform;  // TPS kamerasý pozisyonu (karakterin arkasýnda)

    public Transform target;  // Takip edilecek hedef (karakter)
    public float followSpeed = 10f;  // Kameranýn takip hýzý

    private Camera mainCamera;
    private bool isFPS = false;  // Baþlangýçta TPS modunda olacak

    void Start()
    {
        mainCamera = Camera.main;
        SwitchToTPS();  // Oyuna TPS modunda baþlatýyoruz
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))  // C tuþuna basýnca kamera modu deðiþsin
        {
            isFPS = !isFPS;

            if (isFPS)
                SwitchToFPS();
            else
                SwitchToTPS();
        }
    }

    void LateUpdate()
    {
        // Kamera karakteri takip etsin
        Transform targetCamTransform = isFPS ? fpsCamTransform : tpsCamTransform;
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetCamTransform.position, followSpeed * Time.deltaTime);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, targetCamTransform.rotation, followSpeed * Time.deltaTime);
    }

    void SwitchToFPS()
    {
        Debug.Log("FPS Modu Aktif");
        mainCamera.transform.position = fpsCamTransform.position;
        mainCamera.transform.rotation = fpsCamTransform.rotation;
    }

    void SwitchToTPS()
    {
        Debug.Log("TPS Modu Aktif");
        mainCamera.transform.position = tpsCamTransform.position;
        mainCamera.transform.rotation = tpsCamTransform.rotation;
    }
}
*/