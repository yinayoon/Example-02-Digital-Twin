using UnityEngine;
using UnityEngine.EventSystems; // UI 상호작용 확인을 위한 네임스페이스

public class CameraMouseLook : MonoBehaviour
{
    [Header ("- Float")]
    public float sensitivity = 100f; // 마우스 감도

    // Private
    private Vector3 initialEulerAngles; // 초기 회전값 (EulerAngles)
    private float xRotation = 0f; // 상하 회전값
    private float yRotation = 0f; // 좌우 회전값

    void Start()
    {
        // 초기 회전값 저장
        initialEulerAngles = transform.eulerAngles;
        yRotation = initialEulerAngles.y;
        xRotation = initialEulerAngles.x;
    }

    void Update()
    {
        // UI를 클릭 중이라면 회전을 차단
        if (EventSystem.current.IsPointerOverGameObject())
            return; // Update 메서드 종료

        // 마우스 왼쪽 버튼을 눌렀을 때만 실행
        if (Input.GetMouseButton(0))
        {
            // 마우스 움직임 감지
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
                        
            xRotation -= mouseY; // X축(상하) 회전 계산
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 제한
            yRotation += mouseX; // Y축(좌우) 회전 계산
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f); // 카메라 회전 적용
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) // 스페이스 바를 눌렀을 때 초기 회전값 복구
            ResetRotation();
    }

    void OnDisable() // GameObject가 비활성화될 때 초기 회전값 복구
    {
        ResetRotation();
    }

    private void ResetRotation() // 초기 회전값으로 복구
    {
        transform.eulerAngles = initialEulerAngles; // 초기 회전값으로 복원
        xRotation = initialEulerAngles.x;
        yRotation = initialEulerAngles.y;
    }
}