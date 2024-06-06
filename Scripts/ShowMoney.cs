using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;

public class ShowAbmountOfMoney : MonoBehaviour
{
    string file_with_data = "user_data.json";
    SaveLoadFile io = new SaveLoadFile();
    public TMP_Text to_show;
    string val;

    void Start()
    {
        JObject character_data = JObject.Parse(io.Load_to_file(file_with_data));
        JObject money = JObject.Parse(character_data["ItemsList"].ToString());
        to_show.text = money["Money"].ToString();
    }

    void Update()
    {
        JObject character_data = JObject.Parse(io.Load_to_file(file_with_data));
        to_show.text = character_data["ItemsList"]["Money"].ToString();
    }
}
