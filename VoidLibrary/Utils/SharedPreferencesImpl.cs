using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using VoidLibrary.Utils.Storage;
using System.IO;
using System.Collections;

namespace VoidLibrary.Utils
{
    public class SharedPreferencesImpl : SharedPreferences
    {
        private string path;
        private string backupPath;

        public SharedPreferencesImpl(string name)
        {
            path = GetSharedPrefsPath(name);
            backupPath = MakeBackupPath(path);
            Console.WriteLine(path);
            Console.WriteLine(backupPath);
        }

        private static string GetWorkDirectory()
        {
            try
            {
                return Path.GetDirectoryName(typeof(SharedPreferencesImpl).Assembly.Location);
            }
            catch
            {
                return System.Environment.CurrentDirectory;
            }
        }

        private static string GetSharedPrefsPath(String name)
        {
            string workDir = GetWorkDirectory();
            string sharedprefsDir = Path.Combine(workDir, "shared_prefs");
            if (!Directory.Exists(sharedprefsDir))
            {
                Directory.CreateDirectory(sharedprefsDir);
                if (!Directory.Exists(sharedprefsDir)){
                    FileLog.WriteE("Create Directory Failed!");
                }
            }

            return Path.Combine(sharedprefsDir, name + ".xml");
        }

        private static string MakeBackupPath(string path)
        {
            return path + ".bak";
        }

        string SharedPreferences.GetString(string key, string defValue)
        {
            string value = GetConfigData(key, defValue);
            return value;
        }

        int SharedPreferences.GetInt(string key, int defValue)
        {
            string value = GetConfigData(key, defValue.ToString());
            int parseValue;
            if (!int.TryParse(value, out parseValue))
            {
                parseValue = defValue;
            }
            return parseValue;
        }

        long SharedPreferences.GetLong(string key, long defValue)
        {
            string value = GetConfigData(key, defValue.ToString());
            long parseValue;
            if (!long.TryParse(value, out parseValue))
            {
                parseValue = defValue;
            }
            return parseValue;
        }

        float SharedPreferences.GetFloat(string key, float defValue)
        {
            string value = GetConfigData(key, defValue.ToString());
            float parseValue;
            if (!float.TryParse(value, out parseValue))
            {
                parseValue = defValue;
            }
            return parseValue;
        }

        bool SharedPreferences.GetBoolean(string key, bool defValue)
        {
            string value = GetConfigData(key, defValue.ToString());
            bool parseValue;
            if (!bool.TryParse(value, out parseValue))
            {
                parseValue = defValue;
            }
            return parseValue;
        }

        int[] SharedPreferences.GetIntArray(string key, int[] defValue)
        {
            string value = GetConfigData(key, EditorImpl.ArrayToStr(defValue));
            return StrToArray(value);
        }

        string[] SharedPreferences.GetStrArray(string key, string[] defValue)
        {
            string value = GetConfigData(key, EditorImpl.ArrayToStr(defValue));
            return value.Split(',');
        }

        int[] StrToArray(string strArray)
        {
            string[] arrays = strArray.Split(',');
            int[] outArray = new int[arrays.Length];
            for (int i = arrays.Length - 1; i >= 0; --i)
            {
                int.TryParse(arrays[i], out outArray[i]);
            }
            return outArray;
        }

        private static void RestoreFile(string path, string backupPath)
        {
            if (File.Exists(backupPath))
            {
                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    File.Move(backupPath, path);
                }
                catch (Exception move_e)
                {
                    FileLog.WriteE("Path:" + Path.GetFileName(path) + " Restore File From Backup Failed! Error:" + move_e.Message);
                }
            }
        }

        private string GetConfigData(string szKeyName, string szDefaultValue)
        {
            string lower_key = ToStandardKey(szKeyName);

            string szConfigFile = path;
            RestoreFile(path, backupPath);
            if (!File.Exists(szConfigFile))
            {
                return szDefaultValue;
            }

            XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);
            if (clsXmlDoc == null)
                return szDefaultValue;

            string szXPath = string.Format(".//key[@name='{0}']", lower_key);
            XmlNode clsXmlNode = SelectXmlNode(clsXmlDoc, szXPath);
            if (clsXmlNode == null)
            {
                return szDefaultValue;
            }

