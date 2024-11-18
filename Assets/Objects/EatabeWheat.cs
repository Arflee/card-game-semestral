using UnityEngine;

public class EatabeWheat : MonoBehaviour
{
    [SerializeField] private float growthTime;
    [SerializeField] private float seedTime;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private LayerMask bearLayer;

    public float Freshness { get; private set; }
    public Field field;

    private void Start()
    {
        Freshness = Random.value;
    }

    private void Update()
    {
        Color c = sprite.color;
        c.a = Mathf.Clamp01(Freshness);
        sprite.color = c;

        if (Freshness >= 1)
        {
            return;
        }

        if (Freshness < 0)
            Freshness += Time.deltaTime / seedTime;
        else
            Freshness += Time.deltaTime / growthTime;
    }

    public void Eat()
    {
        Freshness = -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Freshness > 0 && (bearLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            field.DestroyWheat(this);
            Destroy(gameObject);
        }
    }
}
