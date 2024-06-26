//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputSystem/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""1b286284-fa90-44c8-bfd2-0dea3e7b32c4"",
            ""actions"": [
                {
                    ""name"": ""HammerMovement"",
                    ""type"": ""Value"",
                    ""id"": ""2c2308aa-3727-4bff-b5ac-1f82d06336fd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RetrackHammer"",
                    ""type"": ""Value"",
                    ""id"": ""6e6a3e3b-afc2-4219-9d18-a90b98166660"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ChangeCharacter"",
                    ""type"": ""Button"",
                    ""id"": ""662353fc-db68-48ca-9c40-0d71dfb9856f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeGameMode"",
                    ""type"": ""Button"",
                    ""id"": ""317db7a7-826b-4393-b097-f1fe9d2f45db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChangeLayout"",
                    ""type"": ""Button"",
                    ""id"": ""3c11167b-925b-4da6-815d-d6b125fb25b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ReadyUp"",
                    ""type"": ""Button"",
                    ""id"": ""99624ce5-542b-4125-ace4-7fe153f3435e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8fd23eec-4e59-4fa4-b1c1-ba1370c9f023"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HammerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""56647923-2557-49d1-bffc-84e8e7497fb4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeCharacter"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""166cdcf1-f735-4569-b69b-326c4e3294ec"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""ChangeCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ae9e4477-6fed-4a64-a8bb-24113f9c2115"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""ChangeCharacter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e3cbeced-f565-4fee-8df5-5bc6a7581561"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeLayout"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6287285c-5683-40b6-919c-e3b2a961c71a"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""ChangeLayout"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""46696fd3-dd43-4fec-9d43-340ce341b86b"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""ChangeLayout"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ef11c962-d072-4704-a9da-fd45de278338"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""RetrackHammer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0afc1bac-bd33-4187-9eff-2886f2e666d1"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""ReadyUp"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da74ed2f-e7ee-4ae7-96b5-50c1eea3c806"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""BaseControlScheme"",
                    ""action"": ""ChangeGameMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""BaseControlScheme"",
            ""bindingGroup"": ""BaseControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_HammerMovement = m_Movement.FindAction("HammerMovement", throwIfNotFound: true);
        m_Movement_RetrackHammer = m_Movement.FindAction("RetrackHammer", throwIfNotFound: true);
        m_Movement_ChangeCharacter = m_Movement.FindAction("ChangeCharacter", throwIfNotFound: true);
        m_Movement_ChangeGameMode = m_Movement.FindAction("ChangeGameMode", throwIfNotFound: true);
        m_Movement_ChangeLayout = m_Movement.FindAction("ChangeLayout", throwIfNotFound: true);
        m_Movement_ReadyUp = m_Movement.FindAction("ReadyUp", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private List<IMovementActions> m_MovementActionsCallbackInterfaces = new List<IMovementActions>();
    private readonly InputAction m_Movement_HammerMovement;
    private readonly InputAction m_Movement_RetrackHammer;
    private readonly InputAction m_Movement_ChangeCharacter;
    private readonly InputAction m_Movement_ChangeGameMode;
    private readonly InputAction m_Movement_ChangeLayout;
    private readonly InputAction m_Movement_ReadyUp;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @HammerMovement => m_Wrapper.m_Movement_HammerMovement;
        public InputAction @RetrackHammer => m_Wrapper.m_Movement_RetrackHammer;
        public InputAction @ChangeCharacter => m_Wrapper.m_Movement_ChangeCharacter;
        public InputAction @ChangeGameMode => m_Wrapper.m_Movement_ChangeGameMode;
        public InputAction @ChangeLayout => m_Wrapper.m_Movement_ChangeLayout;
        public InputAction @ReadyUp => m_Wrapper.m_Movement_ReadyUp;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void AddCallbacks(IMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_MovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MovementActionsCallbackInterfaces.Add(instance);
            @HammerMovement.started += instance.OnHammerMovement;
            @HammerMovement.performed += instance.OnHammerMovement;
            @HammerMovement.canceled += instance.OnHammerMovement;
            @RetrackHammer.started += instance.OnRetrackHammer;
            @RetrackHammer.performed += instance.OnRetrackHammer;
            @RetrackHammer.canceled += instance.OnRetrackHammer;
            @ChangeCharacter.started += instance.OnChangeCharacter;
            @ChangeCharacter.performed += instance.OnChangeCharacter;
            @ChangeCharacter.canceled += instance.OnChangeCharacter;
            @ChangeGameMode.started += instance.OnChangeGameMode;
            @ChangeGameMode.performed += instance.OnChangeGameMode;
            @ChangeGameMode.canceled += instance.OnChangeGameMode;
            @ChangeLayout.started += instance.OnChangeLayout;
            @ChangeLayout.performed += instance.OnChangeLayout;
            @ChangeLayout.canceled += instance.OnChangeLayout;
            @ReadyUp.started += instance.OnReadyUp;
            @ReadyUp.performed += instance.OnReadyUp;
            @ReadyUp.canceled += instance.OnReadyUp;
        }

        private void UnregisterCallbacks(IMovementActions instance)
        {
            @HammerMovement.started -= instance.OnHammerMovement;
            @HammerMovement.performed -= instance.OnHammerMovement;
            @HammerMovement.canceled -= instance.OnHammerMovement;
            @RetrackHammer.started -= instance.OnRetrackHammer;
            @RetrackHammer.performed -= instance.OnRetrackHammer;
            @RetrackHammer.canceled -= instance.OnRetrackHammer;
            @ChangeCharacter.started -= instance.OnChangeCharacter;
            @ChangeCharacter.performed -= instance.OnChangeCharacter;
            @ChangeCharacter.canceled -= instance.OnChangeCharacter;
            @ChangeGameMode.started -= instance.OnChangeGameMode;
            @ChangeGameMode.performed -= instance.OnChangeGameMode;
            @ChangeGameMode.canceled -= instance.OnChangeGameMode;
            @ChangeLayout.started -= instance.OnChangeLayout;
            @ChangeLayout.performed -= instance.OnChangeLayout;
            @ChangeLayout.canceled -= instance.OnChangeLayout;
            @ReadyUp.started -= instance.OnReadyUp;
            @ReadyUp.performed -= instance.OnReadyUp;
            @ReadyUp.canceled -= instance.OnReadyUp;
        }

        public void RemoveCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_MovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    private int m_BaseControlSchemeSchemeIndex = -1;
    public InputControlScheme BaseControlSchemeScheme
    {
        get
        {
            if (m_BaseControlSchemeSchemeIndex == -1) m_BaseControlSchemeSchemeIndex = asset.FindControlSchemeIndex("BaseControlScheme");
            return asset.controlSchemes[m_BaseControlSchemeSchemeIndex];
        }
    }
    public interface IMovementActions
    {
        void OnHammerMovement(InputAction.CallbackContext context);
        void OnRetrackHammer(InputAction.CallbackContext context);
        void OnChangeCharacter(InputAction.CallbackContext context);
        void OnChangeGameMode(InputAction.CallbackContext context);
        void OnChangeLayout(InputAction.CallbackContext context);
        void OnReadyUp(InputAction.CallbackContext context);
    }
}
