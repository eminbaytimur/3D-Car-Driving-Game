using UnityEngine;
using TMPro; // UI için
using System.Collections.Generic;

public class BoxGameManager : MonoBehaviour
{
    public static BoxGameManager Instance;

    [Header("Ayarlar")]
    public int totalBoxesToSpawn = 5; // Kaç kutu çýksýn?
    public string nextLevelName; // Geçilecek bölümün tam adý

    [Header("Referanslar")]
    public GameObject boxPrefab; // Toplanacak kutu modeli
    public List<Transform> spawnPoints; // Kutularýn çýkabileceði yerler
    public GameObject exitZone; // Bölüm bitince açýlacak kapý/alan
    public TextMeshProUGUI counterText; // Ekranda 0/10 yazan yer

    private int collectedCount = 0;

    void Awake()
    {
        Instance = this;
    }

    public void CollectBox()
    {
        collectedCount++;
        UpdateUI();

        // Eðer hepsi toplandýysa
        if (collectedCount >= totalBoxesToSpawn)
        {
            Debug.Log("Tüm kutular toplandý! Çýkýþ açýldý.");
            if (exitZone != null) exitZone.SetActive(true); // Kapýyý görünür yap
        }
    }

    void UpdateUI()
    {
        if (counterText != null)
         counterText.text = $"Kutular: {collectedCount}/{totalBoxesToSpawn}";
    }

    void Start()
    {
        // Oyun baþýnda çýkýþ kapýsýný gizle
        if (exitZone != null) exitZone.SetActive(false);

        SpawnBoxes();
        UpdateUI();
    }

    void SpawnBoxes()
    {
        // Önce spawn noktalarýnýn bir kopyasýný alalým (Orijinal liste bozulmasýn diye)
        List<Transform> availablePoints = new List<Transform>(spawnPoints);

        // Hedeflenen sayý kadar VEYA elimizdeki nokta bitene kadar döngü kur
        int spawnCount = Mathf.Min(totalBoxesToSpawn, availablePoints.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            // Kalan noktalar arasýndan rastgele bir indeks seç
            int randomIndex = Random.Range(0, availablePoints.Count);

            // O noktayý al
            Transform selectedPoint = availablePoints[randomIndex];

            // Kutuyu yarat
            Instantiate(boxPrefab, selectedPoint.position, selectedPoint.rotation);

            // KRÝTÝK NOKTA: Kullandýðýmýz noktayý bu geçici listeden siliyoruz!
            // Böylece bir sonraki turda bu nokta bir daha seçilemez.
            availablePoints.RemoveAt(randomIndex);
        }

        Debug.Log(spawnCount + " adet kutu, çakýþma olmadan yaratýldý!");
    }
}