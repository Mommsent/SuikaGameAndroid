using UnityEngine;

public sealed class FruitSpawner : MonoBehaviour
{

    public delegate void FruitHasSpowned(Bounds Bounds);
    public static event FruitHasSpowned HasSpawned;

    public delegate void SpriteHasChanged();
    public static event SpriteHasChanged spriteHasChanged;

    [SerializeField] private FruitSelector _fruitSelector;
    public FruitSprite CurrentFruitSprite { get; private set; }
    public ColliderInformer CurrentFruit {  get; private set; }

    private Bounds Bounds;
    private CircleCollider2D _circleCollider;

    [SerializeField] private Transform _fruitTransform;

    [SerializeField] private Transform _parentAfterThrow;

    private void OnEnable()
    {
        Player.throwWasPressed += SpawnFisicalFruit;
        ColliderInformer.wasDropped += SpawnAFruitSprite;
    }

    void Start()
    {
        SpawnAFruitSprite();
    }

    private void SpawnAFruitSprite()
    {
        FruitSprite fruit = _fruitSelector.NextFruit;
        FruitSprite fruitToSpawn = Instantiate(fruit, _fruitTransform);
        CurrentFruitSprite = fruitToSpawn;
        CurrentFruit = _fruitSelector.GetNextFruit();

        _fruitSelector.PickRandomSprite();

        _circleCollider = CurrentFruitSprite.GetComponent<CircleCollider2D>();
        Bounds = _circleCollider.bounds;

        spriteHasChanged?.Invoke();
        HasSpawned?.Invoke(Bounds);
    }

    private void SpawnFisicalFruit()
    {
            Quaternion rotation = CurrentFruitSprite.transform.rotation;
            Vector3 currentFruitTransform = CurrentFruitSprite.transform.position;

            Destroy(CurrentFruitSprite.gameObject);

            ColliderInformer fruitToSpawn = Instantiate(CurrentFruit, currentFruitTransform, rotation);
            fruitToSpawn.transform.SetParent(_parentAfterThrow);
    }

    private void OnDisable()
    {
        Player.throwWasPressed -= SpawnFisicalFruit;
        ColliderInformer.wasDropped -= SpawnAFruitSprite;
    }
}
