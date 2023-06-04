using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePopUps : MonoBehaviour
{
    [SerializeField] Image Message_1;
    [SerializeField] Image Message_2;
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Message_1.gameObject.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Message_1.gameObject.SetActive(false);
            Message_2.gameObject.SetActive(true);
        }
    }
}
