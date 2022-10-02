using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggHolder : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x > 11f)
            Destroy(gameObject);
    }
}
