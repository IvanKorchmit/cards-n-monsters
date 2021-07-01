[System.Serializable]
public class Item
{
    public BaseItem item;
    public int quantity;
    public override string ToString()
    {
        return $"{item.name} {item.id}";
    }
}