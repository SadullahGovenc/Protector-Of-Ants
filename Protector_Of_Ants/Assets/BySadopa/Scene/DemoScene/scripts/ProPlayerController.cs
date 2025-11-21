using UnityEngine;
using UnityEngine.InputSystem; // Yeni sistemi kullanýyoruz

[RequireComponent(typeof(CharacterController))]
public class ProPlayerController : MonoBehaviour
{
    private Animator _animator;
    [Header("Veri Dosyasý")]
    [SerializeField] private PlayerStats stats; // Oluþturduðun MyAntStats buraya gelecek

    [Header("Debug")]
    [SerializeField] private PlayerState currentState; // Anlýk durumu görmek için

    // Durum Listesi
    public enum PlayerState { Idle, Walk, Run, Crouch, Air }
    // --- ANIMATOR HASH ID'LERÝ (String'den daha hýzlý ve optimize) ---
    private static readonly int AnimID_Walk = Animator.StringToHash("isWalking");
    private static readonly int AnimID_Run = Animator.StringToHash("isRunning");
    private static readonly int AnimID_Crouch = Animator.StringToHash("isCrouching");

    // Bileþenler
    private CharacterController _controller;
    private Transform _camTransform;

    // Deðiþkenler
    private Vector2 _inputVector;     // Klavye giriþi (WASD)
    private Vector3 _velocity;        // Düþme hýzý
    private bool _isSprinting;        // Shift basýlý mý?
    private bool _isCrouching;        // Eðilme tuþu basýlý mý?
    private float _turnSmoothVelocity; // Dönüþ yumuþatma referansý

    private void Awake()
    {
        // Awake fonksiyonunda bileþeni cache et
        _animator = GetComponentInChildren<Animator>();
        // CharacterController'ýn altýndaki modeli bulur
        _controller = GetComponent<CharacterController>();
        _camTransform = Camera.main.transform; // Ana kamerayý bul

        // Baþlangýç boyunu ayarla
        if (stats != null) _controller.height = stats.normalHeight;
    }

    private void Update()
    {
        if (stats == null)
        {
            Debug.LogError("Lütfen Inspector'dan 'Stats' dosyasýna MyAntStats'ý sürükle!");
            return;
        }

        HandleState();     // 1. Durumu belirle
                           // Update fonksiyonu içinde, HandleState() çaðrýsýndan sonra:

        // Animasyonlarý ayarla
        bool isWalking = currentState == PlayerState.Walk;
        bool isRunning = currentState == PlayerState.Run;
        bool isCrouching = currentState == PlayerState.Crouch;

        _animator.SetBool(AnimID_Walk, isWalking);
        _animator.SetBool(AnimID_Run, isRunning);
        _animator.SetBool(AnimID_Crouch, isCrouching);

        ApplyMovement();   // 2. Hareketi yap
        ApplyGravity();    // 3. Yerçekimi uygula
    }

    // --- 1. DURUM BELÝRLEME ---
    private void HandleState()
    {
        if (_controller.isGrounded) // Yerdeysek
        {
            if (_isCrouching)
            {
                currentState = PlayerState.Crouch;
            }
            else if (_inputVector.magnitude > 0.1f)
            {
                currentState = _isSprinting ? PlayerState.Run : PlayerState.Walk;
            }
            else
            {
                currentState = PlayerState.Idle;
            }
        }
        else // Yerde deðilsek
        {
            currentState = PlayerState.Air;
        }
    }

    // --- 2. HAREKET MANTIÐI ---
    private void ApplyMovement()
    {
        float targetSpeed = 0f;

        // Duruma göre hýz seç
        switch (currentState)
        {
            case PlayerState.Walk: targetSpeed = stats.walkSpeed; break;
            case PlayerState.Run: targetSpeed = stats.runSpeed; break;
            case PlayerState.Crouch: targetSpeed = stats.walkSpeed / 2f; break;
            case PlayerState.Air: targetSpeed = stats.walkSpeed * 0.8f; break;
        }

        // Yön Hesabý
        Vector3 direction = new Vector3(_inputVector.x, 0f, _inputVector.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Kameranýn baktýðý yöne göre açýyý hesapla
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;

            // Karakteri o yöne yumuþakça döndür
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Hareket vektörü
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _controller.Move(moveDir.normalized * targetSpeed * Time.deltaTime);
        }

        // Eðilme Boyut Ayarý (Lerp ile yumuþak geçiþ)
        float targetHeight = (currentState == PlayerState.Crouch) ? stats.crouchHeight : stats.normalHeight;
        _controller.height = Mathf.Lerp(_controller.height, targetHeight, 10 * Time.deltaTime);

        // Boyut deðiþince ayaklarýn yere basmasý için merkezi ayarla
        _controller.center = Vector3.down * (stats.normalHeight - _controller.height) / 2.0f;
    }

    // --- 3. YERÇEKÝMÝ ---
    private void ApplyGravity()
    {
        if (_controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f; // Yere yapýþýk kal
        }

        _velocity.y += stats.gravity * Time.deltaTime;
        _controller.Move(Vector3.up * _velocity.y * Time.deltaTime);
    }

    // --- INPUT SÝSTEMÝNDEN GELEN MESAJLAR ---
    // Unity bu fonksiyonlarý otomatik çaðýracak (Send Messages modu sayesinde)

    public void OnMove(InputValue value) => _inputVector = value.Get<Vector2>();

    public void OnSprint(InputValue value) => _isSprinting = value.isPressed;

    public void OnCrouch(InputValue value) => _isCrouching = value.isPressed;

    public void OnJump(InputValue value)
    {
        if (value.isPressed && _controller.isGrounded)
        {
            _velocity.y = Mathf.Sqrt(stats.jumpHeight * -2f * stats.gravity);
        }
    }
}
