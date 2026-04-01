using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public RenderTexture weaponRender;

    void Start()
    {
        weaponRender.width = Screen.width;
        weaponRender.height = Screen.height;
    }

}
