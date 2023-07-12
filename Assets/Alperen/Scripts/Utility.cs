using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    static float secondsToMaxDifficulty = 20;

    public static float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
