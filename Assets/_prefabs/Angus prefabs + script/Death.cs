using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnollisionEnter(Collision collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Quitting...");
            SceneManager.LoadScene("angus test scene 1");
        }
    }
}
