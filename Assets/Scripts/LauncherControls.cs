using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LauncherControls : MonoBehaviour
{
    private PlayerInputControls inputs;

    float zTilt;
    public float tiltSpeed = 30;
    [Tooltip("Max rotation toward each side")]
    public float maxTiltAngle = 60;

    [SerializeField] private GameObject launcherGunPivot;

    public UnityEvent OnLauncherFired;
    public UnityEvent<bool> OnLauncherLockUpdated;

    private bool canFire = true;

    private void Awake()
    {
        inputs = new PlayerInputControls();
    }
    private void OnEnable()
    {
        inputs.Enable();

        inputs.Launcher.Tilt.performed += OnTiltPerformed;
        inputs.Launcher.Tilt.canceled += OnTiltCanceled;

        inputs.Launcher.Fire.performed += OnFirePerformed;

        SnapToGrid.OnSnapToGrid += UnlockLauncher;
    }
    private void OnDisable()
    {
        inputs.Disable();

        inputs.Launcher.Tilt.performed -= OnTiltPerformed;
        inputs.Launcher.Tilt.canceled -= OnTiltCanceled;

        inputs.Launcher.Fire.performed -= OnFirePerformed;

        SnapToGrid.OnSnapToGrid -= UnlockLauncher;
    }

    private void FixedUpdate()
    {
        launcherGunPivot.transform.Rotate(Vector3.back, zTilt * tiltSpeed * Time.fixedDeltaTime);

        if (launcherGunPivot.transform.rotation.eulerAngles.z > maxTiltAngle && launcherGunPivot.transform.rotation.eulerAngles.z < 360 - maxTiltAngle)
            if (launcherGunPivot.transform.rotation.eulerAngles.z < 180)
                launcherGunPivot.transform.rotation = Quaternion.Euler(launcherGunPivot.transform.eulerAngles.x, launcherGunPivot.transform.eulerAngles.y, maxTiltAngle);
            else
                launcherGunPivot.transform.rotation = Quaternion.Euler(launcherGunPivot.transform.eulerAngles.x, launcherGunPivot.transform.eulerAngles.y, 360 - maxTiltAngle);
    }

    private void OnTiltPerformed(InputAction.CallbackContext value)
    {
        zTilt = value.ReadValue<float>();
    }
    private void OnTiltCanceled(InputAction.CallbackContext value)
    {
        zTilt = 0f;
    }
    private void OnFirePerformed(InputAction.CallbackContext value)
    {
        if (!canFire) return;
        OnLauncherFired?.Invoke();
    }

    private void UnlockLauncher()
    {
        SetLauncherLockStatus(false);
    }

    public void SetLauncherLockStatus(bool status)
    {
        canFire = !status;
        OnLauncherLockUpdated?.Invoke(canFire);
    }
}
