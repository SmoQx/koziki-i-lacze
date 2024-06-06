using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    // Start is called before the first frame update
    string file_with_data = "user_data.json";
    SaveLoadFile savefile = new SaveLoadFile();
    public ItemObj val;

    void Start()
    {
         // Get the Button component attached to the same GameObject
        Button button = GetComponent<Button>();
        if (button != null)
        {
            // Add a listener to the button click event
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    public void OnButtonClick()
    {
        string character = savefile.Load_to_file(file_with_data);
        JObject parsed = JObject.Parse(character);
        JObject skills = JObject.Parse(parsed["Skills"].ToString());
        JObject inv_parsed = JObject.Parse(parsed["ItemsList"].ToString());
        
        
        if (val != null)
        {
            string what = gameObject.tag;
            string buttonName = gameObject.name;
            int itemValueInt = val.intValue;
            Debug.Log(itemValueInt);
            if (int.Parse(inv_parsed["Money"].ToString()) - itemValueInt >= 0)
            {
                inv_parsed["Money"] = int.Parse(inv_parsed["Money"].ToString()) - itemValueInt;
                if (what != "Skills" && !(inv_parsed.ToString().Contains(buttonName)))
                {
                    inv_parsed[what] = buttonName;
                    parsed["ItemsList"] = inv_parsed;
                    character = parsed.ToString();
                }
                else if (what == "Skills" && int.Parse(skills[buttonName].ToString()) < 5)
                {
                    skills[buttonName] = int.Parse(skills[buttonName].ToString()) + 1;
                    parsed["ItemsList"] = inv_parsed;
                    parsed["Skills"] = skills;
                    character = parsed.ToString();
                    Debug.Log(character);
                }
                savefile.Save_to_file(character, file_with_data);
            }
        }
        else
        {
            Debug.LogError("ItemValue reference is missing.");
        }
    }
}
