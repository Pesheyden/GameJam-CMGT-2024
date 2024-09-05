using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;
    public static PlayerController Instance => _instance;
    private PlayerMovement _playerMovement;
    private PlayerGhostShoot _playerGhostShoot;

    public int KeyLevel;
    private BoxCollider2D _collider;

    [SerializeField] private GameObject _currentCharacter; //TODO: remove serializeField
    
    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError($"2 {name} could not exist in same time");
            Destroy(this);
        }
        _instance = this;

        _playerMovement = GetComponent<PlayerMovement>();
        _playerGhostShoot = GetComponent<PlayerGhostShoot>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartShooting();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {   
            GameManager.Instance.PauseGame();
        }
    }

    public void StartShooting()
    {
        _playerGhostShoot.TurnOnShootingMode();
        _playerMovement.TurnMovement(false);
        
    }

    public void StartPossession(NPCController target)
    {
        //Get info
        var info = target.Possess();
        
        //Apply info
        ChangeCharacter(info.Item1.CharacterPrefab);
        KeyLevel = info.Item1.KeyLevel;
        _collider.size = info.Item1.ColliderSize * 0.15f;
        _playerMovement.UpdateMovementValues(info.Item1.Speed, info.Item1.JumpPower);
        PossessionTimer.Instance.StartTimer(info.Item1.PossessionTime);
        
        //Transform position
        transform.position = info.Item2.position;

        _playerMovement.TurnMovement(true);
    }

    private void ChangeCharacter(GameObject prefab)
    {
        Destroy(_currentCharacter);
        _currentCharacter = Instantiate(prefab, transform);
    }

    public void ChangePlayerStatus(bool status)
    {
        if (status)
        {
            _playerMovement.TurnMovement(true);
            _playerGhostShoot.IsPaused = true;
        }
        else
        {
            _playerMovement.TurnMovement(false);
            _playerGhostShoot.IsPaused = false;
        }
    }
}
