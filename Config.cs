using System;
using System.Globalization;
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

    public Config(string configPath, string section = "default")
    {
        Path = configPath;
        Section = section;
    }

    public void WriteString(string key, string value)
    {
        WritePrivateProfileString(Section, key.ToLower(), value, Path);
    }

    public void WriteInt(string key, int value)
    {
        WriteString(key, value.ToString());
    }

    public void WriteFloat(string key, float value)
    {
        WriteString(key, value.ToString(CultureInfo.InvariantCulture));
    }

    public void WriteBool(string key, bool value)
    {
        WriteString(key, value.ToString());
    }

    public string ReadString(string key, string defaultValue = "")
    {
        try
        {
            var temp = new StringBuilder(255);
            GetPrivateProfileString(Section, key.ToLower(), defaultValue, temp, 255, Path);
            return temp.ToString();
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    public int ReadInt(string key, int defaultValue = 0)
    {
        try
        {
            return int.Parse(ReadString(key));
        }
        catch (Exception)
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
        catch (Exception)
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
        catch (Exception)
        {
            return defaultValue;
        }
    }
}
