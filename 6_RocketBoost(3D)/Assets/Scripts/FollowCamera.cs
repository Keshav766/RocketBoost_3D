using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject cameraRef;
    [SerializeField] Vector3 offSet = new Vector3(0f,0f,1f);
    void Update()
    {
        transform.position = cameraRef.transform.position + offSet;
    }
}
