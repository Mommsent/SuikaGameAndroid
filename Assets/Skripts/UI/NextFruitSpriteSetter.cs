using UnityEngine;
using UnityEngine.UI;

public class NextFruitSpriteSetter : MonoBehaviour
{
    private Image image; 
    [SerializeField] private FruitSelector _nextFruitSelector;

    private void Awake()
    {
        FruitSpawner.spriteHasChanged += SetNextFruitImage;
        image = GetComponent<Image>();
    }
    void Start()
    {
        SetNextFruitImage();
    }

    private void SetNextFruitImage()
    {
        image.overrideSprite = _nextFruitSelector.NextFruitSprite;
    }
}
