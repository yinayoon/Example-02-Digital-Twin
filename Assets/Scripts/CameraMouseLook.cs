using UnityEngine;
using UnityEngine.EventSystems; // UI ��ȣ�ۿ� Ȯ���� ���� ���ӽ����̽�

public class CameraMouseLook : MonoBehaviour
{
    [Header ("- Float")]
    public float sensitivity = 100f; // ���콺 ����

    // Private
    private Vector3 initialEulerAngles; // �ʱ� ȸ���� (EulerAngles)
    private float xRotation = 0f; // ���� ȸ����
    private float yRotation = 0f; // �¿� ȸ����

    void Start()
    {
        // �ʱ� ȸ���� ����
        initialEulerAngles = transform.eulerAngles;
        yRotation = initialEulerAngles.y;
        xRotation = initialEulerAngles.x;
    }

    void Update()
    {
        // UI�� Ŭ�� ���̶�� ȸ���� ����
        if (EventSystem.current.IsPointerOverGameObject())
            return; // Update �޼��� ����

        // ���콺 ���� ��ư�� ������ ���� ����
        if (Input.GetMouseButton(0))
        {
            // ���콺 ������ ����
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                        
            xRotation -= mouseY; // X��(����) ȸ�� ���
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ���� ȸ�� ����
            yRotation += mouseX; // Y��(�¿�) ȸ�� ���
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f); // ī�޶� ȸ�� ����
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽� �ٸ� ������ �� �ʱ� ȸ���� ����
            ResetRotation();
    }

    void OnDisable() // GameObject�� ��Ȱ��ȭ�� �� �ʱ� ȸ���� ����
    {
        ResetRotation();
    }

    private void ResetRotation() // �ʱ� ȸ�������� ����
    {
        transform.eulerAngles = initialEulerAngles; // �ʱ� ȸ�������� ����
        xRotation = initialEulerAngles.x;
        yRotation = initialEulerAngles.y;
    }
}