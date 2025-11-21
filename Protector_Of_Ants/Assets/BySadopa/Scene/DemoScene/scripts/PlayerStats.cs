using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Game/Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Hareket Ayarlarý")]
    public float walkSpeed = 4f;       // Yürüme hýzý
    public float runSpeed = 7f;        // Koþma hýzý
    public float gravity = -9.81f;     // Yerçekimi
    public float jumpHeight = 1.5f;    // Zýplama gücü

    [Header("Durum Ayarlarý")]
    public float crouchHeight = 1f;    // Eðilince boyu kaç olsun
    public float normalHeight = 2f;    // Normal boyu
}
