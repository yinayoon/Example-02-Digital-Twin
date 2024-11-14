using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("- Float")]
    public float zoomSpeed = 10f; // 줌 속도
    public float minFOV = 15f; // 최소 Field of View
    public float maxFOV = 60f; // 최대 Field of View

    [Header("- Camera")]
    private Camera cameraComponent; // 현재 오브젝트에 붙어있는 Camera 컴포넌트    

    // Private
    private float initialFOV; // 초기 Field of View 값

    void Start()
    {        
        cameraComponent = GetComponent<Camera>(); // 현재 오브젝트에서 Camera 컴포넌트를 가져옴        
        initialFOV = cameraComponent.fieldOfView; // 초기 FOV 값 저장
    }

    void Update()
    {
        if (cameraComponent != null)
        {            
            float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // 마우스 휠 입력 값

            if (scrollInput != 0)
            {                
                cameraComponent.fieldOfView -= scrollInput * zoomSpeed; // FOV 값 변경
                cameraComponent.fieldOfView = Mathf.Clamp(cameraComponent.fieldOfView, minFOV, maxFOV); // FOV 제한
            }

            if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 바를 눌렀을 때
                ResetFOV(); // FOV 초기화
        }
    }
        
    void OnDisable() // GameObject가 비활성화될 때 초기 FOV 값 복구
    {
        ResetFOV();
    }

    private void ResetFOV() // FOV를 초기값으로 복구
    {
        if (cameraComponent != null)
        {
            cameraComponent.fieldOfView = initialFOV; // 초기 FOV 값으로 복구
        }
    }
}