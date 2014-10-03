using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web;
using System.Runtime.InteropServices;
using Network;
using System.Security.Cryptography;
using System.IO;
using Utils;
using System.Net;


namespace cTlauncher
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Properties
        public static MainWindow mainWindow;
        public static Config launcherConfig;
        public static bool isLoggedIn;
        public static WebServer ws;

        #endregion Properties

        #region MainProc
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            this.Title = "[Tera-Online] {ProjectS1.Launcher}";
            launcherConfig = new Config();
        }
        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            playButton.Visibility = Visibility.Hidden;
            launcherConfig.CheckGameFile();
            launcherConfig.Init();
            isLoggedIn = false;
        }
        #endregion MainProc

        #region Funcs
        private void languageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            launcherConfig.SaveLanguage();
            PrintMessage("Language: " + launcherConfig.Language + ", Id: " + launcherConfig.LanguageId + ", languageGame: " + launcherConfig.LanguageGame);
        }
        public void PrintMessage(string Message)
        {
            string Time = DateTime.Now.ToString("HH:mm:ss") + " | " + DateTime.Now.ToShortTimeString();
            int threadId = Thread.CurrentThread.ManagedThreadId;

            Dispatcher.Invoke(new Action(delegate
            {
                _scrollviewerStatus.Content += (Time + " | " + threadId + " | " + Message + "\n");
                _scrollviewerStatus.ScrollToEnd();
            }));
        }
        private void _inputUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            this._inputUsername.Text = "";
        }
        private void _inputPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            this._inputPassword.Password = "";
        }
        #endregion Funcs

        #region Buttons
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (_inputUsername.Text == "" || _inputPassword.Password == "")
                return;

            if (!isLoggedIn)
            {
                String username = _inputUsername.Text;
                String password = Crypt.StringToMD5(_inputPassword.Password);
                string authString = "&" + OpCode.LoginPacket.GetHashCode() + "&" + username + "&" + password;

                PrintMessage("Connecting to Login-Server: " + launcherConfig.ServerIP + ":" + launcherConfig.ServerPort);
                PrintMessage("Checking Username...");
               
                //Threaded Login try...
                Thread thread = new Thread(delegate() { threadedLogin(authString); });
                thread.Start();
            }
        }
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (_inputUsername.Text == "" || _inputPassword.Password == "")
                return;

            if (!isLoggedIn)
            {
                String username = _inputUsername.Text;
                String password = Crypt.StringToMD5(_inputPassword.Password);
                string regString = "&" + OpCode.RegisterPacket.GetHashCode() + "&" + username + "&" + password;

                PrintMessage("Connecting to Login-Server: " + launcherConfig.ServerIP + ":" + launcherConfig.ServerPort);
                PrintMessage("Checking your data...");
                //Threaded Login try...
                Thread thread = new Thread(delegate() { threadedLogin(regString); });
                thread.Start();
            }
        }
        public void threadedLogin(string inputString)
        {
            try
            {
                //Connect to Server
                if (ClientConnection.CheckConnectionMessage(inputString))
                {
                    Dispatcher.Invoke(new Action(delegate
                    {
                        isLoggedIn = true;//set this to true for running game!
                        loginButton.Visibility = Visibility.Hidden;
                        playButton.Visibility = Visibility.Visible;
                        registerButton.Visibility = Visibility.Hidden;
                        _inputUsername.IsEnabled = false;
                        _inputPassword.IsEnabled = false;
                        //PrintMessage("Authorization successfull, Starting Game...");
                        //this.Content = "Authorization successfull, Starting Game...";//set for screen text only
                    }));
                }
            }
            catch 
            { 
                PrintMessage("Connection Error! is the LoginServer Online?");
            }

        }
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (isLoggedIn)
            {
                try
                {
                    ws = new WebServer(SendServerListResponse, Config.sl_FileToLoad);
                    ws.Run();
                    PrintMessage("WebService started!.");
                    Thread.Sleep(1000);

                    //Login Data...
                    int unk1 = 1; //access level?
                    String password = Crypt.StringToMD5(_inputPassword.Password);
                    int unk2 = 0; //language long or lastserverid?
                    int unk3 = 0; //lastserverid or user perm?
                    String username = _inputUsername.Text;
                    string language = launcherConfig.LanguageGame;
                    string space = " ";

                    //Arguments = accessLevel + " " + PasswordHash + " " + LanguageLong + " " + lastServerId + " " + userPerm + " " + AccountName + "," + LanguageShort
                    //string LaunchString = " 1 " + password + " 0 0 " + username + " " + launcherConfig.LanguageGame;
                    string LaunchString = space + unk1 + space + password + space + unk2 + space + unk3 + space + username + space + language;

                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = Config.CurrentGameFile;
                    startInfo.Arguments = LaunchString;
                    startInfo.UseShellExecute = false;
                    startInfo.CreateNoWindow = true;
                    startInfo.RedirectStandardOutput = true;
                    startInfo.RedirectStandardError = true;
                    startInfo.RedirectStandardInput = true;
                    Process.Start(startInfo);

                    //wait for tera exe and exit...
                    PrintMessage("Waiting for Game...");                    
                    Thread.Sleep(1000);
     
                    PrintMessage("WebService stopped!.");
                    ws.Stop();
                    Thread.Sleep(1000);

                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    PrintMessage("Error: Could not start the Game Exe\n" + ex.ToString());
                    Environment.Exit(0);
                }
            }
        }

        #endregion Buttons

        #region Serverlist
        public static string SendServerListResponse(HttpListenerRequest request)
        {
            return string.Format(GetServerList());
        }
        public static string GetServerList()
        {
            string result = ReadServerListFile();
            return string.Format(result);
        }
        public static string ReadServerListFile()
        {
            string result;
            using (StreamReader streamReader = new StreamReader(Config.sl_FileToRead, UTF8Encoding.UTF8))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }


        #endregion Serverlist

    }

}
