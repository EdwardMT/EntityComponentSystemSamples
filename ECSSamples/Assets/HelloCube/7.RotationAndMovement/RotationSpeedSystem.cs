using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class RotationSpeedSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var dt = Time.DeltaTime;

        Entities.WithName("RotationSpeedSystem").ForEach((ref Rotation rotation, in RotationSpeedCom rotSpeed) =>
        {
            rotation.Value = math.mul(math.normalize(rotation.Value),
                quaternion.AxisAngle(math.up(), rotSpeed.RadiansPerSecond * dt));
        }).ScheduleParallel();
    }
}