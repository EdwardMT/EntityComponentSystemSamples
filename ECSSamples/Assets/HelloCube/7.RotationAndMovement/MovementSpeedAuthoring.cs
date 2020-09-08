using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
[AddComponentMenu("DOTS Samples/RotationAndMovement/Movement Speed")]
public class MovementSpeedAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float MoveSpeedPerSecond = 2;

    public void Convert(Entity entity, EntityManager dstMgr, GameObjectConversionSystem conversionSystem)
    {
        dstMgr.AddComponentData(entity, new MovementSpeedCom {MoveSpeedPerSecond = MoveSpeedPerSecond});
    }
}