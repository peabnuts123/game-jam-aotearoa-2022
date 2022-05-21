using System.Collections;
using System.Collections.Generic;
using Game.Entities;
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

    public override void InstallBindings()
    {
        // Singletons
        Container.Bind<PauseManager>().FromInstance(singleton_PauseManager);
        Container.Bind<TimeController>().FromInstance(singleton_TimeController);
    }
}
