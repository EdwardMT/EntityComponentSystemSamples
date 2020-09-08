using System;
using Unity.Entities;

[Serializable]
public struct RotationSpeedCom : IComponentData
{
    public float RadiansPerSecond;
}