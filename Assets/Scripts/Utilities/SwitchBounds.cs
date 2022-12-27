using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBounds : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        SwitchConfinerShape();
    }

    private void SwitchConfinerShape()
    {
        PolygonCollider2D confinerShape = GameObject.FindGameObjectWithTag("BoundsConfiner").GetComponent<PolygonCollider2D>();

        CinemachineConfiner confiner = GetComponent<CinemachineConfiner>();

        confiner.m_BoundingShape2D = confinerShape;

        //Call this if the bounding shape's point change at runtime
        confiner.InvalidatePathCache();
    }
}