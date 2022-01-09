using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawScrollView : MonoBehaviour
{
    public BoxCollider scrollviewCollider;
    public GameObject boardContainer;

    public bool CheckInsideScrollView(BoxCollider collider)
    {
        float itemMinBoundY = collider.bounds.center.y - collider.size.y / 2 * collider.transform.lossyScale.y;
        return scrollviewCollider.bounds.max.y > itemMinBoundY;
    }
}
