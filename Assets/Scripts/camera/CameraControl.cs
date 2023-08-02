using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float cameraMovementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float minVerticalAngle;
    [SerializeField] float maxVerticalAngle;

    public float zoomSpeed = 5f;  
    public float minZoomFOV = 20f;
    public float maxZoomFOV = 60f;
    private Transform cameraTransform;
    private Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        MoveCamera();
        CameraRotation();
        CameraZoom();
    }

    private void MoveCamera()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;


        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 newPosition = transform.position + moveDirection * cameraMovementSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    private void CameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.up, mouseX, Space.World);

            Vector3 currentRotation = transform.localEulerAngles;
            float newRotatiomX = currentRotation.x - mouseY;

            newRotatiomX = Mathf.Clamp(newRotatiomX, minVerticalAngle, maxVerticalAngle);
            transform.localEulerAngles = new Vector3(newRotatiomX, currentRotation.y, currentRotation.z);
        }
        
    }

    private void CameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        float newFov = mainCamera.fieldOfView - (scrollInput * zoomSpeed);

        newFov = Mathf.Clamp(newFov, minZoomFOV, maxZoomFOV);
        mainCamera.fieldOfView = newFov;

    }
}
