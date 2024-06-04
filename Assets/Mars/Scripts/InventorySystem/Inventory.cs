using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
 
namespace PLu.Mars.InventorySystem
{
    public class Inventory : MonoBehaviour, IInventory
    {
        private Item[] _items;
        private int _slots = 20;

        public delegate void OnInventoryChanged();
        public OnInventoryChanged onInventoryChangedCallback;
        void Awake()
        {
            _items = new Item[_slots];
        }
        public bool TryAdd(Item item)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = item;
                    if (onInventoryChangedCallback != null)
                    {
                        onInventoryChangedCallback.Invoke();
                    }
                    return true;
                }
            }
            return false;
        }
    
        public bool TryRemove(string name, out Item item, float quantity = -1f)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].Name == name)
                {
                    if (_items[i].Quantity > quantity)
                    {
                        _items[i].AddQuantity(-quantity);
                        if (onInventoryChangedCallback != null)
                        {
                            onInventoryChangedCallback.Invoke();
                        }
                        item = new Item(name, _items[i].Description, _items[i].WeightPerUnit, quantity);
                        return true;
                    }
                    else
                    {
                        item = _items[i];
                        _items[i] = null;
                        if (onInventoryChangedCallback != null)
                        {
                            onInventoryChangedCallback.Invoke();
                        }
                        return true;
                    }
                }
            }

            item = null;
            return false;
        }
        public bool HasItem(string name)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].Name == name)
                {
                    return true;
                }
            }
            return false;
        }
        public float QuantityOf(string name)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].Name == name)
                {
                    return _items[i].Quantity;
                }
            }
            return 0f;
        }
    }
}
