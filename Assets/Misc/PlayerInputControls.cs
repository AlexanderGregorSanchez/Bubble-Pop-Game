//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Misc/PlayerInputControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputControls"",
    ""maps"": [
        {
            ""name"": ""Launcher"",
            ""id"": ""9c5bf272-c868-4db5-a48f-73aa5ce85936"",
            ""actions"": [
                {
                    ""name"": ""Tilt"",
                    ""type"": ""Value"",
                    ""id"": ""7f574629-bb7f-4d4e-a92d-ed528eb171a8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""54c05a16-ff72-4cd7-be9c-23820ebf4269"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Left/Right"",
                    ""id"": ""168a8401-8c9f-44be-916d-06c084af347f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""490bdfec-8804-402d-959b-3d834f9183d9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""537fec91-84ff-4433-957e-fe72a17c85bf"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tilt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""da9e3e31-c128-44ad-842e-eba90d635759"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Launcher
        m_Launcher = asset.FindActionMap("Launcher", throwIfNotFound: true);
        m_Launcher_Tilt = m_Launcher.FindAction("Tilt", throwIfNotFound: true);
        m_Launcher_Fire = m_Launcher.FindAction("Fire", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Launcher
    private readonly InputActionMap m_Launcher;
    private ILauncherActions m_LauncherActionsCallbackInterface;
    private readonly InputAction m_Launcher_Tilt;
    private readonly InputAction m_Launcher_Fire;
    public struct LauncherActions
    {
        private @PlayerInputControls m_Wrapper;
        public LauncherActions(@PlayerInputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Tilt => m_Wrapper.m_Launcher_Tilt;
        public InputAction @Fire => m_Wrapper.m_Launcher_Fire;
        public InputActionMap Get() { return m_Wrapper.m_Launcher; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LauncherActions set) { return set.Get(); }
        public void SetCallbacks(ILauncherActions instance)
        {
            if (m_Wrapper.m_LauncherActionsCallbackInterface != null)
            {
                @Tilt.started -= m_Wrapper.m_LauncherActionsCallbackInterface.OnTilt;
                @Tilt.performed -= m_Wrapper.m_LauncherActionsCallbackInterface.OnTilt;
                @Tilt.canceled -= m_Wrapper.m_LauncherActionsCallbackInterface.OnTilt;
                @Fire.started -= m_Wrapper.m_LauncherActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_LauncherActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_LauncherActionsCallbackInterface.OnFire;
            }
            m_Wrapper.m_LauncherActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Tilt.started += instance.OnTilt;
                @Tilt.performed += instance.OnTilt;
                @Tilt.canceled += instance.OnTilt;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
            }
        }
    }
    public LauncherActions @Launcher => new LauncherActions(this);
    public interface ILauncherActions
    {
        void OnTilt(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
    }
}