using UnityEngine;

[CreateAssetMenu(fileName = "New Category", menuName = "Recipes/Category")]
public class RecipeCategory : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public CraftRecipe[] Recipes;
}
