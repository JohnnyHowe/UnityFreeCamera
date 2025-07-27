using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSensitivity = 50;
    [SerializeField] private float lookKeySensitivity = 100;
    [SerializeField] private float lookMouseSensitivity = 2;
    [SerializeField] private float fovScrollSensitivity = 10;
    [SerializeField] public bool lockMouse = true;
    protected float sensitivityMultiplier = 1;
    private Camera targetCamera;

    private void Start()
    {
        SetCamera(Camera.main);
        if (lockMouse) Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        UpdateMovement(Time.unscaledDeltaTime);
        UpdateMouseLook();
    }

    public void SetCamera(Camera targetCamera)
    {
        this.targetCamera = targetCamera;
    }

    public void UpdateMovement(float deltaTime)
    {
        Transform currentCameraTransform = targetCamera.transform;
        float moveSpeed = deltaTime * movementSensitivity * sensitivityMultiplier;
        if (Input.GetKey(KeyCode.W)) currentCameraTransform.position += moveSpeed * currentCameraTransform.forward;
        if (Input.GetKey(KeyCode.S)) currentCameraTransform.position += moveSpeed * -currentCameraTransform.forward;
        if (Input.GetKey(KeyCode.D)) currentCameraTransform.position += moveSpeed * currentCameraTransform.right;
        if (Input.GetKey(KeyCode.A)) currentCameraTransform.position += moveSpeed * -currentCameraTransform.right;
        if (Input.GetKey(KeyCode.Space)) currentCameraTransform.position += moveSpeed * Vector3.up;
        if (Input.GetKey(KeyCode.LeftShift)) currentCameraTransform.position += moveSpeed * -Vector3.up;

        // look (keys)
        float lookSpeedKey = deltaTime * lookKeySensitivity * sensitivityMultiplier;
        if (Input.GetKey(KeyCode.UpArrow)) currentCameraTransform.eulerAngles += lookSpeedKey * -Vector3.right;
        if (Input.GetKey(KeyCode.DownArrow)) currentCameraTransform.eulerAngles += lookSpeedKey * Vector3.right;
        if (Input.GetKey(KeyCode.LeftArrow)) currentCameraTransform.eulerAngles += lookSpeedKey * -Vector3.up;
        if (Input.GetKey(KeyCode.RightArrow)) currentCameraTransform.eulerAngles += lookSpeedKey * Vector3.up;
    }

    public void UpdateFov()
    {
        targetCamera.fieldOfView += -Input.GetAxis("Mouse ScrollWheel") * fovScrollSensitivity;
    }

    public void UpdateMouseLook()
    {
        Transform currentCameraTransform = targetCamera.transform;
        float maxChange = lookMouseSensitivity * sensitivityMultiplier;
        float rotX = Input.GetAxis("Mouse X") * maxChange;
        float rotY = Input.GetAxis("Mouse Y") * maxChange;
        currentCameraTransform.RotateAround(currentCameraTransform.position, Vector3.up, rotX);
        currentCameraTransform.RotateAround(currentCameraTransform.position, currentCameraTransform.right, -rotY);
    }
}