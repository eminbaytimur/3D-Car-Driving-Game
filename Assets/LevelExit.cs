using UnityEngine;
using UnityEngine.SceneManagement; // Sahne deðiþimi için þart

public class LevelExit : MonoBehaviour
{
    public string sceneToLoad; // Inspector'dan yazacaðýz

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null && other.attachedRigidbody.CompareTag("Player"))
        {
            Debug.Log("Bölüm Bitti! Diðer sahneye geçiliyor...");
            // Eðer yöneticiye yazdýðýmýz sahne ismi varsa onu yükle, yoksa buradakini
            if (BoxGameManager.Instance != null && !string.IsNullOrEmpty(BoxGameManager.Instance.nextLevelName))
            {
                SceneManager.LoadScene(BoxGameManager.Instance.nextLevelName);
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}