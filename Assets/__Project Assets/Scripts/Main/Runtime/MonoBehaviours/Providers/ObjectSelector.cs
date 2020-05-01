using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSelector<T> : MonoBehaviour
{
    protected T _selectedObject;

    public T SelectedObject => _selectedObject;
}
