using System;
using Game.Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Components
{
    [Serializable]
    public class PlayerControllerEvent : UnityEvent<PlayerController>
    {
    }
}