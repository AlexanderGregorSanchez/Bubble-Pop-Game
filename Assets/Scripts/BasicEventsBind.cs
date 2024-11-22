using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicEventsBind : MonoBehaviour
{
    public UnityEvent OnObjectAwake;
    public UnityEvent OnObjectEnable;
    public UnityEvent OnObjectStart;
    public UnityEvent OnObjectDisable;
    public UnityEvent OnObjectDestroy;

    private void Awake()
    {
        OnObjectAwake?.Invoke();
    }
    private void OnEnable()
    {
        OnObjectEnable?.Invoke();
    }
    private void OnDisable()
    {
        OnObjectDisable?.Invoke();
    }
    private void Start()
    {
        OnObjectStart?.Invoke();
    }
    private void OnDestroy()
    {
        OnObjectDestroy?.Invoke();
    }
    
}
