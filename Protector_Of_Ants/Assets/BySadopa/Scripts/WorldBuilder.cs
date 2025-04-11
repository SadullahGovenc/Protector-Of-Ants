using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class WorldBuilder : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject grassPrefab;
    public int width = 100;
    public int height = 100;
    public float tileSize = 2f;

    [ContextMenu("Yol + Çim Haritasý Oluþtur")]
    public void BuildWorld()
    {
        ClearChildren();

        HashSet<Vector2Int> roadPoints = GenerateMultiBranchPath();
        Vector3 origin = transform.position - new Vector3(width * tileSize / 2f, 0, height * tileSize / 2f);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 spawnPos = origin + new Vector3(x * tileSize, 0, z * tileSize);
                Vector2Int gridPos = new Vector2Int(x, z);

                GameObject prefab = roadPoints.Contains(gridPos) ? roadPrefab : grassPrefab;
                GameObject spawned = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                spawned.transform.position = spawnPos;
                spawned.transform.SetParent(transform);
            }
        }

        Debug.Log("Dünya oluþturuldu!");
    }

    HashSet<Vector2Int> GenerateMultiBranchPath()
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        // Ana yol: soldan saða
        Vector2Int current = new Vector2Int(0, height / 2);
        path.Add(current);

        while (current.x < width - 1)
        {
            int dir = Random.Range(0, 3); // 0: ileri, 1: yukarý, 2: aþaðý

            if (dir == 0 && current.x < width - 1)
                current += Vector2Int.right;
            else if (dir == 1 && current.y < height - 1)
                current += Vector2Int.up;
            else if (dir == 2 && current.y > 0)
                current += Vector2Int.down;

            path.Add(current);

            // Rastgele dallanma: yeni bir koldan devam et
            if (Random.value < 0.1f)
            {
                Vector2Int branchStart = current;
                int branchLength = Random.Range(5, 15);
                Vector2Int branchDir = Random.value > 0.5f ? Vector2Int.up : Vector2Int.down;

                for (int i = 0; i < branchLength; i++)
                {
                    branchStart += branchDir;
                    if (branchStart.x >= 0 && branchStart.x < width && branchStart.y >= 0 && branchStart.y < height)
                        path.Add(branchStart);
                }
            }
        }

        return path;
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
