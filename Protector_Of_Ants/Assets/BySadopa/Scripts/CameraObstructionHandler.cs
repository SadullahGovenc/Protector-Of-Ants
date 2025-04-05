using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstructionHandler : MonoBehaviour
{
    public Transform player;  // Takip edilen oyuncu
    public LayerMask obstructionMask;  // Engellenebilecek nesneler
    private List<Renderer> obstructedObjects = new List<Renderer>();

    void Update()
    {
        HandleObstructions();
    }

    void HandleObstructions()
    {
        // Önceki nesneleri tekrar görünür yap
        foreach (var obj in obstructedObjects)
        {
            if (obj != null)
                obj.material.color = new Color(obj.material.color.r, obj.material.color.g, obj.material.color.b, 1f);
        }
        obstructedObjects.Clear();

        // Kamera ile oyuncu arasýndaki engelleri kontrol et
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, obstructionMask);

        foreach (var hit in hits)
        {
            Renderer objRenderer = hit.collider.GetComponent<Renderer>();

            if (objRenderer != null)
            {
                // Þeffaf yap
                objRenderer.material.color = new Color(objRenderer.material.color.r, objRenderer.material.color.g, objRenderer.material.color.b, 0.3f);
                obstructedObjects.Add(objRenderer);
            }
        }
    }
}
