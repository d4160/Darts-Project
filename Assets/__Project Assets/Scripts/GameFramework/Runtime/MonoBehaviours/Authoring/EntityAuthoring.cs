using d4160.GameFramework;
#if UNITY_ECS
using Unity.Entities;
#endif

namespace GameFramework
{
    using UnityEngine;

    public class EntityAuthoring : DefaultEntityAuthoring
    {
        [SerializeField] protected ArchetypeEnum _entity;

        public override int Entity => (int)_entity;

#if UNITY_ECS

        public override void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            if (enabled)
            {
                dstManager.AddComponentData(entity, new EntityData() { entity = (int)_entity });
            }
        }
#endif
    }

}