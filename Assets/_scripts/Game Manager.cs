using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public RawImage weaponRender;
    public Camera weaponRenderCamera;

    private void Start()
    {
        //weaponRenderCamera.targetTexture.Release();
        //RenderTexture newTexture = new(Screen.width, Screen.height, 24);
        //weaponRenderCamera.targetTexture = newTexture;
        //weaponRender.texture = newTexture;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            #if (UNITY_EDITOR)
                EditorApplication.isPaused = true;
            #endif
        }
    }

}
