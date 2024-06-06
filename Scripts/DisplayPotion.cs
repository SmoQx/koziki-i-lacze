using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DisplayPotion : MonoBehaviour
{
    SaveLoadFile io = new SaveLoadFile();
    string player_data_file = "user_data.json";
    JObject player_data;
    // Start is called before the first frame update
    public UnityEngine.UI.Image targetImage;
    public Sprite[] images;

    void Start()
    {
        player_data = JObject.Parse(io.Load_to_file(player_data_file));
        if (player_data["ItemsList"]["Potion"].ToString() == "RedPotion")
        {
            targetImage.sprite = images[0];
        }
        else if (player_data["ItemsList"]["Potion"].ToString() == "BluePotion")
        {
            targetImage.sprite = images[1];
        }
        else if (player_data["ItemsList"]["Potion"].ToString() == "YellowPotion")
        {
            targetImage.sprite = images[2];
        }
        else
            targetImage.sprite = images[3];
    }

    // Update is called once per frame
    void Update()
    {
        player_data = JObject.Parse(io.Load_to_file(player_data_file));
        if (player_data["ItemsList"]["Potion"].ToString() == "RedPotion")
        {
            targetImage.sprite = images[0];
        }
        else if (player_data["ItemsList"]["Potion"].ToString() == "BluePotion")
        {
            targetImage.sprite = images[1];
        }
        else if (player_data["ItemsList"]["Potion"].ToString() == "YellowPotion")
        {
            targetImage.sprite = images[2];
        }
        else
            targetImage.sprite = images[3];
    }
}
