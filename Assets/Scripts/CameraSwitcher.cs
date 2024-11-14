using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스

public class CameraSwitcher : MonoBehaviour
{
    [Header("- GameObject")]
    public GameObject[] cameraGameObjects; // 카메라가 포함된 GameObject 배열

    [Header("- GUI")]
    public TMP_Dropdown dropdown; // TMP_Dropdown UI
    
    void Start()
    {        
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged); // 드롭다운 값 변경 시 호출될 메서드 연결        
        ActivateCamera(0); // 초기 카메라 설정 (첫 번째 카메라 활성화)
    }

    void OnDropdownValueChanged(int index)
    {
        // 선택된 옵션에 따라 카메라 활성화
        ActivateCamera(index);
    }

    void ActivateCamera(int index)
    {
        // index번째를 제외한 모든 카메라 GameObject를 비활성화
        for (int i = 0; i < cameraGameObjects.Length; i++)
        {
            bool active = (i == index);
            cameraGameObjects[i].SetActive(active);
        }
    }
}