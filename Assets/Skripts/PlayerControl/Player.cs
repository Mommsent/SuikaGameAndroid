using UnityEngine;

public sealed class Player : MonoBehaviour
{
    public delegate void ThrowWasPressed();
    public static event ThrowWasPressed throwWasPressed;

    public delegate void MenuWasPressed();
    public static event MenuWasPressed menuWasPressed;

    [SerializeField] private float _speed = 10f;
    [SerializeField] private BoxCollider2D _boundaries;
    [SerializeField] private Transform _fruitThrowTransform;
    [SerializeField] private CallPauseMenu _menu;

    private Bounds _bounds;

    private float _leftBound;
    private float _rightBound;

    private float _startingLeftBound;
    private float _startingRightBound;

    private float _offset;

    private const float EXTRA_WIDTH = 0.02f;

    private void Awake()
    {
        InitializeParameters();
    }

    private void OnEnable()
    {
        FruitSpawner.HasSpawned += ChangeBoundary;
    }

    private void Update()
    {
        if(!_menu.GameIsPaused)
        {
            if (UserInput.Instance.IsThrowPressed)
                throwWasPressed?.Invoke();
            Move();
        }
        if (UserInput.Instance.IsMenuPressed)
            menuWasPressed?.Invoke();
    }

    private void InitializeParameters()
    {
        _bounds = _boundaries.bounds;

        _offset = transform.position.x - _fruitThrowTransform.position.x;

        _leftBound = _bounds.min.x + _offset;
        _rightBound = _bounds.max.x + _offset;

        _startingLeftBound = _leftBound;
        _startingRightBound = _rightBound;
    }

    private void Move()
    {
        Vector3 newPosition = transform.position + new Vector3(UserInput.Instance.MoveInput.x * _speed * Time.deltaTime, 0f, 0f);
        newPosition.x = Mathf.Clamp(newPosition.x, _leftBound, _rightBound);

        transform.position = newPosition;
    }

    private void ChangeBoundary(Bounds bounds)
    {
        _leftBound = _startingLeftBound;
        _rightBound = _startingRightBound;

        _leftBound += bounds.extents.x + EXTRA_WIDTH;
        _rightBound -= bounds.extents.x + EXTRA_WIDTH;
    }

    private void OnDisable()
    {
        FruitSpawner.HasSpawned -= ChangeBoundary;
    }
}
