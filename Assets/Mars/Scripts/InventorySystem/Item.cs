using UnityEngine;

 namespace PLu.Mars.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Resources/Item")]
    public class Item : ScriptableObject
    {
        public string Name => name;
        public string Description => _description;
        public float Quantity => _quantity;
        public float WeightPerUnit => _weightPerUnit;
        public Sprite Icon => _icon;
        public float Weight => WeightPerUnit * Quantity;

        [SerializeField] private string _description;
        [SerializeField] private float _weightPerUnit;
        [SerializeField] private float _quantity;
        [SerializeField] private Sprite _icon;

        public Item(string name, string description, float weightPerUnit, float quantity, Sprite icon)
        {
            this.name = name;
            _description = description;
            _weightPerUnit = weightPerUnit;
            _quantity = quantity;
            _icon = icon;
        }
        public Item()
        {
            _description =  "Default";
            _weightPerUnit = 1f;
            _quantity = 0f;
            _icon = null;
        }
        public Item(Item otherItem)
        {
            name = otherItem.Name;
            _description = otherItem.Description;
            _weightPerUnit = otherItem.WeightPerUnit;
            _quantity = otherItem.Quantity;
            _icon = otherItem.Icon;
        }
        public Item(Item otherItem, float quantity)
        {
            name = otherItem.Name;
            _description = otherItem.Description;
            _weightPerUnit = otherItem.WeightPerUnit;
            _quantity = quantity;
            _icon = otherItem.Icon;
        }
        public bool AddQuantity(float quantity)
        {
            if (quantity < 0)
            {
                return false;
            }
            _quantity += quantity;
            return true;
        }
        public Item RemoveQuantity(float quantity, out bool isExhausted)
        {
            float actualQuantity;
            isExhausted = false;

            if (quantity < 0)
            {
                return null;
            }
            if (quantity > _quantity)
            {
                actualQuantity = _quantity;
                _quantity = 0;
                isExhausted = true;
                return new Item(this, actualQuantity);
            }
            _quantity -= quantity;
            actualQuantity = quantity;
            return new Item(this, actualQuantity);
        }
       public Item Merge(Item otherItem, bool copy = true)
        {
            if (otherItem.Name != Name)
            {
                return null;
            }
            
            if (copy)
            {
                return new Item(Name, Description, WeightPerUnit, Quantity + otherItem.Quantity, Icon);
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
