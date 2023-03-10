using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformObjectView : LevelObjectView
{
    [SerializeField] private List<Sprite> _platformVariants;

    public void SetSprite()
    {
        var randIndex = Random.Range(0, _platformVariants.Count);
        SpriteRenderer.sprite = _platformVariants[randIndex];
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        OnLevelObjectContact?.Invoke();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}