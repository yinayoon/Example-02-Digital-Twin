using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("- Float")]
    public float zoomSpeed = 10f; // �� �ӵ�
    public float minFOV = 15f; // �ּ� Field of View
    public float maxFOV = 60f; // �ִ� Field of View

    [Header("- Camera")]
    private Camera cameraComponent; // ���� ������Ʈ�� �پ��ִ� Camera ������Ʈ    

    // Private
    private float initialFOV; // �ʱ� Field of View ��

    void Start()
    {        
        cameraComponent = GetComponent<Camera>(); // ���� ������Ʈ���� Camera ������Ʈ�� ������        
        initialFOV = cameraComponent.fieldOfView; // �ʱ� FOV �� ����
    }

    void Update()
    {
        if (cameraComponent != null)
        {            
            float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // ���콺 �� �Է� ��

            if (scrollInput != 0)
            {                
                cameraComponent.fieldOfView -= scrollInput * zoomSpeed; // FOV �� ����
                cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, minFOV, maxFOV); // FOV ����
            }

            if (Input.GetKeyDown(KeyCode.Space)) // �����̽� �ٸ� ������ ��
                ResetFOV(); // FOV �ʱ�ȭ
        }
    }
        
    void OnDisable() // GameObject�� ��Ȱ��ȭ�� �� �ʱ� FOV �� ����
    {
        ResetFOV();
    }

    private void ResetFOV() // FOV�� �ʱⰪ���� ����
    {
        if (cameraComponent != null)
        {
            cameraComponent.fieldOfView = initialFOV; // �ʱ� FOV ������ ����
        }
    }
}