using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    void Start()
    {
        Invoke("CommitSeppuku",3);
    }


    private void CommitSeppuku()
    {
        Destroy(gameObject);
    }
}
