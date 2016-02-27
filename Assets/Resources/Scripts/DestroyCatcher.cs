using UnityEngine;
using System.Collections;

public class DestroyCatcher : MonoBehaviour {

	void OnDestroy()
    {
        Debug.Break();
    }
}
