using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItemDatabase{

    static private List<BaseItem> _items;

    static private bool _isDatabaseLoaded = false;

    static private void ValidateDatabase()
    {
        if (_items == null) _items = new List<BaseItem>(); 
        {

        }
        if (!_isDatabaseLoaded) LoadDatabase();
    }

    static public void LoadDatabase()
    {
        if (_isDatabaseLoaded) return;
        _isDatabaseLoaded = true;
        LoadDatabaseForce();
    }

    static public void LoadDatabaseForce()
    {
        ValidateDatabase();
        BaseItem[] resources = Resources.LoadAll<BaseItem>(@"BaseItems");
        foreach(BaseItem item in resources)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);            
            }
        }    
    }

    static public void ClearDatabase()
    {
        _isDatabaseLoaded = false;
        _items.Clear();
    }

    static public BaseItem GetItem(int id)
    {
        ValidateDatabase();
        foreach(BaseItem item in _items)
        {
         if(item.ItemID == id)
            {
                return ScriptableObject.Instantiate(item) as BaseItem;
            }

        }
        return null;
    }

}
