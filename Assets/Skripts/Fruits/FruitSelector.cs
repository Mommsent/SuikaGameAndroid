using UnityEngine;

public class FruitSelector : MonoBehaviour
{
    public static FruitSelector Instance { get; private set; }

    public int FruitsListLength { get; private set; }
    [SerializeField] private ColliderInformer[] _fruits;
    [SerializeField] private FruitSprite[] _noPhysicsFruits;
    [SerializeField] public int HighestStartingIndex { get; private set; } = 3;
    public Sprite NextFruitSprite { get; private set; }

    [SerializeField] private Sprite[] _fruitSprites;

    public FruitSprite NextFruit {  get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        PickRandomSprite();
        FruitsListLength = _fruits.Length;
    }

    public FruitSprite PickRandomSprite()
    {
        int randomIndex = Random.Range(0, HighestStartingIndex);
        if(randomIndex < _noPhysicsFruits.Length)
        {
            NextFruit = _noPhysicsFruits[randomIndex];
            NextFruitSprite = _fruitSprites[randomIndex];
            return NextFruit;
        }
        return null;
    }

    public ColliderInformer GetNextFruit()
    {
        if(NextFruit.index < _fruits.Length) 
        {
            ColliderInformer nextFruit = _fruits[NextFruit.index];
            return nextFruit;
        }
        return null;
    }

    public ColliderInformer GetNextLevelFruit(int index)
    {
        ++index;
        ColliderInformer leveledUpFruit = _fruits[index];
        return leveledUpFruit;
    }
}
