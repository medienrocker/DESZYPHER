using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class AESEncryption : MonoBehaviour
{
    public TMP_InputField inputFieldMessage;
    public TMP_InputField inputFieldKey;
    public TextMeshProUGUI resultText;
    public ToastManager toastManager;
    public float duration = 3.0f;
    public GameObject particleEffect; // Reference to the particle effect GameObject

    public AudioControl audioControl;

    void Start()
    {
        // audioControl = GetComponent<AudioControl>();
    }

    public void EncryptMessage()
    {
        if (string.IsNullOrEmpty(inputFieldMessage.text))
        {
            toastManager.ShowToast("Bitte Nachricht eingeben!", duration);
            return;
        }

        if (string.IsNullOrEmpty(inputFieldKey.text))
        {
            toastManager.ShowToast("Bitte Schlüssel eingeben!", duration);
            return;
        }

        try
        {
            string encrypted = CryptoHelper.Encrypt(inputFieldMessage.text, inputFieldKey.text);
            resultText.text = encrypted;
            toastManager.ShowToast("Nachricht verschlüsselt!", duration);
        }
        catch (Exception ex)
        {
            Debug.LogError("Encryption failed: " + ex.Message);
            toastManager.ShowToast("Verschlüsselung fehlgeschlagen!", duration);
        }
    }

    public void DecryptMessage()
    {
        if (string.IsNullOrEmpty(inputFieldMessage.text))
        {
            toastManager.ShowToast("Bitte verschlüsselte Nachricht eingeben!", duration);
            return;
        }

        if (string.IsNullOrEmpty(inputFieldKey.text))
        {
            toastManager.ShowToast("Bitte Schlüssel eingeben!", duration);
            return;
        }

        try
        {
            string decrypted = CryptoHelper.Decrypt(inputFieldMessage.text, inputFieldKey.text);
            resultText.text = decrypted;
            toastManager.ShowToast("Nachricht entschlüsselt!", duration);

            if (!audioControl.isParticleEffectMuted)
            {
                particleEffect.SetActive(true);
                StartCoroutine(DisableParticleEffectAfterDelay(5.0f));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Decryption failed: " + ex.Message);
            toastManager.ShowToast("Entschlüsselung fehlgeschlagen!", duration);
        }
    }

    private IEnumerator DisableParticleEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        particleEffect.SetActive(false);
    }

    public void CopyToClipboard()
    {
        TextEditor te = new TextEditor
        {
            text = resultText.text
        };
        te.SelectAll();
        te.Copy();
        toastManager.ShowToast("Nachricht kopiert!", duration);
    }

    public void PasteFromClipboard()
    {
        inputFieldMessage.text = GUIUtility.systemCopyBuffer;
        toastManager.ShowToast("Nachricht eingefügt!", duration);
    }

    public void QuitApplication()
    {
        Debug.Log("Application quitting...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
