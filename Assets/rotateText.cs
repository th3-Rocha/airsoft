using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required to use TextMesh Pro

public class RotateText : MonoBehaviour
{
    public float rotationSpeed = 30f; // Rotation speed (degrees per second)

    private TextMeshPro textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        // Get the TextMeshPro component from the current GameObject
        textMeshPro = GetComponent<TextMeshPro>();

        // Optional: Set an initial text value
        if (textMeshPro != null)
        {
            textMeshPro.text = "Rotating Text";
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the text around the Y axis (or any other axis)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
