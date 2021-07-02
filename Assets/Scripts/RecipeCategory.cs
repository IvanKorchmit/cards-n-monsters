using UnityEngine;

[CreateAssetMenu(fileName = "New Category", menuName = "Recipes/Category")]
public class RecipeCategory : ScriptableObject
{
    public CraftRecipe[] Recipes;
}