            XmlNode clsValueAttr = clsXmlNode.Attributes.GetNamedItem("value");
            if (clsValueAttr == null)
                return szDefaultValue;
            return clsValueAttr.Value;
        }

        private static XmlDocument GetXmlDocument(string szXmlFile)
        {
            if (IsEmptyString(szXmlFile))
                return null;
            if (!File.Exists(szXmlFile))
                return null;
            XmlDocument clsXmlDoc = new XmlDocument();
            try
            {
                clsXmlDoc.Load(szXmlFile);
            }
            catch
            {
                return null;
            }
            return clsXmlDoc;
        }

        private static XmlNode SelectXmlNode(XmlNode clsRootNode, string szXPath)
        {
            if (clsRootNode == null || IsEmptyString(szXPath))
                return null;
            try
            {
                return clsRootNode.SelectSingleNode(szXPath);
            }
            catch
            {
                return null;
            }
        }

        private static bool IsEmptyString(string szString)
        {
            if (szString == null)
                return true;
            if (szString.Trim() == string.Empty)
                return true;
            return false;
        }

        private static string ToStandardKey(string key)
        {
            return key.ToLower();
        }

        Editor SharedPreferences.Edit()
        {
            return new EditorImpl(path, backupPath);
        }

        private class EditorImpl : Editor
        {
            private Editor Editor { get { return this; } }

            Hashtable mModified = new Hashtable();
            bool mClear = false;

            string path;
            string backupPath;

            public EditorImpl(string path, string backupPath)
            {
                this.path = path;
                this.backupPath = backupPath;
            }

            Editor Editor.PutString(string key, string value)
            {
                string lower_key = ToStandardKey(key);
                lock (this)
                {
                    if (mModified.Contains(lower_key))
                    {
                        mModified.Remove(lower_key);
                    }
                    mModified.Add(lower_key, value);
                }
                return this;
            }

            Editor Editor.PutInt(string key, int value)
            {
                return Editor.PutString(key, value.ToString());
            }

            Editor Editor.PutLong(string key, long value)
            {
                return Editor.PutString(key, value.ToString());
            }

            Editor Editor.PutFloat(string key, float value)
            {
                return Editor.PutString(key, value.ToString());
            }

            Editor Editor.PutBoolean(string key, bool value)
            {
                return Editor.PutString(key, value.ToString());
            }

            Editor Editor.PutIntArray(string key, int[] intArray)
            {
                return Editor.PutString(key, ArrayToStr(intArray));
            }

            Editor Editor.PutStrArray(string key, string[] strArray)
            {
                return Editor.PutString(key, ArrayToStr(strArray));
            }

            Editor Editor.Remove(string key)
            {
                string lower_key = ToStandardKey(key);
                lock (this)
                {
                    if (mModified.Contains(lower_key))
                    {
                        mModified.Remove(lower_key);
                    }
                    mModified.Add(lower_key, this);
                }
                return this;
            }

            Editor Editor.Clear()
            {
                throw new NotImplementedException();
                mClear = true;
                return this;
            }

            bool Editor.Commit()
            {
                RestoreFile(path, backupPath);
                string szConfigFile = path;
                if (!File.Exists(szConfigFile))
                {
                    if (!CreateXmlFile(szConfigFile, "map"))
                        return false;
                }
                else
                {
                    try
                    {
                        File.Copy(path, backupPath, true);
                    }
                    catch (Exception e)
                    {
                        FileLog.WriteE("Path:" + Path.GetFileName(path) + " BackUp Failed! Path:" + backupPath + " Error:" + e.Message);
                    }
                }
                XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);

                lock (this)
                {
                    bool remove;
                    foreach (DictionaryEntry entry in mModified)
                    {
                        string key = entry.Key.ToString();
                        string value = entry.Value.ToString();
                        remove = entry.Value is EditorImpl;

                        string szXPath = string.Format(".//key[@name='{0}']", key);
                        XmlNode clsXmlNode = SelectXmlNode(clsXmlDoc, szXPath);

                        if (remove)
                        {
                            if (clsXmlNode != null)
                            {
                                RemoveXmlNode(clsXmlNode);
                            }
                            continue;
                        }
                        if (clsXmlNode == null)
                        {
                            clsXmlNode = CreateXmlNode(clsXmlDoc, "key");
                        }
                        if (!SetXmlAttr(clsXmlNode, "name", key))
                            continue;
                        if (!SetXmlAttr(clsXmlNode, "value", value))
                            continue;
                    }
                    mModified.Clear();
                }

                bool savedSuccess = SaveXmlDocument(clsXmlDoc, szConfigFile);
                if (savedSuccess)
                {
                    if (File.Exists(backupPath))
                    {
                        File.Delete(backupPath);
                    }
                }

                return savedSuccess;
            }

            void Editor.Apply()
            {
                throw new NotImplementedException();
            }

            private static bool CreateXmlFile(string szFileName, string szRootName)
            {
                if (szFileName == null || szFileName.Trim() == "")
                    return false;
                if (szRootName == null || szRootName.Trim() == "")
                    return false;

                XmlDocument clsXmlDoc = new XmlDocument();
                clsXmlDoc.AppendChild(clsXmlDoc.CreateXmlDeclaration("1.0", Encoding.UTF8.BodyName, null));
                clsXmlDoc.AppendChild(clsXmlDoc.CreateNode(XmlNodeType.Element, szRootName, ""));
                try
                {
                    clsXmlDoc.Save(szFileName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private static XmlNode CreateXmlNode(XmlNode clsParentNode, string szNodeName)
            {
                try
                {
                    XmlDocument clsXmlDoc = null;
                    if (clsParentNode.GetType() != typeof(XmlDocument))
                        clsXmlDoc = clsParentNode.OwnerDocument;
                    else
                        clsXmlDoc = clsParentNode as XmlDocument;
                    XmlNode clsXmlNode = clsXmlDoc.CreateNode(XmlNodeType.Element, szNodeName, string.Empty);
                    if (clsParentNode.GetType() == typeof(XmlDocument))
                    {
                        clsXmlDoc.LastChild.AppendChild(clsXmlNode);
                    }
                    else
                    {
                        clsParentNode.AppendChild(clsXmlNode);
                    }
                    return clsXmlNode;
                }
                catch
                {
                    return null;
                }
            }

            private static bool SetXmlAttr(XmlNode clsXmlNode, string szAttrName, string szAttrValue)
            {
                if (clsXmlNode == null)
                    return false;
                if (IsEmptyString(szAttrName))
                    return false;
                if (IsEmptyString(szAttrValue))
                    szAttrValue = string.Empty;
                XmlAttribute clsAttrNode = clsXmlNode.Attributes.GetNamedItem(szAttrName) as XmlAttribute;
                if (clsAttrNode == null)
                {
                    XmlDocument clsXmlDoc = clsXmlNode.OwnerDocument;
                    if (clsXmlDoc == null)
                        return false;
                    clsAttrNode = clsXmlDoc.CreateAttribute(szAttrName);
                    clsXmlNode.Attributes.Append(clsAttrNode);
                }
                clsAttrNode.Value = szAttrValue;
                return true;
            }

            private static bool RemoveXmlNode(XmlNode clsXmlNode)
            {
                if (clsXmlNode == null)
                {
                    return false;
                }
                if (clsXmlNode.ParentNode != null)
                {
                    XmlNode keyNode = clsXmlNode.Attributes.GetNamedItem("name");
                    string name = "not found";
                    try {
                        if (keyNode != null)
                        {
                            name = keyNode.Value;
                        }
                        clsXmlNode.ParentNode.RemoveChild(clsXmlNode);
                        FileLog.WriteE("Remove " + name);
                    }
                    catch (ArgumentException arg_e)
                    {
                        FileLog.WriteE("Remove " + name + " Failed! Error:" + arg_e.Message);
                    }
                }
                else
                {
                    clsXmlNode.RemoveAll();
                }
                return true;
            }

            private static bool SaveXmlDocument(XmlDocument clsXmlDoc, string szXmlFile)
            {
                if (clsXmlDoc == null)
                    return false;
                if (IsEmptyString(szXmlFile))
                    return false;
                try
                {
                    if (File.Exists(szXmlFile))
                        File.Delete(szXmlFile);
                }
                catch
                {
                    return false;
                }
                try
                {
                    clsXmlDoc.Save(szXmlFile);
                }
                catch
                {
                    return false;
                }
                return true;
            }

            public static string ArrayToStr(int[] array)
            {
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; ++i)
                {
                    strBuilder.Append(array[i]);
                    if (i + 1 != array.Length)
                    {
                        strBuilder.Append(',');
                    }
                }
                return strBuilder.ToString();
            }

            public static string ArrayToStr(string[] array)
            {
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < array.Length; ++i)
                {
                    strBuilder.Append(array[i]);
                    if (i + 1 != array.Length)
                    {
                        strBuilder.Append(',');
                    }
                }
                return strBuilder.ToString();
            }

        }
    }
}
