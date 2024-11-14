using UnityEngine;
using UnityEngine.EventSystems; // UI ��ȣ�ۿ� Ȯ���� ���� ���ӽ����̽�

public class ModelMouseRotate : MonoBehaviour
{
    [Header("- Float")]
    public float sensitivity = 100f; // ���콺 ����

    [Header("- GameObject")]
    public GameObject targetModel; // ȸ����ų �𵨸�
    
    private float xRotation = 0f; // ���� ȸ����
    private float yRotation = 0f; // �¿� ȸ����
    private Vector3 initialEulerAngles; // �𵨸��� �ʱ� ȸ���� (EulerAngles)

    void Start()
    {
        // �𵨸��� �ʱ� ȸ���� ����
        if (targetModel != null)
        {
            initialEulerAngles = targetModel.transform.eulerAngles;
            yRotation = initialEulerAngles.y;
            xRotation = initialEulerAngles.x;
        }
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // UI�� Ŭ�� ���̶�� ȸ���� ����
            return; // Update �޼��� ����

        // ���콺 ���� ��ư�� ������ ���� ����
        if (Input.GetMouseButton(0) && targetModel != null) // 0�� ���� ���콺 ��ư
        {
            // ���콺 ������ ����
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                        
            xRotation -= mouseY; // X��(����) ȸ�� ���
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ȸ�� ����
            yRotation += mouseX; // Y��(�¿�) ȸ�� ���
            
            targetModel.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f); // �𵨸� ȸ�� ���� (�ʱ� ���� �������� ȸ��)
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽� �ٸ� ������ �� ȸ���� �ʱ�ȭ
        {
            ResetRotation();
        }
    }
        
    void OnDisable() // GameObject�� ��Ȱ��ȭ�� �� �ʱ� ȸ���� ����
    {
        ResetRotation();
    }

    private void ResetRotation() // �ʱ� ȸ�������� ����
    {
        if (targetModel != null)
        {
            targetModel.transform.eulerAngles = initialEulerAngles; // �ʱ� ȸ�������� ����
            xRotation = initialEulerAngles.x;
            yRotation = initialEulerAngles.y;
        }
    }
}