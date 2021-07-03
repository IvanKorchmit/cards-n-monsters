[System.Serializable]
public struct Item
{   
    public BaseItem item;
    public int quantity;
    public override string ToString()
    {
        return $"{item.name} {quantity}";
    }
    public Item(int quantity, BaseItem item)
    {
        this.quantity = quantity;
        this.item = item;
    }
}