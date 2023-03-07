using UnityEngine;
using Spine.Unity;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Properties: Spine

    public SkeletonAnimation spineComponent;

    #endregion
    
    #region Properties: Physics

    public Rigidbody rigidBody;
    public float speed;
    public float runRatio;
    public bool isRunning;
    
    #endregion

    #region Properties: Input
    
    public InputActionAsset actionAsset;
    private InputActionMap _playerActions;
    public InputAction move; //1D Axis
    public InputAction setRunning; //Button
    public InputAction interact; //Button
    
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

    // Start is called before the first frame update

    private void Awake()
    {
        #region Init Input Actions

        _playerActions = actionAsset.FindActionMap("Player");
        move = _playerActions.FindAction("Move");
        setRunning = _playerActions.FindAction("IsRunning");
        interact = _playerActions.FindAction("Interact");

        #endregion

        #region Assign Input Callbacks

        setRunning.started += context => { isRunning = true; };

        setRunning.canceled += context => { isRunning = false; };

        #endregion

        #region Set Up State Instances

        Idle = new States.Idle(this);
        Walking = new States.Walking(this);
        Running = new States.Running(this);
        State = Idle;

        #endregion
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
        var moveValue = move.ReadValue<float>();

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
