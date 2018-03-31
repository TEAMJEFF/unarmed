using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSightline : MonoBehaviour {

    private float distanceToPlayer = 7.0f;
    private List<ObjectTransparency> thingsMadeTransparent = new List<ObjectTransparency>();
	
	// Update is called once per frame
	void Update () {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position, transform.forward, distanceToPlayer);

        foreach(RaycastHit hit in hits)
        {
            Renderer R = hit.collider.GetComponent<Renderer>();
            if (R == null)
            {
                continue;
            }
            ObjectTransparency OT = R.GetComponent<ObjectTransparency>();
            if (OT == null)
            {
                OT = R.gameObject.AddComponent<ObjectTransparency>();
            }
            OT.MakeTransparent();
            thingsMadeTransparent.Add(OT);
        }
	}

    public void ResetTransparency()
    {
        for (int i = 0; i < thingsMadeTransparent.Count; i++)
        {
            thingsMadeTransparent[i].ResetTransparency();
        }
    }
}
