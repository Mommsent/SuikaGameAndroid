using UnityEngine;

public class ColliderInformer : MonoBehaviour
{
    public delegate void WasCollidedWithSameFruit(int amount);
    public static event WasCollidedWithSameFruit wasCollided;

    public delegate void HasDropped();
    public static event HasDropped wasDropped;

    public bool WasCombinedIn { get; private set; } = false;

    public bool HasCollided { get; private set; }

    private FruitInfo _info;

    [SerializeField] private AudioClip _audioClip;

    private void Awake()
    {
        _info = GetComponent<FruitInfo>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasCollided == false && WasCombinedIn == false)
        {
            HasCollided = true;
            wasDropped?.Invoke();
        }

        FruitInfo info = collision.gameObject.GetComponent<FruitInfo>();
        if (info != null)
        {
            if (info.FruitIndex == _info.FruitIndex)
            {
                int thisID = gameObject.GetInstanceID();
                int otherID = collision.gameObject.GetInstanceID();

                if (thisID > otherID)
                {
                    if (_info.FruitIndex == FruitSelector.Instance.FruitsListLength - 1)
                    {
                        WasCombinedIn = true;
                        wasCollided?.Invoke(info.PointsWhenAnnihilated);
                        DestroyCollidedFruits(collision);
                    }
                    else
                    {
                        WasCombinedIn = true;
                        wasCollided?.Invoke(info.PointsWhenAnnihilated);
                        CombinedFruit(collision);
                        DestroyCollidedFruits(collision);
                    }
                }
            }
        }

    }

    private void CombinedFruit(Collision2D collision)
    {
        Vector3 middlePosition = (transform.position + collision.transform.position) / 2f;
        ColliderInformer nextLevelFruit = Instantiate(GetNextFruitPref(_info.FruitIndex), transform.parent);
        nextLevelFruit.WasCombinedIn = true;
        nextLevelFruit.transform.position = middlePosition;
    }

    private ColliderInformer GetNextFruitPref(int index)
    {
        ColliderInformer nextLevelFruit = FruitSelector.Instance.GetNextLevelFruit(index);
        return nextLevelFruit;
    }

    private void DestroyCollidedFruits(Collision2D collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
