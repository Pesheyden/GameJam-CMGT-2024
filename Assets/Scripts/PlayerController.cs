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

    public void StartShooting()
    {
        _playerGhostShoot.TurnOnShootingMode();
        _playerMovement.IsCanMove = false;
    }

    public void StartPossession(NPCController target)
    {
        //Get info
        var info = target.Possess();
        
        //Apply info
        ChangeCharacter(info.Item1.CharacterPrefab);
        KeyLevel = info.Item1.KeyLevel;
        _collider.size = info.Item1.ColliderSize * 0.15f;
        _playerMovement.UpdateMovementValues(info.Item1.Speed);
        PossessionTimer.Instance.StartTimer(info.Item1.PossessionTime);
        
        //Transform position
        transform.position = info.Item2.position;

        _playerMovement.IsCanMove = true;
    }

    private void ChangeCharacter(GameObject prefab)
    {
        Destroy(_currentCharacter);
        Instantiate(prefab, transform);
    }

    public void ChangePlayerStatus(bool status)
    {
        if (status)
        {
            _playerMovement.IsCanMove = true;
            _playerGhostShoot.IsPaused = true;
        }
        else
        {
            _playerMovement.IsCanMove = false;
            _playerGhostShoot.IsPaused = false;
        }
    }
}
