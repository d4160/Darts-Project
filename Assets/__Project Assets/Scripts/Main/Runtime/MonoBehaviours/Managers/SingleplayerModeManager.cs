using System.Collections;
using System.Collections.Generic;
using d4160.GameFoundation;
using d4160.GameFramework;
using GameFramework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityExtensions;

public class SingleplayerModeManager : GameModeManagerBase
{
    [SerializeField] private bool _instanceDart;
    [SerializeField] private SpawnProvider _provider;
    [SerializeField] private Transform _dartPosition;
    [SerializeField] private int _darts;
    [SerializeField] private HUDCanvas _hud;

    private int _used;
    private int _score;

    void Start()
    {
        if (_instanceDart)
            Invoke("Instantiate", 1f);
    }

    public void AddScore(int val)
    {
        _score += val;

        _hud.UpdateStat(0, _score);
        _hud.UpdateStat(1, val);
    }

    public GameObject Instantiate()
    {
        _used++;

        if (_darts < _used)
            return null;
        
        _provider.OverrideSpawnPosition = _dartPosition.position;
        _provider.Spawn();
        _provider.LastGameObject.transform.rotation = Quaternion.Euler(Vector3.up * -90f);

        return _provider.LastGameObject;
    }

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