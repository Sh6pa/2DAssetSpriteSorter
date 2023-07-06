using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraAnimationScript : MonoBehaviour
{
    public Transform Start;
    public Transform End;
    [Range(1f, 60f)]
    public float Duration;

    private float m_counter = 0;
    private bool m_isReversing = false;
    public SortingLayer _sortingLayer;
    // Update is called once per frame
    void Update()
    {
        m_counter += Time.deltaTime + Time.deltaTime * -2.0f * Convert.ToSingle(m_isReversing);
        if (m_counter > Duration || m_counter <= 0)
            m_isReversing = !m_isReversing;
        transform.position = new Vector3(Mathf.Lerp(Start.position.x, End.position.x, Mathf.Clamp01(m_counter / Duration)), transform.position.y, transform.position.z);
    }
}
