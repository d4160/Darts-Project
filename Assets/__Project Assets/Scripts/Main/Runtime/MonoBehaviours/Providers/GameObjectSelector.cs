using System;
using UnityEngine;

public abstract class GameObjectSelector : ObjectSelector<GameObject>
{
    public Action<GameObject> onSelected;
    public Action<GameObject> onDeselected;

    public virtual void CheckSelection()
    {
        CheckIn();

        CheckOut();
    }

    protected virtual void CheckIn()
    {
    }

    protected virtual void CheckOut()
    {
    }
}
