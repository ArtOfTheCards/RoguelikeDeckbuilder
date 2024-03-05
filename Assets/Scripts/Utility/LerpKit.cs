using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This file contains functions used to extend lerping to include different eases.
// Code adapted from: https://www.febucci.com/2018/08/easing-functions/.
public class LerpKit : MonoBehaviour
{
    public static float Flip(float t)
    {
        return 1-t;
    }

    public static float EaseIn(float t, float power=2)
    {
        // EaseIn starts slow and speeds up, like an exponential curve.
        return Mathf.Pow(t,power);
    }

    public static float EaseOut(float t, float power=2)
    {
        // EaseIn starts fast and slows down, like a logarithmic curve.
        return Flip(EaseIn(Flip(t), power));
    }

    public static float EaseInOut(float t, float power=2)
    {
        // EaseIn starts fast, slows down, and speeds up, like a cubic curve.
        return Mathf.Lerp(EaseIn(t,power), EaseOut(t,power), t);
    }
}
