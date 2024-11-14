using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class JsonFileReader : MonoBehaviour
{
    [Header("- Int")]
    public int folderNumber = 1; // 사용자가 설정할 수 있는 폴더 번호

    [Header("- GUI")]
    public TextMeshProUGUI temperatureText; // Temperature Text
    public TextMeshProUGUI humidityText; // Humidity Text
    public TextMeshProUGUI airQualityText; // Air Quality Index Text
    public TextMeshProUGUI noiseLevelText; // Noise Level Text

    // Private
    private string folderPath;
    private int currentFileIndex = 1; // 현재 읽고 있는 파일 번호

    void OnEnable()
    {
        // "Resources/Json/Sensor (사용자가 설정한 폴더 번호)"로 경로 설정 
        folderPath = Path.Combine(Application.dataPath, $"Resources/Json/Sensor ({folderNumber})");
                
        StartCoroutine(ReadJsonFiles()); // 5초 간격으로 파일 읽기 시작
    }

    void OnDisable()
    {
        StopCoroutine(ReadJsonFiles());
    }
    
    private IEnumerator ReadJsonFiles() // JSON 파일을 5초 간격으로 읽어 변수와 텍스트 갱신
    {
        currentFileIndex = 1; // 파일 번호 초기화

        while (currentFileIndex <= 50) // 파일 번호가 50까지 진행
        {            
            string fileName = $"environmentData_{currentFileIndex:00}.json"; // 파일 이름 생성
            string filePath = Path.Combine(folderPath, fileName);
                        
            if (File.Exists(filePath)) // 파일이 존재하는지 확인
            {                
                string jsonData = File.ReadAllText(filePath); // JSON 파일 읽기
                                
                EnvironmentData data = JsonUtility.FromJson<EnvironmentData>(jsonData); // JSON 데이터 파싱

                // 변수에 값 저장
                float temperature = data.temperature;
                int humidity = data.humidity;
                int airQualityIndex = data.airQualityIndex;
                int noiseLevel = data.noiseLevel;
                
                UpdateTextFields(temperature, humidity, airQualityIndex, noiseLevel); // Text Mesh Pro 텍스트 갱신
            }            
                        
            currentFileIndex++; // 다음 파일로 이동                       
            yield return new WaitForSeconds(5f); // 5초 대기
        }
    }

    // Text Mesh Pro 텍스트 필드를 업데이트
    private void UpdateTextFields(float temperature, int humidity, int airQualityIndex, int noiseLevel)
    {
        if (temperatureText != null) { temperatureText.text = temperature.ToString(); }
        if (humidityText != null) { humidityText.text = humidity.ToString(); }
        if (airQualityText != null) { airQualityText.text = airQualityIndex.ToString(); }
        if (noiseLevelText != null) { noiseLevelText.text = noiseLevel.ToString(); }
    }

    // 환경 데이터 클래스
    [System.Serializable]
    public class EnvironmentData
    {
        public float temperature; // 온도
        public int humidity; // 습도
        public int airQualityIndex; // 대기질 지수
        public int noiseLevel; // 소음 수준
    }
}
