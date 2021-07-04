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

    public override int GetHashCode() => (item, quantity).GetHashCode();

    public override bool Equals(object obj)
    {
        if (obj is Item i)
        {
            return item == i.item && quantity == i.quantity;
        }
        return false;
    }
    public static bool operator ==(Item a, Item b)
    {
        return a.item == b.item && a.quantity == b.quantity;
    }
    public static bool operator !=(Item a, Item b)
    {
        return !(a.item == b.item && a.quantity == b.quantity);
    }
}