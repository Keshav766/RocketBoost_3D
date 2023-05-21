using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarHandler : MonoBehaviour
{
    [SerializeField] GameObject enemyRef;
    [SerializeField] Vector3 posOffset = new Vector3(0, 1, 0);

    

    void Update()
    {
        transform.position = enemyRef.transform.position + posOffset;
    }

  
}
