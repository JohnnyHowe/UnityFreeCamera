# if DEBUG
using ExtendedUnityInspector;
using UnityEngine;

public class FreeCameraDebug : FreeCamera
{
    [SerializeField] protected bool changeCameraLock = true;
    [SerializeField] protected bool verbose = true;
    [SerializeField] protected float sensitivityMultiplierOnMouseDown = 0.25f;
    [Header("Debug Info")]
    [SerializeField][ReadOnly] protected bool isFreeCameraEnabled = true;
    [SerializeField][ReadOnly] protected bool isMouseLookEnabled = true;
    [SerializeField][ReadOnly] protected bool isSensitive = false;
    [SerializeField][ReadOnly] protected CursorLockMode originalCursorLockMode;

    protected void Start()
    {
        originalCursorLockMode = Cursor.lockState;
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFreeCamera();

            // auto enable mouse look
            isMouseLookEnabled = !isFreeCameraEnabled;
            ToggleMouseLook();

            if (changeCameraLock) UpdateCameraLock();
        }

        if (isFreeCameraEnabled && Input.GetKeyDown(KeyCode.G))
        {
            ToggleMouseLook();
            UpdateCameraLock();
        }

        // Update FreeCamera parts manually (base.Update overriden by this.Update)
        if (isFreeCameraEnabled)
        {
            UpdateMovement(Time.unscaledDeltaTime);
            UpdateFov();
            if (isMouseLookEnabled) UpdateMouseLook();

            if (Input.GetMouseButtonDown(0)) isSensitive = true;
            if (Input.GetMouseButtonUp(0)) isSensitive = false;
            sensitivityMultiplier = isSensitive ? sensitivityMultiplierOnMouseDown : 1;
        }
    }

    protected void OnDisable()
    {
        Cursor.lockState = originalCursorLockMode;
    }

    protected virtual void ToggleFreeCamera()
    {
        isFreeCameraEnabled = !isFreeCameraEnabled;
        if (verbose) Debug.Log("FreeCamera: " + isFreeCameraEnabled);
        SetCamera(Camera.main);

        if (isFreeCameraEnabled) originalCursorLockMode = Cursor.lockState;
        UpdateCameraLock();
    }

    protected virtual void ToggleMouseLook()
    {
        isMouseLookEnabled = !isMouseLookEnabled;
        if (verbose) Debug.Log("FreeCamera.isMouseLookEnabled: " + isFreeCameraEnabled);
    }

    protected void UpdateCameraLock()
    {
        if (isMouseLookEnabled && isFreeCameraEnabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = originalCursorLockMode;
        }
    }
}
#endif