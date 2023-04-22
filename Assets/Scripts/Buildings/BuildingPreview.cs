using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPreview : MonoBehaviour
{
    public Material canPlaceMaterial;
    public Material cantPlaceMaterial;
    private MeshRenderer[] meshRenderers;
    private List<GameObject> collidingObjets = new List<GameObject>();

    private void Awake()
    {
        meshRenderers = transform.GetComponentsInChildren<MeshRenderer>();
    }

    public void CanPlace()
    {
        SetMaterial(canPlaceMaterial);
    }

    public void CantPlace()
    {
        SetMaterial(cantPlaceMaterial);
    }

    void SetMaterial(Material mat)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            Material[] mats = new Material[meshRenderers[i].materials.Length];
            for (int j = 0; j < mats.Length; j++)
            {
                mats[j] = mat;
            }
            meshRenderers[i].materials = mats;
        }
    }

    public bool CollidingWithObjects()
    {
        collidingObjets.RemoveAll(x => x == null);
        return collidingObjets.Count > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer != 10)
        {
            collidingObjets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 10)
        {
            collidingObjets.Remove(other.gameObject);
        }
    }
}
