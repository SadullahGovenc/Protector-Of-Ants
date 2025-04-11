using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Zemin Prefablarý")]
    public GameObject[] groundPrefabs;

    [Header("Yerleþim Ayarlarý")]
    public Transform groundPlane;     // Üzerine daðýtýlacak büyük plane
    public float areaSize = 600f;     // Prefablerin kaplayacaðý kare alan (600x600)
    public float tileSize = 5f;       // Her prefab ne kadar alan kaplýyor
    public bool randomize = true;

    void Start()
    {
        SpawnGround();
    }

    void SpawnGround()
    {
        if (groundPlane == null)
        {
            Debug.LogError("GroundPlane atanmamýþ!");
            return;
        }

        // Plane’in merkezinden baþlayarak yerleþtirme yapacaðýz
        Vector3 planeCenter = groundPlane.position;
        float halfArea = areaSize / 2f;

        int tilesX = Mathf.FloorToInt(areaSize / tileSize);
        int tilesZ = Mathf.FloorToInt(areaSize / tileSize);

        for (int x = 0; x < tilesX; x++)
        {
            for (int z = 0; z < tilesZ; z++)
            {
                Vector3 spawnPos = new Vector3(
                    planeCenter.x - halfArea + (x * tileSize) + tileSize / 2f,
                    planeCenter.y,
                    planeCenter.z - halfArea + (z * tileSize) + tileSize / 2f
                );

                GameObject prefabToSpawn = groundPrefabs[0];

                if (randomize)
                {
                    int index = Random.Range(0, groundPrefabs.Length);
                    prefabToSpawn = groundPrefabs[index];
                }

                Instantiate(prefabToSpawn, spawnPos, Quaternion.identity, transform);
            }
        }

        Debug.Log($"Toplam {tilesX * tilesZ} zemin prefabý yerleþtirildi.");
    }
}
