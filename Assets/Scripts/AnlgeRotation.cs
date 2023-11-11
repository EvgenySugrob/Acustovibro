using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnlgeRotation : MonoBehaviour
{
    [SerializeField]
    private Transform room;

    private void Update()
    {
        var direction = room.position - transform.position;
        var angle = Mathf.Atan2(direction.x,direction.y) * Mathf.Rad2Deg;
        Debug.Log(angle);
    }
}

