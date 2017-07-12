using System;
using System.Collections.Generic;
using System.Text;

namespace VoidLibrary.Utils.Storage
{
    public interface SharedPreferences
    {
        //Map<String, ?> getAll();

        string GetString(string key, string defValue);

        int GetInt(string key, int defValue);

        long GetLong(string key, long defValue);

        float GetFloat(string key, float defValue);

        bool GetBoolean(string key, bool defValue);

        int[] GetIntArray(string key, int[] defValue);

        string[] GetStrArray(string key, string[] defValue);

        Editor Edit();
    }

    public interface Editor
    {
        Editor PutString(string key, string value);

        Editor PutInt(string key, int value);

        Editor PutLong(string key, long value);

        Editor PutFloat(string key, float value);

        Editor PutBoolean(string key, bool value);

        Editor PutIntArray(string key, int[] intArray);

        Editor PutStrArray(string key, string[] intArray);

        Editor Remove(String key);

        Editor Clear();

        bool Commit();

        void Apply();
    }
}
