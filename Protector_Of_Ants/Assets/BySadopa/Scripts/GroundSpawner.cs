using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Zemin Prefablar�")]
    public GameObject[] groundPrefabs;

    [Header("Yerle�im Ayarlar�")]
    public Transform groundPlane;     // �zerine da��t�lacak b�y�k plane
    public float areaSize = 600f;     // Prefablerin kaplayaca�� kare alan (600x600)
    public float tileSize = 5f;       // Her prefab ne kadar alan kapl�yor
    public bool randomize = true;

    void Start()
    {
        SpawnGround();
    }

    void SpawnGround()
    {
        if (groundPlane == null)
        {
            Debug.LogError("GroundPlane atanmam��!");
            return;
        }

        // Plane�in merkezinden ba�layarak yerle�tirme yapaca��z
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

        Debug.Log($"Toplam {tilesX * tilesZ} zemin prefab� yerle�tirildi.");
    }
}
