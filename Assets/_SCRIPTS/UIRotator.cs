using UnityEngine;

public class UIRotator : MonoBehaviour
{
    // SerializeField attribute allows you to set the rotation speed in the Unity Editor
    [SerializeField]
    private float rotationSpeed = 100f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the UI Image around its Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
