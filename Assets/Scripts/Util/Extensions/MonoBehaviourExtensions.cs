using System;
using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static IEnumerator Timeout(this MonoBehaviour self, float sleepTimeSeconds, Action callback)
    {
        yield return new WaitForSeconds(sleepTimeSeconds);
        callback();
    }
}