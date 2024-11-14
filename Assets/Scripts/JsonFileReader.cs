using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class JsonFileReader : MonoBehaviour
{
    [Header("- Int")]
    public int folderNumber = 1; // ����ڰ� ������ �� �ִ� ���� ��ȣ

    [Header("- GUI")]
    public TextMeshProUGUI temperatureText; // Temperature Text
    public TextMeshProUGUI humidityText; // Humidity Text
    public TextMeshProUGUI airQualityText; // Air Quality Index Text
    public TextMeshProUGUI noiseLevelText; // Noise Level Text

    // Private
    private string folderPath;
    private int currentFileIndex = 1; // ���� �а� �ִ� ���� ��ȣ

    void OnEnable()
    {
        // "Resources/Json/Sensor (����ڰ� ������ ���� ��ȣ)"�� ��� ���� 
        folderPath = Path.Combine(Application.dataPath, $"Resources/Json/Sensor ({folderNumber})");
                
        StartCoroutine(ReadJsonFiles()); // 5�� �������� ���� �б� ����
    }

    void OnDisable()
    {
        StopCoroutine(ReadJsonFiles());
    }
    
    private IEnumerator ReadJsonFiles() // JSON ������ 5�� �������� �о� ������ �ؽ�Ʈ ����
    {
        currentFileIndex = 1; // ���� ��ȣ �ʱ�ȭ

        while (currentFileIndex <= 50) // ���� ��ȣ�� 50���� ����
        {            
            string fileName = $"environmentData_{currentFileIndex:00}.json"; // ���� �̸� ����
            string filePath = Path.Combine(folderPath, fileName);
                        
            if (File.Exists(filePath)) // ������ �����ϴ��� Ȯ��
            {                
                string jsonData = File.ReadAllText(filePath); // JSON ���� �б�
                                
                EnvironmentData data = JsonUtility.FromJson<EnvironmentData>(jsonData); // JSON ������ �Ľ�

                // ������ �� ����
                float temperature = data.temperature;
                int humidity = data.humidity;
                int airQualityIndex = data.airQualityIndex;
                int noiseLevel = data.noiseLevel;
                
                UpdateTextFields(temperature, humidity, airQualityIndex, noiseLevel); // Text Mesh Pro �ؽ�Ʈ ����
            }            
                        
            currentFileIndex++; // ���� ���Ϸ� �̵�                       
            yield return new WaitForSeconds(5f); // 5�� ���
        }
    }

    // Text Mesh Pro �ؽ�Ʈ �ʵ带 ������Ʈ
    private void UpdateTextFields(float temperature, int humidity, int airQualityIndex, int noiseLevel)
    {
        if (temperatureText != null) { temperatureText.text = temperature.ToString(); }
        if (humidityText != null) { humidityText.text = humidity.ToString(); }
        if (airQualityText != null) { airQualityText.text = airQualityIndex.ToString(); }
        if (noiseLevelText != null) { noiseLevelText.text = noiseLevel.ToString(); }
    }

    // ȯ�� ������ Ŭ����
    [System.Serializable]
    public class EnvironmentData
    {
        public float temperature; // �µ�
        public int humidity; // ����
        public int airQualityIndex; // ����� ����
        public int noiseLevel; // ���� ����
    }
}
