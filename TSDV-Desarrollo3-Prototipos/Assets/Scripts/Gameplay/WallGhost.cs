using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGhost : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void CapturePlayer(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer($"Player"))
        {
            
        }
    }
}
