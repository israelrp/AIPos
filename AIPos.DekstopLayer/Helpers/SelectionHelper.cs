using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIPos.DekstopLayer.Helpers
{
    public class SelectionHelper<KeyType>
    {
        Dictionary<KeyType, bool> selectedValues = new Dictionary<KeyType, bool>();
        public bool GetIsSelected(KeyType key)
        {
            bool isSelected;
            if (selectedValues.TryGetValue(key, out isSelected))
                return isSelected;
            return false;
        }
        public void SetIsSelected(KeyType key, bool value)
        {
            if (value)
                selectedValues[key] = value;
            else
                selectedValues.Remove(key);
        }
        public List<KeyType> GetSelectedKeys()
        {
            List<KeyType> list = new List<KeyType>();
            foreach (KeyType key in selectedValues.Keys)
                list.Add(key);
            return list;
        }
        public string GetSelectedKeysAsString()
        {
            List<KeyType> list = GetSelectedKeys();
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
                str.AppendLine(list[i].ToString());
            return str.ToString();
        }
        public int GetSelectionCount()
        {
            return selectedValues.Count;
        }
    }
}