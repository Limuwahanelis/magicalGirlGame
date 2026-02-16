using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController2D : PlayerControllerMain
{
    public override bool IsAlive => _isAlive;
    public override GameObject MainBody => _mainBody;
    [Header("Player")]
    [SerializeField] GameObject _mainBody;
    [SerializeField] AnimationManager _playerAnimationManager;
    [SerializeField] PlayerMovement2D _playerMovement;
    [SerializeField] AudioEventPlayer _playerAudioEventPlayer;
    [SerializeField] PlayerChecks2D _playerChecks;
    //[SerializeField] PlayerCombat _playerCombat;
    //[SerializeField] PlayerCollisions _playerCollisions;
    [SerializeField] PlayerHealthSystem _playerHealthSystem;
    private Player2DContext _context;
    private Dictionary<Type, Player2DState> playerStates = new Dictionary<Type, Player2DState>();
    private bool _isAlive = true;
    //[SerializeField, HideInInspector] private string _initialStateType;
    void Start()
    {

        Initalize();
    }
    override protected void Initalize()
    {

        List<Type> states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(domainAssembly => domainAssembly.GetTypes())
            .Where(type => typeof(Player2DState).IsAssignableFrom(type) && !type.IsAbstract).ToArray().ToList();

        _context = new Player2DContext
        {
            ChangePlayerState = ChangeState,
            animationManager = _playerAnimationManager,
            playerMovement = _playerMovement,
            WaitAndPerformFunction = WaitAndExecuteFunction,
            WaitFrameAndPerformFunction = WaitFrameAndExecuteFunction,
            audioEventPlayer = _playerAudioEventPlayer,
            coroutineHolder = this,
            checks = _playerChecks,
            //combat = _playerCombat,
            //collisions = _playerCollisions,
        };

        Player2DState.GetState getState = GetState;
        foreach (Type state in states)
        {
            playerStates.Add(state, (Player2DState)Activator.CreateInstance(state, getState));
        }
        // Set Startitng state
        Logger.Log(Type.GetType(_initialStateType));
        Player2DState newState = GetState(Type.GetType(_initialStateType));
        newState.SetUpState(_context);
        _currentPlayerState = newState;
        Logger.Log(newState.GetType());

        _playerHealthSystem.OnPushed += PushPlayer;
    }
    public Player2DState GetState(Type state)
    {
        return playerStates[state];
    }
    void Update()
    {
        _currentPlayerState.Update();
    }
    private void FixedUpdate()
    {
        _currentPlayerState.FixedUpdate();
    }
    public void ChangeState(Player2DState newState)
    {
        if (_printState) Logger.Log(newState.GetType());
        _currentPlayerState.InterruptState();
        _currentPlayerState = newState;
    }
    private void OnDestroy()
    {
        _playerHealthSystem.OnPushed -= PushPlayer;
    }
}
