using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform fpsCamTransform;  // FPS konumu (karakterin kafas�)
    public Transform tpsCamTransform;  // TPS konumu (karakterin arkas�)
    public Transform character;        // Takip edilecek karakter

    public float followSpeed = 10f;    // Takip h�z�
    public float rotationSpeed = 5f;   // D�n�� h�z�

    private Camera mainCamera;
    private bool isFPS = false;        // Ba�lang��ta TPS modu a��k

    void Start()
    {
        mainCamera = Camera.main;
        SwitchToTPS();  // Ba�lang��ta TPS aktif
    }

    void Update()
    {
        // FPS/TPS Ge�i�i (C tu�una bas�nca de�i�tir)
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
        // Hedef kameran�n olmas� gereken pozisyon
        Transform targetCamTransform = isFPS ? fpsCamTransform : tpsCamTransform;

        // Kameray� d�zg�n bir �ekilde takip ettir
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

/// Buras� ilk kulland���m ayar
/// 


/*using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform fpsCamTransform;  // FPS kameras� pozisyonu (karakterin kafas�na yak�n)
    public Transform tpsCamTransform;  // TPS kameras� pozisyonu (karakterin arkas�nda)

    public Transform target;  // Takip edilecek hedef (karakter)
    public float followSpeed = 10f;  // Kameran�n takip h�z�

    private Camera mainCamera;
    private bool isFPS = false;  // Ba�lang��ta TPS modunda olacak

    void Start()
    {
        mainCamera = Camera.main;
        SwitchToTPS();  // Oyuna TPS modunda ba�lat�yoruz
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))  // C tu�una bas�nca kamera modu de�i�sin
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