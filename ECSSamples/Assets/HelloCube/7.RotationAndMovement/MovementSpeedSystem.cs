using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class MovementSpeedSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;
        var random = new Random(2);
        Entities.WithName("MovementSpeedSystem").ForEach((ref Translation pos, in MovementSpeedCom moveSpeed) =>
        {
            if (pos.Value.x > 200)
                pos.Value.x = -50;
            if (pos.Value.x < -50)
                pos.Value.x = 200;
            if (pos.Value.z > 200)
                pos.Value.z = -50;
            if (pos.Value.z < -50)
                pos.Value.z = 200;

            pos.Value = new float3(
                pos.Value.x + dt * moveSpeed.MoveSpeedPerSecond * math.cos(random.NextFloat(0.0f, 10.0f)), pos.Value.y,
                pos.Value.z + math.sin(random.NextFloat(0.0f, 10.0f) * moveSpeed.MoveSpeedPerSecond * dt));
        }).ScheduleParallel();
    }
}