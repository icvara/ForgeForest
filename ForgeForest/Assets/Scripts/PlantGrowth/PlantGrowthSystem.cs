using System.Collections.Generic;
using UnityEngine;

public class PlantGrowthSystem : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> PlantPhases = new List<GameObject>();

    [SerializeField]
    private float GrowthTime = 60f;

    private int CurrentPhase = 0;
    private bool FullyGrown = false;

    private float GrowthTimeElapsed = 0f;




  

    private void Update()
    {
        if (!FullyGrown)
        {
            UpdatePlantBasedOnPhase(Time.deltaTime);
        }
    }

    public void UpdatePlantBasedOnPhase(float timePassed)
    {
        if (FullyGrown)
            return;

        GrowthTimeElapsed += timePassed;

        int targetPhase = Mathf.FloorToInt(GrowthTimeElapsed / TimePerPhase);

        targetPhase = Mathf.Clamp(targetPhase, 0, PlantPhases.Count - 1);

        if (targetPhase != CurrentPhase)
        {
            ChangePhase(targetPhase);
        }
    }

    // Used for preset plants already placed in the scene
    public void SetPhase(int phase)
    {
        phase = Mathf.Clamp(phase, 0, PlantPhases.Count - 1);

        CurrentPhase = phase;

        GrowthTimeElapsed = TimePerPhase * phase;

        FullyGrown = (CurrentPhase >= PlantPhases.Count - 1);

        UpdateVisuals();
    }

    // Changes the plant to the next growth phase
    private void ChangePhase(int phase)
    {
        CurrentPhase = phase;

        if (CurrentPhase >= PlantPhases.Count - 1)
        {
            CurrentPhase = PlantPhases.Count - 1;
            FullyGrown = true;
        }

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < PlantPhases.Count; i++)
        {
            PlantPhases[i].SetActive(i == CurrentPhase);
        }
    }

    private float TimePerPhase
    {
        get
        {
            if (PlantPhases.Count <= 1)
                return GrowthTime;

            return GrowthTime / (PlantPhases.Count - 1);
        }
    }
}
