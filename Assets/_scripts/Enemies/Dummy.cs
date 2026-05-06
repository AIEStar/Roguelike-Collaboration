using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Dummy : Enemy
{
    public Material hitMaterial;
    public Material hitMaterial2;
    public Material normMaterial;

    MeshRenderer render;

    private void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    public override void MeleeHit()
    {
        render.material = hitMaterial;
    }

    public override void RangedHit()
    {
        render.material = hitMaterial2;
    }
}
