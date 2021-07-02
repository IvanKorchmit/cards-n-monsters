[System.Serializable]
public class Item
{   
    public BaseItem item;
    public int quantity;
    public override string ToString()
    {
        return $"{item.name} {item.id}";
    }
    public Item(int quantity, BaseItem item)
    {
        this.quantity = quantity;
        this.item = item;
    }
}