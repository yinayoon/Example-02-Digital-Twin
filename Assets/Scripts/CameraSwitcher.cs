using UnityEngine;
using TMPro; // TextMeshPro ���ӽ����̽�

public class CameraSwitcher : MonoBehaviour
{
    [Header("- GameObject")]
    public GameObject[] cameraGameObjects; // ī�޶� ���Ե� GameObject �迭

    [Header("- GUI")]
    public TMP_Dropdown dropdown; // TMP_Dropdown UI
    
    void Start()
    {        
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged); // ��Ӵٿ� �� ���� �� ȣ��� �޼��� ����        
        ActivateCamera(0); // �ʱ� ī�޶� ���� (ù ��° ī�޶� Ȱ��ȭ)
    }

    void OnDropdownValueChanged(int index)
    {
        // ���õ� �ɼǿ� ���� ī�޶� Ȱ��ȭ
        ActivateCamera(index);
    }

    void ActivateCamera(int index)
    {
        // index��°�� ������ ��� ī�޶� GameObject�� ��Ȱ��ȭ
        for (int i = 0; i < cameraGameObjects.Length; i++)
        {
            bool active = (i == index);
            cameraGameObjects[i].SetActive(active);
        }
    }
}