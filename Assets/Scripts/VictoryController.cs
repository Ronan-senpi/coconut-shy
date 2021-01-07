using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    [SerializeField]
    LayerMask layerMask;
    private void OnTriggerEnter(Collider other)
    {
        //Compare sur les bit et non sur la text plus rapide
        if ((layerMask.value & (1 << other.gameObject.layer)) > 0)
        {
            GameManager.Instance.AddCan(other.gameObject.name);
        }
    }
}
