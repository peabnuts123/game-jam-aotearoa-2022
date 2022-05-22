using System.Collections;
using System.Collections.Generic;
using Game.Entities;
using Game.UI;
using UnityEngine;
using Zenject;

public class PauseableSceneInstaller : MonoInstaller
{
    [NotNull]
    [SerializeField]
    private PauseManager singleton_PauseManager;
    [NotNull]
    [SerializeField]
    private TimeController singleton_TimeController;
    [NotNull]
    [SerializeField]
    private GoalMenu goalUIMenu;

    public override void InstallBindings()
    {
        // Singletons
        Container.Bind<PauseManager>().FromInstance(singleton_PauseManager);
        Container.Bind<TimeController>().FromInstance(singleton_TimeController);

        // References
        Container.Bind<GoalMenu>().FromInstance(goalUIMenu);
    }
}
