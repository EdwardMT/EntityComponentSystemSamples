using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = Unity.Mathematics.Random;

public class ClassicSpawner : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject Root;
    public int CountX;
    public int CountY;

    private List<GameObject> _objects;
    private List<float> _rotationSpeed;
    private List<float> _movementSpeed;
    
    public void Start()
    {
        _objects = new List<GameObject>();
        _rotationSpeed = new List<float>();
        _movementSpeed = new List<float>();
        
        var random = new Random(1);
        for (var x = 0; x < CountX; ++x)
        {
            for (var y = 0; y < CountY; ++y)
            {
                var obj = Object.Instantiate(Prefab, Root.transform, false);
                obj.transform.position = new Vector3(x * 1.3f, noise.cnoise(new float2(x, y) * 0.21f) * 2, y * 1.3f);
                _objects.Add(obj);
                _rotationSpeed.Add(math.radians(random.NextFloat(25.0f, 90.0f)) );
                _movementSpeed.Add(random.NextFloat(1.0f, 10.0f));
            }
        }
    }

    public void Update()
    {
        var random = new Random(2);
        for (var i = 0; i < _objects.Count; ++i)
        {
            var trans = _objects[i].transform;
            
            trans.rotation = math.mul(math.normalize(trans.rotation),
                quaternion.AxisAngle(math.up(), _rotationSpeed[i] * Time.deltaTime));

            var pos = trans.position;

            if (pos.x > 200)
                pos.x = -50;
            if (pos.x < -50)
                pos.x = 200;
            if (pos.z > 200)
                pos.z = -50;
            if (pos.z < -50)
                pos.z = 200;

            trans.position =
                new Vector3(pos.x + Time.deltaTime * _movementSpeed[i] * math.cos(random.NextFloat(0.0f, 10.0f)), pos.y,
                    pos.z + Time.deltaTime * _movementSpeed[i] * math.sin(random.NextFloat(0.0f, 10.0f)));
        }
    }
}