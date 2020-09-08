using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]
public class SpawnerSystem : SystemBase
{
    private BeginInitializationEntityCommandBufferSystem _commandBufferSystem;

    protected override void OnCreate()
    {
        base.OnCreate();
        _commandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var commandBuffer = _commandBufferSystem.CreateCommandBuffer().ToConcurrent();
        Entities
            .WithName("SpawnerSystem")
            .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
            .ForEach((Entity entity, int entityInQueryIndex, in SpawnerCom spawner, in LocalToWorld location) =>
            {
                var random = new Random(1);
                for (var x = 0; x < spawner.CountX; ++x)
                {
                    for (var y = 0; y < spawner.CountY; ++y)
                    {
                        var instance = commandBuffer.Instantiate(entityInQueryIndex, spawner.Prefab);

                        var pos = math.transform(location.Value,
                            new float3(x * 1.3f, noise.cnoise(new float2(x, y) * 0.21f) * 2, y * 1.3f));
                        commandBuffer.SetComponent(entityInQueryIndex, instance, new Translation {Value = pos});
                        commandBuffer.SetComponent(entityInQueryIndex, instance,
                            new RotationSpeedCom {RadiansPerSecond = math.radians(random.NextFloat(25.0f, 90.0f))});
                        commandBuffer.SetComponent(entityInQueryIndex, instance,
                            new MovementSpeedCom {MoveSpeedPerSecond = random.NextFloat(1.0f, 10.0f)});
                    }
                }

                commandBuffer.DestroyEntity(entityInQueryIndex, entity);
            }).ScheduleParallel();
        
        _commandBufferSystem.AddJobHandleForProducer(Dependency);
    }
}