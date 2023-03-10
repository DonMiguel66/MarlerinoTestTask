using System;
using UnityEngine;

public class LevelObjectView : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D _rigidbody;
    protected float relativeVelocity;
    public Action OnLevelObjectContact { get; set; }

    public Transform Transform => _transform;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public Collider2D Collider => _collider;
    public Rigidbody2D RigidBody => _rigidbody;
    public float RelativeVelocity => relativeVelocity;
    
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        //LevelObjectView levelObject = collision.gameObject.GetComponent<LevelObjectView>();
        //OnLevelObjectContact?.Invoke(levelObject);
        OnLevelObjectContact?.Invoke();
    }

    /*protected void OnCollisionEnter2D(Collision2D collision)
    {
        relativeVelocity = collision.relativeVelocity.y;
        LevelObjectView levelObject = collision.gameObject.GetComponent<LevelObjectView>();
        OnLevelObjectContact?.Invoke(levelObject);
    }*/
}