using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public sealed class AimLineController : MonoBehaviour
{
    [SerializeField] private Transform _fruitTrowTransform;
    [SerializeField] private Transform _bottomTransform;

    private LineRenderer _lineRenderer;

    private float _topPos;
    private float _bottomPos;
    private float _x;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        InitializePositions();
        SetLineRendererPos();
    }

    private void OnValidate()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        InitializePositions();
        SetLineRendererPos();
    }

    private void InitializePositions()
    {
        _x = _fruitTrowTransform.position.x;
        _topPos = _fruitTrowTransform.position.y;
        _bottomPos = _bottomTransform.position.y;
    }

    private void SetLineRendererPos()
    {
        _lineRenderer.SetPosition(0, new Vector3(_x, _topPos));
        _lineRenderer.SetPosition(1, new Vector3(_x, _bottomPos));
    }
}
