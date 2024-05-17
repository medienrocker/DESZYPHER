using UnityEngine;
using System.Collections;
using TMPro; 

public class ToastManager : MonoBehaviour
{
    public TextMeshProUGUI toastText; // Change to TextMeshProUGUI
    public GameObject toastContainer;  // Assume this is the parent object of the toastText which can be enabled/disabled.

    void Start()
    {
        // Initially hide the toast message
        toastContainer.SetActive(false);
    }

    public void ShowToast(string message, float duration)
    {
        StartCoroutine(ToastRoutine(message, duration));
    }

    private IEnumerator ToastRoutine(string message, float duration)
    {
        toastText.text = message;
        toastContainer.SetActive(true);
        yield return new WaitForSeconds(duration);
        toastContainer.SetActive(false);
    }
}


