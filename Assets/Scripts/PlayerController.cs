using System;
using Spine;
using UnityEngine;
using Spine.Unity;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Properties: Spine

    public SkeletonAnimation spineComponent;
    [SpineEvent]public string footstepEventName;

    #endregion
    
    #region Properties: Physics

    public Rigidbody rigidBody;
    public float speed;
    public float runRatio;
    public bool isRunning;
    
    #endregion

    #region Properties: Input
    
    [SerializeField]
    private InputActionAsset actionAsset;
    private InputActionMap _playerActions;
    
    [NonSerialized]
    public InputAction Move; //1D Axis
    [NonSerialized]
    public InputAction SetRunning; //Button
    [NonSerialized]
    public InputAction Interact; //Button
    
    #endregion

    #region Properties: State Machine

    //The reference of current state
    private IState _state;
    public IState State
    {
        get => _state;
        set
        {
            if (value == null || value == _state) return;
            _state?.OnExit();
            value.OnEnter(_state);
            _state = value;
        }
    }
    
    //Instance of All States
    public IState Idle;
    public IState Walking;
    public IState Running;

    #endregion

    #region Properties: Audio

    [SerializeField] private AudioSource audioSource;

    #endregion

    // Start is called before the first frame update

    private void Awake()
    {
        #region Init Input Actions

        _playerActions = actionAsset.FindActionMap("Player");
        Move = _playerActions.FindAction("Move");
        SetRunning = _playerActions.FindAction("IsRunning");
        Interact = _playerActions.FindAction("Interact");

        #endregion

        #region Assign Input Callbacks

        SetRunning.started += context => { isRunning = true; };

        SetRunning.canceled += context => { isRunning = false; };

        #endregion

        #region Set Up State Instances

        Idle = new States.Idle(this);
        Walking = new States.Walking(this);
        Running = new States.Running(this);
        
        State = Idle;

        #endregion

        #region Set Up Animation Event

        spineComponent.AnimationState.Event += AnimationStateOnEvent;

        #endregion
    }

    private void AnimationStateOnEvent(TrackEntry entry, Spine.Event e)
    {
        if (e.Data.Name == footstepEventName)
        {
            //Play Foot Step Sound Effect
            audioSource.Play();
        }
    }

    private void OnEnable()
    {
        //Enable controls of player
        _playerActions.Enable();
    }

    private void OnDisable()
    {
        //Disable controls of player
        _playerActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var moveValue = Move.ReadValue<float>();

        if (moveValue == 0)
        {
            //Change the Animation to Idle
            State.Stop();
        }
        else
        {
            if (isRunning)
            {
                //Change the Animation to Running
                State.Run();
            }
            else
            {
                //Change the Animation to Walk
                State.Walk();
            }
        }
        
        State.Update();
    }
}
