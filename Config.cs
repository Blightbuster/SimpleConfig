﻿using System;
using System.Runtime.InteropServices;
using System.Text;

class Config
{
    public string Path;
    public string Section;

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


    public Config(string configPath)
    {
        Path = configPath;
    }

    public void WriteString(string key, string value)
    {
        WritePrivateProfileString(Section, key.ToLower(), value, this.Path);
    }

    public void WriteInt(string key, int value)
    {
        WriteString(key, value.ToString());
    }

    public void WriteFloat(string key, float value)
    {
        WriteString(key, value.ToString());
    }

    public void WriteBool(string key, bool value)
    {
        WriteString(key, value.ToString());
    }

    public string ReadString(string key, string defaultValue = "")
    {
        try
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(Section, key.ToLower(), "", temp, 255, this.Path);
            return temp.ToString();
        }
        catch (Exception e)
        {
            return defaultValue;
        }
    }

    public int ReadInt(string key, int defaultValue = 0)
    {
        try
        {
            return Int32.Parse(ReadString(key));
        }
        catch (Exception e)
        {
            return defaultValue;
        }
    }

    public float ReadFloat(string key, float defaultValue = 0)
    {
        try
        {
            return float.Parse(ReadString(key));
        }
        catch (Exception e)
        {
            return defaultValue;
        }
    }

    public bool ReadBool(string key, bool defaultValue = false)
    {
        try
        {
            return bool.Parse(ReadString(key));
        }
        catch (Exception e)
        {
            return defaultValue;
        }
    }
}