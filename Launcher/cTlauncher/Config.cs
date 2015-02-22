using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace Tera
{
    public class Config
    {
        #region Properties
        public static string CurrentDirectory = Environment.CurrentDirectory + @"\";
        public static string CurrentDataDirectory = CurrentDirectory + @"data\";
        public static string CurrentSettingsFile = CurrentDataDirectory + "conf.cTlauncher.xml";
        public static string CurrentGameFile = CurrentDirectory + "cTgame.clf";
        public static string ServerlistLocal;
        public static string ServerlistWeb;
        public static string sl_FileToRead;
        public static string sl_FileToLoad;


        string _ServerIP;
        int _ServerPort;
        string _Language;
        string _LanguageGame;
        int _LanguageId;

        public Config()
        {
            _ServerIP = "127.0.0.1";
            _ServerPort = 2101;
            _Language = "English";
            _LanguageGame = "uk";
            _LanguageId = 1;
        }
        public string ServerIP
        {
            get { return _ServerIP; }
            set { _ServerIP = value; }
        }
        public int ServerPort
        {
            get { return _ServerPort; }
            set { _ServerPort = value; }
        }
        public string Language
        {
            get { return _Language; }
            set { _Language = value; }
        }
        public string LanguageGame
        {
            get { return _LanguageGame; }
            set { _LanguageGame = value; }
        }
        public int LanguageId
        {
            get { return _LanguageId; }
            set { _LanguageId = value; }
        }
        #endregion Properties

        #region Init
        public void Init()
        {
            //Add Combobox Language items
            MainWindow.mainWindow.languageComboBox.Items.Clear();
            MainWindow.mainWindow.languageComboBox.Items.Add(Languages.English.ToString());
            MainWindow.mainWindow.languageComboBox.Items.Add(Languages.French.ToString());
            MainWindow.mainWindow.languageComboBox.Items.Add(Languages.German.ToString());

            if (File.Exists(Config.CurrentSettingsFile))//Config File
            {
                //Read the configuration object from a file
                MainWindow.launcherConfig = Config.ReadFromXmlFile<Config>(Config.CurrentSettingsFile);
                //read out the variables from file
                ServerIP = MainWindow.launcherConfig.ServerIP;
                ServerPort = MainWindow.launcherConfig.ServerPort;
                Language = MainWindow.launcherConfig.Language;
                LanguageGame = MainWindow.launcherConfig.LanguageGame;
                LanguageId = MainWindow.launcherConfig.LanguageId;
            }
            else
            {
                //Create a new Configuration Folder
                if (!Directory.Exists(Config.CurrentDataDirectory)) { Directory.CreateDirectory(CurrentDataDirectory); }
                // Create a new configuration object and initialize some variables
                MainWindow.launcherConfig.ServerIP = _ServerIP;
                MainWindow.launcherConfig.ServerPort = _ServerPort;
                MainWindow.launcherConfig.Language = _Language;
                MainWindow.launcherConfig.LanguageGame = _LanguageGame;
                MainWindow.launcherConfig.LanguageId = _LanguageId;
                // Serialize the configuration object to a file
                Config.WriteToXmlFile<Config>(Config.CurrentSettingsFile, MainWindow.launcherConfig);
            }

            SwitchLanguage();
            PrintMessage("Server:" + MainWindow.launcherConfig.ServerIP + "," + " ServerPort: " + MainWindow.launcherConfig.ServerPort);
        }
        #endregion Init

        #region Language
        public void SaveLanguage()
        {
            try
            {
                MainWindow.launcherConfig.Language = MainWindow.mainWindow.languageComboBox.SelectedValue.ToString();
                
                SwitchLanguage();
                // Create a new configuration object and initialize some variables
                MainWindow.launcherConfig.ServerIP = _ServerIP;
                MainWindow.launcherConfig.ServerPort = _ServerPort;
                MainWindow.launcherConfig.Language = _Language;
                MainWindow.launcherConfig.LanguageGame = _LanguageGame;
                MainWindow.launcherConfig.LanguageId = _LanguageId;
                // Serialize the configuration object to a file
                Config.WriteToXmlFile<Config>(Config.CurrentSettingsFile, MainWindow.launcherConfig);
            }
            catch//(Exception ex)
            {
                PrintMessage("Error: Saving to Config File!");
                //PrintMessage("Exception: " + ex.ToString());
            }
        }
        public void SwitchLanguage()
        {
            switch (MainWindow.launcherConfig.Language)
            {
                case "":
                    MainWindow.launcherConfig.Language = Languages.English.ToString();
                    MainWindow.launcherConfig.LanguageGame = "uk";
                    MainWindow.launcherConfig.LanguageId = Languages.English.GetHashCode();
                    break;
                case "English":
                    MainWindow.launcherConfig.Language = Languages.English.ToString();
                    MainWindow.launcherConfig.LanguageGame = "uk";
                    MainWindow.launcherConfig.LanguageId = Languages.English.GetHashCode();
                    sl_FileToLoad = "http://127.0.0.1:12345/server/serverlist.uk/";
                    sl_FileToRead = CurrentDataDirectory + @"serverlist.uk";
                    break;
                case "French":
                    MainWindow.launcherConfig.Language = Languages.French.ToString();
                    MainWindow.launcherConfig.LanguageGame = "fr";
                    MainWindow.launcherConfig.LanguageId = Languages.French.GetHashCode();
                    sl_FileToLoad = "http://127.0.0.1:12345/server/serverlist.fr/";
                    sl_FileToRead = CurrentDataDirectory + @"serverlist.fr";
                    break;
                case "German":
                    MainWindow.launcherConfig.Language = Languages.German.ToString();
                    MainWindow.launcherConfig.LanguageGame = "de";
                    MainWindow.launcherConfig.LanguageId = Languages.German.GetHashCode();
                    sl_FileToLoad = "http://127.0.0.1:12345/server/serverlist.de/";
                    sl_FileToRead = CurrentDataDirectory + @"serverlist.de";
                    break;
            };

            MainWindow.mainWindow.languageComboBox.SelectedItem = MainWindow.launcherConfig.Language;
            MainWindow.mainWindow.languageImage.Source = new BitmapImage(new Uri(CurrentDataDirectory + MainWindow.launcherConfig.LanguageGame + ".gif"));
        }
        #endregion

        #region Functions
        public static void PrintMessage(string Message)
        {
            MainWindow.mainWindow.PrintMessage(Message);
        }
        public void CheckGameFile()
        {
            PrintMessage("GameFile: Checking " + CurrentGameFile);

            if (File.Exists(CurrentGameFile) &&
                File.ReadAllText(CurrentGameFile) != "")
            {
                PrintMessage("GameFile: OK!");
                return;
            }
            else
            {
                PrintMessage("GameFile: CORRUPT!, Startup Repair!");

                if (File.Exists(CurrentGameFile) &&
                    File.ReadAllText(CurrentGameFile) == "")
                {
                    File.Delete(CurrentGameFile);
                }
                //Create new GameExe
                ByteArrayToFile(CurrentGameFile, ExtractResource("cTlauncher.Resources.cTbin.clf"));

                PrintMessage("GameFile: OK!, Repair Done!");
            }
        }
        public byte[] ExtractResource(String filename)
        {
            Assembly assemblys = Assembly.GetExecutingAssembly();
            foreach (string resources in assemblys.GetManifestResourceNames())
            {
                //lookup resources
                //PrintMessage(reso.ToString());
            }
            try
            {
                using (Stream resourceFilestream = assemblys.GetManifestResourceStream(filename))
                {
                    if (resourceFilestream == null) return null;
                    byte[] ba = new byte[resourceFilestream.Length];
                    resourceFilestream.Read(ba, 0, ba.Length);
                    return ba;
                }
            }
            catch//(Exception ex)
            {
                // Error
                PrintMessage("Error: Extracting Game File!");
                //PrintMessage("Exception: " + ex.ToString());
                return null;
            }
        }
        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                FileStream _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.ReadWrite);
                // Writes a block of bytes to this stream using data from a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);
                // close file stream
                _FileStream.Close();
                return true;
            }
            catch//(Exception ex)
            {
                //Error
                PrintMessage("Error: Saving New Game File!");
                //PrintMessage("Exception: " + e.ToString());
            }
            // error occured, return false
            return false;
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
    }

    public enum Languages
    {
        English = 1,
        French = 2,
        German = 3
    };
}
