using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager getInstance() { return instance; }

    EconomyManager economyManager;
    BuildingManager buildingManager;

    InGameUI inGameUI;

    enum PlayingState { COMBAT, BUILDING}

    PlayingState currentPlayingState;

    private void Awake()
    {
        instance = this;

    }


    // Start is called before the first frame update
    void Start()
    {
        economyManager = this.GetComponent<EconomyManager>();
        buildingManager = this.GetComponent<BuildingManager>();

        inGameUI = UIManager.getInstance().getInGameUI();

        ActivateCombatState();
    }

    // Update is called once per frame
    void Update()
    {
        inGameUI.setValueGoldAvailable(economyManager.getCurrentGold());
        inGameUI.setValueSearchAvailable(economyManager.getCurrentSearchPoint());

       
    }


    public void SwapPlayingState()
    {
        if (currentPlayingState == PlayingState.COMBAT)
        {
            ActivateBuildingState();
        }
        else
        {
            ActivateCombatState();
        }
    }

    private void ActivateCombatState()
    {
        buildingManager.enabled = false;

        inGameUI.DisplayCombatUI();

        currentPlayingState = PlayingState.COMBAT;
    }

    private void ActivateBuildingState()
    {
        buildingManager.enabled = true;

        inGameUI.DisplayBuildingUI();
        currentPlayingState = PlayingState.BUILDING;
    }
}
