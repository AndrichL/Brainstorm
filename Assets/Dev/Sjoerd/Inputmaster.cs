// GENERATED AUTOMATICALLY FROM 'Assets/Dev/Sjoerd/Inputmaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Inputmaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputmaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputmaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e1beef3e-b186-458e-ba10-c023ff8f8a84"",
            ""actions"": [
                {
                    ""name"": ""Movment"",
                    ""type"": ""Button"",
                    ""id"": ""0579d15a-4562-4e46-9d44-619b8adc0293"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""455c15d2-2e30-4fbe-8bb9-f41fe5782bd7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movment"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""aaa09b61-3d38-4086-b525-cf1f73477c92"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fa73c914-a775-4399-ace3-606c7d161e3b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f974755b-7146-4c5f-b0eb-0a875c638946"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e3dec4f1-3866-4ed6-b11b-7cb87581f9ad"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movment = m_Player.FindAction("Movment", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movment;
    public struct PlayerActions
    {
        private @Inputmaster m_Wrapper;
        public PlayerActions(@Inputmaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movment => m_Wrapper.m_Player_Movment;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movment.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovment;
                @Movment.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovment;
                @Movment.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovment;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movment.started += instance.OnMovment;
                @Movment.performed += instance.OnMovment;
                @Movment.canceled += instance.OnMovment;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnMovment(InputAction.CallbackContext context);
    }
}