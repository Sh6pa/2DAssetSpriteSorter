using UnityEngine;
using System;

[Serializable]
public class TransformData
{
    public Vector3 LocalPosition = Vector3.zero;
    public Vector3 LocalEulerRotation = Vector3.zero;
    public Vector3 LocalScale = Vector3.one;
    public Vector3 WorldSpacePos = Vector3.zero;
    public Quaternion WorldSpaceRot = Quaternion.identity;


    // Unity requires a default constructor for serialization
    public TransformData() { }

    public TransformData(Transform transform)
    {
        LocalPosition = transform.localPosition;
        LocalEulerRotation = transform.localEulerAngles;
        LocalScale = transform.localScale;
        WorldSpacePos = transform.position;
        WorldSpaceRot = transform.rotation;
    }
    public void ApplyLocalTo(Transform transform)
    {
        transform.localPosition = LocalPosition;
        transform.localEulerAngles = LocalEulerRotation;
        transform.localScale = LocalScale;
    }
    public void ApplyWorldTo(Transform transform)
    {
        transform.position = WorldSpacePos;
        transform.rotation = WorldSpaceRot;
        transform.localScale = LocalScale;
    }
    public void ApplyLocalToWorldTo(Transform transform)
    {
        transform.position = LocalPosition;
        transform.eulerAngles = LocalEulerRotation;
        transform.localScale = LocalScale;
    }
}
