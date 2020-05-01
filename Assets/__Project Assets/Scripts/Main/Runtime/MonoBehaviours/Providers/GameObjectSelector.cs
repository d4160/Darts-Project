using System;
using UnityEngine;

public abstract class GameObjectSelector : ObjectSelector<GameObject>
{
    public Action<GameObject> onSelected;
    public Action<GameObject> onDeselected;

    public abstract void CheckSelection();
}
