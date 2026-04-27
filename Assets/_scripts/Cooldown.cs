using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public Texture normal;
    public Texture aiming;
    public Texture honing;
    public camera camScript;
    RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
        image.texture = normal;
    }
    
    public void Switch(bool ranged, bool honed)
    {
        if(ranged)
        {
            if(honed)
            {
                image.texture = honing;
            } 
            else
            {
                image.texture = aiming;
            }
            camScript.Collide(true);
        } 
        else
        {
            image.texture = normal;
            camScript.Collide(false);
        }
    }
}
