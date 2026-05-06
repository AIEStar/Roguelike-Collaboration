
using UnityEngine;

public class Spinner : MonoBehaviour
{
    // Adjustable speed and direction from the Inspector
    [SerializeField] public Vector3 rotationSpeed = new Vector3(0, 100, 0);

    void Update()
    {
        // Rotate the object every frame, independent of framerate
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
