using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using GameFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityExtensions;

public class MultiplayerModeManager : GameModeManagerBase
{
    public override void Despawn(GameObject instance, int entity, int poolIndex = 0, float delay = 0f)
    {
        Despawn(instance, (ArchetypeEnum)entity, poolIndex, delay);
    }

    public override void Despawn(GameObject instance, int entity, int category, int poolIndex = 0, float delay = 0f)
    {
        Despawn(instance, (ArchetypeEnum)entity, category, poolIndex, delay);
    }

    public void Despawn(GameObject instance, ArchetypeEnum entity, int poolIndex = 0, float delay = 0f)
    {
    }

    public void Despawn(GameObject instance, ArchetypeEnum entity, int category, int poolIndex = 0, float delay = 0f)
    {
        switch (entity)
        {
            case ArchetypeEnum.Player:
                break;
        }
    }

    public override void StartSpawner(int spawnIndex = -1)
    {
        switch (spawnIndex)
        {
            case -1:
                break;
        }
    }

    public override void StopSpawner(int spawnIndex = -1)
    {
        switch (spawnIndex)
        {
            case -1:
                break;
        }
    }
}