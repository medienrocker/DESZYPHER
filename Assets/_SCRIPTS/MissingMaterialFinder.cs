using UnityEngine;
using TMPro;

public class MissingMaterialFinder : MonoBehaviour
{
    void Start()
    {
        // Find all TextMeshProUGUI components in the scene
        TextMeshProUGUI[] textComponents = FindObjectsOfType<TextMeshProUGUI>();

        // Iterate through each TextMeshProUGUI component
        foreach (TextMeshProUGUI textComponent in textComponents)
        {
            // Check if the fontSharedMaterial is null
            if (textComponent.fontSharedMaterial == null)
            {
                // Log a warning with the name of the GameObject and a reference to it
                Debug.LogWarning("Missing material on GameObject: " + textComponent.gameObject.name, textComponent.gameObject);
            }
            else
            {
                // Log the name of the material
                Debug.Log("Material on GameObject " + textComponent.gameObject.name + ": " + textComponent.fontSharedMaterial.name);
            }
        }
    }
}

