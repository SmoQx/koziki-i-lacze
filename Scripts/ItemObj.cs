using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemObj : MonoBehaviour
{
   public int intValue;
   public TMP_Text display_text;

   void Start()
   {
        display_text.text = intValue.ToString();
   }

   void Update()
   {
        display_text.text = intValue.ToString();
   }
}
