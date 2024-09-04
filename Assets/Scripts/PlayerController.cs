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

    private GameObject _currentCharacter;
    
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
    }

    private void StartShooting()
    {
        _playerGhostShoot.TurnOnShootingMode();
    }

    public void StartPossession(NPCController target)
    {
        //Get info
        var info = target.Possess();
        
        //Apply info
        ChangeCharacter(info.Item1.CharacterPrefab);
        _playerMovement.speed = info.Item1.Speed;
        KeyLevel = info.Item1.KeyLevel;
        
        //Transform position
        transform.position = info.Item2.position;

        _playerMovement.IsCanMove = true;
    }

    private void ChangeCharacter(GameObject prefab)
    {
        Destroy(_currentCharacter);
        Instantiate(prefab, transform);
    }
}
