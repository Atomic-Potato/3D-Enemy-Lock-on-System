// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerControlsMaps.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControlsMaps : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControlsMaps()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControlsMaps"",
    ""maps"": [
        {
            ""name"": ""GroundedMovement"",
            ""id"": ""177036dc-797f-4a06-a26e-754dfa6bee54"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f93ef4a7-4161-408d-9c80-238a35e65f46"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Value"",
                    ""id"": ""c2a6921f-8afc-4d78-9cb2-7c40b7db0e63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""e3060a96-453d-4ea5-8861-77350da22027"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""edfa2c6a-a490-4f45-b660-3c9d01db60be"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""52aaae58-5e2f-4938-a246-d6dc96974801"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""96ef0ae4-4867-4bf5-b00f-0e19911c9cfa"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""64f290bb-a978-4e3c-b4f8-6076ca1f551c"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""5d74d483-42b3-489a-a363-a5b8165799d3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""29bf3677-7be7-4228-b190-ab2afc1bc787"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""206927d6-f1e5-465d-ac8f-9e77b7dd04d5"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GroundedMovement
        m_GroundedMovement = asset.FindActionMap("GroundedMovement", throwIfNotFound: true);
        m_GroundedMovement_Move = m_GroundedMovement.FindAction("Move", throwIfNotFound: true);
        m_GroundedMovement_Run = m_GroundedMovement.FindAction("Run", throwIfNotFound: true);
        m_GroundedMovement_Jump = m_GroundedMovement.FindAction("Jump", throwIfNotFound: true);
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

    // GroundedMovement
    private readonly InputActionMap m_GroundedMovement;
    private IGroundedMovementActions m_GroundedMovementActionsCallbackInterface;
    private readonly InputAction m_GroundedMovement_Move;
    private readonly InputAction m_GroundedMovement_Run;
    private readonly InputAction m_GroundedMovement_Jump;
    public struct GroundedMovementActions
    {
        private @PlayerControlsMaps m_Wrapper;
        public GroundedMovementActions(@PlayerControlsMaps wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_GroundedMovement_Move;
        public InputAction @Run => m_Wrapper.m_GroundedMovement_Run;
        public InputAction @Jump => m_Wrapper.m_GroundedMovement_Jump;
        public InputActionMap Get() { return m_Wrapper.m_GroundedMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GroundedMovementActions set) { return set.Get(); }
        public void SetCallbacks(IGroundedMovementActions instance)
        {
            if (m_Wrapper.m_GroundedMovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GroundedMovementActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_GroundedMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public GroundedMovementActions @GroundedMovement => new GroundedMovementActions(this);
    public interface IGroundedMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
}
