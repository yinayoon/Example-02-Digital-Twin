using UnityEngine;

public class RaycastSensor : MonoBehaviour
{
    [Header ("- LayerMask")]
    public LayerMask sensorLayer; // "Sensor" ���̾ �����ϱ� ���� LayerMask
    public LayerMask selectSensorLayer; // "Select Sensor" ���̾ �����ϱ� ���� LayerMask

    // Private
    private Camera cameraComponent; // ������Ʈ�� ���� Camera ������Ʈ
    private GameObject currentTarget; // ���� ����� Ÿ��

    void Start()
    {        
        cameraComponent = GetComponent<Camera>(); // ���� ������Ʈ�� ���� Camera ������Ʈ�� ������
    }

    void Update()
    {
        if (cameraComponent == null) return; // Camera ������Ʈ�� ������ ���� �ߴ�
                
        Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition); // ���콺 Ŀ�� ��ġ���� ����ĳ��Ʈ �߻�
        RaycastHit hit;

        // ����ĳ��Ʈ�� Sensor ���̾ ���� ������Ʈ�� �浹�ߴ��� Ȯ��
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, sensorLayer))
        {            
            if (hit.collider.gameObject != currentTarget) // ���ο� Ÿ������ Ȯ��
            {
                ActivateSelectSensor(currentTarget, false); // ���� Ÿ�� ó��                
                currentTarget = hit.collider.gameObject; // ���ο� Ÿ�� ����                
                ActivateSelectSensor(currentTarget, true); // �ڽ� �� "Select Sensor" ���̾ �ش��ϴ� ������Ʈ Ȱ��ȭ
            }
        }
        else
        {
            ActivateSelectSensor(currentTarget, false); // Ÿ�ٿ��� ����ٸ� ���� Ÿ�� ��Ȱ��ȭ
            currentTarget = null;
        }
    }

    // �ڽ� �� "Select Sensor" ���̾��� ������Ʈ�� Ȱ��ȭ
    private void ActivateSelectSensor(GameObject target, bool active)
    {
        if (target == null) return;

        if (active == true)
        {
            foreach (Transform child in target.transform)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("Select Sensor"))
                    child.gameObject.SetActive(true); // �ڽ� ������Ʈ Ȱ��ȭ            
            }
        }
        else
        {
            foreach (Transform child in target.transform)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("Select Sensor"))
                    child.gameObject.SetActive(false); // �ڽ� ������Ʈ ��Ȱ��ȭ    
            }
        }
    }
}
