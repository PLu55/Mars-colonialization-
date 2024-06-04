using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
namespace PLu.Mars.InventorySystem
{
    public interface IInventory
    {
	    delegate void OnItemChanged();
        bool TryAdd(Item item);
        bool TryRemove(string name, out Item item, float quantity = -1f);
        bool HasItem(string name);
        float QuantityOf(string name);       
    }
}
