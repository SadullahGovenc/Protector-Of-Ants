using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProceduralGroundSpawner : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject grassPrefab;
    public int width = 100;
    public int height = 100;
    public float tileSize = 2f;

    [HideInInspector] public HashSet<Vector2Int> roadPositions = new HashSet<Vector2Int>();

    [ContextMenu(" Haritayı Oluştur")]
    public void GenerateWorld()
    {
        ClearChildren();
        roadPositions = GenerateMultiBranchPaths();
        SpawnTiles();
    }

    HashSet<Vector2Int> GenerateMultiBranchPaths()
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        int mainPathStartY = height / 2;
        Vector2Int current = new Vector2Int(0, mainPathStartY);
        path.Add(current);

        //  Ana yol: soldan sağa gider
        while (current.x < width - 1)
        {
            int dir = Random.Range(0, 3); // sağ, yukarı, aşağı

            if (dir == 0 && current.x < width - 1)
                current += Vector2Int.right;
            else if (dir == 1 && current.y < height - 1)
                current += Vector2Int.up;
            else if (dir == 2 && current.y > 0)
                current += Vector2Int.down;

            path.Add(current);

            // DAHA UZUN DALLAR
            if (Random.value < 0.03f) // %5 ihtimalle dal başlat
            {
                Vector2Int branchStart = current;
                int branchLength = Random.Range(50, 120); //  Minimum 50 karelik uzunluk
                Vector2Int branchDir;

                float r = Random.value;
                if (r < 0.33f) branchDir = Vector2Int.up;
                else if (r < 0.66f) branchDir = Vector2Int.down;
                else branchDir = Vector2Int.right; //  Dal sağa da sapabilir

                for (int i = 0; i < branchLength; i++)
                {
                    branchStart += branchDir;

                    // Sınır kontrolü
                    if (branchStart.x < 0 || branchStart.x >= width || branchStart.y < 0 || branchStart.y >= height)
                        break;

                    path.Add(branchStart);

                    // Arada yönü değiştir: dalı biraz daha doğal göster
                    if (Random.value < 0.2f)
                    {
                        branchDir = Random.Range(0, 2) == 0 ? Vector2Int.up : Vector2Int.down;
                    }

                    // Ufak sağ sapmalar
                    if (Random.value < 0.3f && branchStart.x < width - 1)
                    {
                        branchStart += Vector2Int.right;
                        path.Add(branchStart);
                    }
                }
            }
        }

        Debug.Log($"Toplam Yol Uzunluğu: {path.Count} tile.");
        return path;
    }


    void SpawnTiles()
    {
        Vector3 origin = transform.position - new Vector3(width * tileSize / 2f, 0, height * tileSize / 2f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector2Int gridPos = new Vector2Int(x, z);
                Vector3 spawnPos = origin + new Vector3(x * tileSize, 0, z * tileSize);

                GameObject prefabToSpawn = roadPositions.Contains(gridPos) ? roadPrefab : grassPrefab;

                GameObject spawned = (GameObject)PrefabUtility.InstantiatePrefab(prefabToSpawn);
                spawned.transform.position = spawnPos;
                spawned.transform.SetParent(transform);
            }
        }
    }

    void ClearChildren()
    {
#if UNITY_EDITOR
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
#endif
    }
}
