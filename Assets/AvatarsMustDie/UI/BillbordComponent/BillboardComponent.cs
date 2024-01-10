using System;
using UnityEngine;
using Zenject;

public readonly struct BillboardProtocol
{
    public readonly Transform ObjectTransform;
    public readonly Transform SubjectTransform;

    public BillboardProtocol(Transform objectTransform, Transform subjectTransform)
    {
        ObjectTransform = objectTransform;
        SubjectTransform = subjectTransform;
    }
}

public class BillboardComponent : ITickable, IDisposable
{
    private readonly Transform _object;
    private readonly Transform _subject;
    private readonly TickableManager _tickableManager;
    
    public BillboardComponent(BillboardProtocol protocol, TickableManager tickableManager)
    {
        _object = protocol.ObjectTransform;
        _subject = protocol.SubjectTransform;
        _tickableManager = tickableManager;
        tickableManager.Add(this);
    }
    
    public void Tick()
    {
        _object.transform.LookAt(_object.transform.position + _subject.transform.forward);
    }

    public void Dispose()
    {
        _tickableManager.Remove(this);
    }
}
