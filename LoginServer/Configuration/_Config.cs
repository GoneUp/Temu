using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevTera;
using Utils;
using Utils.Logger;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml;
using System.Data;
using System.Xml.Serialization;
using System.Reflection;
using Database_Manager.Database;

namespace Configuration
{
    public class Config
    {
        #region properties
        public static string lsFileType = "conf.";
        public static string lsFileName = "loginserver";
        public static string lsPath = Path.Combine("Configuration", string.Format("{0}.xml", lsFileType + lsFileName));
        #endregion properties

        #region Functions
        public static void Init_LS_Config()
        {
            try { LoginServer.loginserverConfig.Init(); Logger.WriteLine(LogState.Info, "Server Configuration initialized!"); }
            catch (Exception) { }
        }
        public static void Init_LOG_Config()
        {
            try { LoginServer.logger.Init(); Logger.WriteLine(LogState.Info, "Logger Configuration initialized!"); }
            catch (Exception) { }
        }
        public static void Init_DB_Config()
        {
            try { LoginServer.dbs.Init(); Logger.WriteLine(LogState.Info, "DatabaseSystem initialized!"); }
            catch (Exception) { }
        }
        #endregion Functions

        #region Serialize/Deserialize
        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        #endregion Serialize/Deserialize

        #region AddonFunctions
        public static void CreateDefaultFolder()
        {
            string folder = "Configuration";
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
        #endregion AddonFunctions

    }
}
