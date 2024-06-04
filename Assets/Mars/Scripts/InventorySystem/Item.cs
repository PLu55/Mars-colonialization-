
 namespace PLu.Mars.InventorySystem
{
    public class Item
    {
        public string Name => _name;
        public string Description => _description;
        public float Quantity => _quantity;
        public float WeightPerUnit => _weightPerUnit;
        public float Weight => WeightPerUnit * Quantity;
        public void AddQuantity (float quantity) => _quantity += quantity;

        private string _name;
        private string _description;
        private float _weightPerUnit;
        private float _quantity;

        public Item(string name, string description, float weightPerUnit, float quantity)
        {
            _name = name;
            _description = description;
            _weightPerUnit = weightPerUnit;
            _quantity = quantity;
        }

        public Item Merge(Item otherItem, bool copy = true)
        {
            if (otherItem.Name != Name)
            {
                return null;
            }
            
            if (copy)
            {
                return new Item(Name, Description, WeightPerUnit, Quantity + otherItem.Quantity);
            }
            _quantity += otherItem.Quantity;
            return this;
        }   

        public new string ToString()
        {
            return $"Item(Name: {Name}, Description: {Description}, Weight: {Weight}, Quantoty: {Quantity})";
        }
    }
}
