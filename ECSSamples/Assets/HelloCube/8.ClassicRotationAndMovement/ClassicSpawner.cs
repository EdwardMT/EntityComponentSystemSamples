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
        for (var i = 0; i < _objects.Count; ++i)
        {
            var trans = _objects[i].transform;
            
            trans.rotation = math.mul(math.normalize(trans.rotation),
                quaternion.AxisAngle(math.up(), _rotationSpeed[i] * Time.deltaTime));

            var pos = trans.position;
            if (pos.x > 110)
                pos = new Vector3(-10.0f, pos.y, pos.z);

            trans.position = new Vector3(pos.x + Time.deltaTime * _movementSpeed[i], pos.y, pos.z);
        }
    }
}