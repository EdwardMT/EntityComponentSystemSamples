using System;
using Unity.Entities;

[Serializable]
public struct MovementSpeedCom : IComponentData
{
    public float MoveSpeedPerSecond;
}