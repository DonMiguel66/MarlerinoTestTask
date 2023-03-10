using System;
using UnityEngine;


public class RestartZoneView : MonoBehaviour
{
    public Action OnRestartZoneContact { get; set; }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerObjectView>())
            OnRestartZoneContact?.Invoke();
    }
}