using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RABBar : MonoBehaviour
{
    public Image imgHealthBar;
    private int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {

            imgHealthBar.fillAmount = imgHealthBar.fillAmount - (damage * 0.01f);

        }
    }
}
