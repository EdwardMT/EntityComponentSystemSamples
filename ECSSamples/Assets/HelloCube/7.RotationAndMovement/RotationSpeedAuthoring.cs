using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[RequiresEntityConversion]
[AddComponentMenu("DOTS Samples/RotationAndMovement/Rotation Speed")]
public class RotationSpeedAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
        public float DegreesPerSecond = 360;

        public void Convert(Entity entity, EntityManager dstMgr, GameObjectConversionSystem conversionSystem)
        {
                dstMgr.AddComponentData(entity,
                        new RotationSpeedCom {RadiansPerSecond = math.radians(DegreesPerSecond)});
        }
}