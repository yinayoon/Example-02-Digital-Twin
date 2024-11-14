using UnityEngine;

public class RaycastSensor : MonoBehaviour
{
    [Header ("- LayerMask")]
    public LayerMask sensorLayer; // "Sensor" 레이어를 설정하기 위한 LayerMask
    public LayerMask selectSensorLayer; // "Select Sensor" 레이어를 설정하기 위한 LayerMask

    // Private
    private Camera cameraComponent; // 오브젝트에 붙은 Camera 컴포넌트
    private GameObject currentTarget; // 현재 검출된 타겟

    void Start()
    {        
        cameraComponent = GetComponent<Camera>(); // 현재 오브젝트에 붙은 Camera 컴포넌트를 가져옴
    }

    void Update()
    {
        if (cameraComponent == null) return; // Camera 컴포넌트가 없으면 실행 중단
                
        Ray ray = cameraComponent.ScreenPointToRay(Input.mousePosition); // 마우스 커서 위치에서 레이캐스트 발사
        RaycastHit hit;

        // 레이캐스트가 Sensor 레이어에 속한 오브젝트와 충돌했는지 확인
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, sensorLayer))
        {            
            if (hit.collider.gameObject != currentTarget) // 새로운 타겟인지 확인
            {
                ActivateSelectSensor(currentTarget, false); // 이전 타겟 처리                
                currentTarget = hit.collider.gameObject; // 새로운 타겟 설정                
                ActivateSelectSensor(currentTarget, true); // 자식 중 "Select Sensor" 레이어에 해당하는 오브젝트 활성화
            }
        }
        else
        {
            ActivateSelectSensor(currentTarget, false); // 타겟에서 벗어났다면 이전 타겟 비활성화
            currentTarget = null;
        }
    }

    // 자식 중 "Select Sensor" 레이어의 오브젝트를 활성화
    private void ActivateSelectSensor(GameObject target, bool active)
    {
        if (target == null) return;

        if (active == true)
        {
            foreach (Transform child in target.transform)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("Select Sensor"))
                    child.gameObject.SetActive(true); // 자식 오브젝트 활성화            
            }
        }
        else
        {
            foreach (Transform child in target.transform)
            {
                if (child.gameObject.layer == LayerMask.NameToLayer("Select Sensor"))
                    child.gameObject.SetActive(false); // 자식 오브젝트 비활성화    
            }
        }
    }
}
