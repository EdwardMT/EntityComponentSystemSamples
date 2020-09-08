using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class MovementSpeedSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;

        Entities.WithName("MovementSpeedSystem").ForEach((ref Translation pos, in MovementSpeedCom moveSpeed) =>
        {
            if (pos.Value.x > 200)
                pos.Value.x = -50;
            
            pos.Value = new float3(pos.Value.x + dt * moveSpeed.MoveSpeedPerSecond, pos.Value.y, pos.Value.z);
        }).ScheduleParallel();
    }
}