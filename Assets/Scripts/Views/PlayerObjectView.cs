using UnityEngine;

public class PlayerObjectView : LevelObjectView
{
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        relativeVelocity = collision.relativeVelocity.y;
    }
}