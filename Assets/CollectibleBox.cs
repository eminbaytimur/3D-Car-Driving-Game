using UnityEngine;

public class CollectibleBox : MonoBehaviour
{
    // KORUMA KALKANI: Bu deðiþken kutunun toplandýðýný hafýzasýnda tutar.
    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        // Eðer bu kutu zaten toplandý olarak iþaretlendiyse, hiçbir þey yapma!
        if (isCollected) return;

        Rigidbody carRigidbody = other.attachedRigidbody;

        if (carRigidbody != null && carRigidbody.CompareTag("Player"))
        {
            // ÝÞARETLE: "Ben toplandým, artýk bana kimse dokunamaz."
            isCollected = true;

            // Puaný ver
            BoxGameManager.Instance.CollectBox();

            // Kutuyu yok et
            Destroy(gameObject);
        }
    }
}