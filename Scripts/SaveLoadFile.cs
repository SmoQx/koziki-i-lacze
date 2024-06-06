using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadFile : MonoBehaviour
{   

    [ContextMenu("Save_to_file")]
    public void Save_to_file(string tekst, string filePath)
    {    
        try
        {
            File.WriteAllText(filePath, contents: tekst);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public string Load_to_file(string filePath)
    {
        try
        {
            string what_was_read = File.ReadAllText(filePath);
            return what_was_read;
        }   
        catch (Exception ex)    
        {
            Debug.Log(ex);
            return "error";
        }
    }
}
