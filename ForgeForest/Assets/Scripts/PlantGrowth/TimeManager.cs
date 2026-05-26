using UnityEngine;
using System.Collections.Generic;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    private float currentTime;
    private float RateOfTime;
    private float LengthOfDay;
    private float TimeOfDay;

    public event Action<float> TimeChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        TimeChanged?.Invoke(Time.deltaTime);
    }
}
