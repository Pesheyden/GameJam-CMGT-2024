using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGhostShoot : MonoBehaviour
{
    [SerializeField] private GameObject _ghostPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _arrowControlsObject;
    [SerializeField] private Transform _rotatableArrowPart;
    [SerializeField] private Transform _scalableArrowPart;
    [SerializeField] private float _maxArrowScale;
    [SerializeField] private float _minArrowScale;
    [SerializeField] private float _maxMouseDistance;
    
    
    private float _currentStrength;
    private bool _isShootingModeOn;
    public bool IsPaused;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if(!_isShootingModeOn || IsPaused)
            return;
        if(Input.GetMouseButtonDown(0))
            Shoot();
        
        TurnArrow();
        ScaleArrow();
    }
    private void TurnArrow()
    {
        _rotatableArrowPart.rotation = Quaternion.Euler(new Vector3(0, 0, -Mathf.Rad2Deg * Mathf.Atan2(_camera.ScreenToWorldPoint(Input.mousePosition).x -transform.position.x, _camera.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y)));
    }
    private void ScaleArrow()
    {
        var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float mouseDistance = Vector3.Distance(transform.position, mousePosition);
        float newScale = mouseDistance / _maxMouseDistance;
        var scale = _scalableArrowPart.localScale;
        scale.y = Mathf.Clamp(newScale, _minArrowScale, _maxArrowScale);
        _scalableArrowPart.localScale = scale;
        _currentStrength = scale.y / _maxArrowScale;
    }

    public void TurnOnShootingMode()
    {
        _isShootingModeOn = true;
        _arrowControlsObject.SetActive(true);
    }
    
    public void Shoot()
    {
        Vector2 direction = transform.position - _camera.ScreenToWorldPoint(Input.mousePosition);
        var ghost = Instantiate(_ghostPrefab, transform.position, quaternion.identity);
        ghost.GetComponent<Rigidbody2D>().AddForce(-direction * _speed * _currentStrength);
        ghost.GetComponent<Ghost>().PlayerGhostShoot = this;
        TurnOffShootingMode();
    }

    private void TurnOffShootingMode()
    {
        _isShootingModeOn = false;
        _arrowControlsObject.SetActive(false);
    }
}
