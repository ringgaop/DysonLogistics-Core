using Unity.Entities;
using Unity.Collections;

namespace DysonLogistics.Simulation
{
    /// <summary>
    /// BlobAsset used for read-only, highly shared data like production recipes.
    /// Eliminates GC and reduces memory footprint for 100k+ buildings.
    /// </summary>
    public struct RecipeData
    {
        public int InputItemID;
        public int OutputItemID;
        public float CraftTimeSeconds;
    }

    public struct RecipeCatalog
    {
        public BlobArray<RecipeData> Recipes;
    }

    /// <summary>
    /// Authoring component to bake recipes into BlobAssets at runtime.
    /// </summary>
    public class RecipeCatalogAuthoring : UnityEngine.MonoBehaviour
    {
        // Inspector fields would go here
    }

    public class RecipeCatalogBaker : Baker<RecipeCatalogAuthoring>
    {
        public override void Bake(RecipeCatalogAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            
            // Create BlobAsset and build the recipe list
            using var builder = new BlobBuilder(Allocator.Temp);
            ref var root = ref builder.ConstructRoot<RecipeCatalog>();
            
            var recipes = builder.Allocate(ref root.Recipes, 10000); // Simulating 10k recipes
            for (int i = 0; i < 10000; i++)
            {
                recipes[i] = new RecipeData
                {
                    InputItemID = i,
                    OutputItemID = i + 1,
                    CraftTimeSeconds = 1.5f
                };
            }

            var blobAsset = builder.CreateBlobAssetReference<RecipeCatalog>(Allocator.Persistent);
            AddBlobAsset(ref blobAsset, out _);
            AddComponent(entity, new RecipeReference { Blob = blobAsset });
        }
    }

    public struct RecipeReference : IComponentData
    {
        public BlobAssetReference<RecipeCatalog> Blob;
    }
}
