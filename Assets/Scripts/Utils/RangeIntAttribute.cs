using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIntAttribute : PropertyAttribute {

    //sealed class Ref<T>
    //{
    //    public Ref(T var)
    //    {
    //        Value = var;
    //    }
    //    public T Value;
    //}

    public int min;
    public int max;
    public RangeIntAttribute(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}
