using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject tilePrefab; // 1x1x1 low poly zemin prefab'ýný buraya sürükle
    public int sizeX = 100;
    public int sizeZ = 100;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile prefab atanmadý!");
            return;
        }

        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                Vector3 pos = new Vector3(x, 0, z); // her biri 1 birim aralýklý
                Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
