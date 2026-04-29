using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Material hitMaterial;
    public Material hitMaterial2;
    public Material normMaterial;

    MeshRenderer render;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    public void MeleeHit()
    {
        render.material = hitMaterial;
    }

    public void RangedHit()
    {
        render.material = hitMaterial2;
    }
}
