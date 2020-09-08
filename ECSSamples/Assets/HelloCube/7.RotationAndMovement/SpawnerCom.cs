using Unity.Entities;

public struct SpawnerCom : IComponentData
{
    public int CountX;
    public int CountY;
    public Entity Prefab;
}