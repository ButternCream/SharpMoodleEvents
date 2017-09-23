using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using HtmlAgilityPack;
using System.Threading;

namespace Moodle_Events
{
    public partial class Main : Form
    {

        private static String ACCOUNT_SID;  
        private static String AUTH_TOKEN;   
        private static String TWILIO_NUMBER;
        private static String CELL_NUMBER;
        private static String USERNAME;
        private static String PASSWORD;

        private static volatile bool running = false;

        Thread checkTexts;

        public Main()
        {
            InitializeComponent();
            textUsername.Text = Properties.Settings.Default.Username;
            textPassword.Text = Properties.Settings.Default.Password;
            ACCOUNT_SID = Properties.Settings.Default.Account_SID;
            AUTH_TOKEN = Properties.Settings.Default.Auth_Token;
            TWILIO_NUMBER = Properties.Settings.Default.Twilio_Number;
            CELL_NUMBER = Properties.Settings.Default.Cell_Number;
            if (ACCOUNT_SID != String.Empty && AUTH_TOKEN != String.Empty)
            {
                TwilioClient.Init(ACCOUNT_SID, AUTH_TOKEN);
            }
        }

        public static void check()
        {
            var messages = MessageResource.Read();
            foreach(var m in messages)
            {
                var msg_lower = m.Body.ToLower();
                if (m.From.ToString() == CELL_NUMBER && (msg_lower.Contains("homework") || msg_lower.Contains("hw")))
                {
                    get_assignments();
                    MessageResource.Update(m.Sid, "");
                    return;
                }
            }
        }

        public static void get_assignments()
        {
            var text = get_events();
            send_text(text, new PhoneNumber(CELL_NUMBER));
        }

        public static void send_text(String text, PhoneNumber number)
        {
            var messasge = MessageResource.Create(
                    to: number,
                    from: new PhoneNumber(TWILIO_NUMBER),
                    body: text);
        }

        public static String format_assignments(Dictionary<String, List<String>> assignments, Dictionary<String, String> dates)
        {
            String formatted = "Assignments\n";
            foreach(var class_ in assignments)
            {
                formatted += "Class: " + class_.Key + "\n";
                foreach(var title in assignments[class_.Key])
                {
                    formatted += " - " + title + " (Due: " + dates[class_.Key + title] + ")\n";
                }
                formatted += "\n";
            }
            return formatted;
        }


        public static HtmlAgilityPack.HtmlDocument get_html()
        {
            String URL = "https://moodle.sonoma.edu/C/calendar/view.php";
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            ChromeDriver driver = new ChromeDriver(options);
            driver.Url = "http://login.sonoma.edu";
            Thread.Sleep(2000);
            IWebElement username = driver.FindElementById("username");
            IWebElement pass = driver.FindElementById("password");

            username.SendKeys(USERNAME);
            pass.SendKeys(PASSWORD);

            driver.FindElementByName("submit").Click();
            Thread.Sleep(2000);
            driver.Url = "https://moodle.sonoma.edu/C/login/";
            Thread.Sleep(2000);
            driver.FindElementByLinkText("CAS users").Click();
            driver.Url = URL;
            Thread.Sleep(2000);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(driver.PageSource);
            driver.Quit();
            return doc;
        }


        public static String get_events()
        {
            Dictionary<String, List<String>> assignments = new Dictionary<String, List<String>> {};
            Dictionary<String, String> dates = new Dictionary<String, String> {};
            HtmlAgilityPack.HtmlDocument doc = get_html();
            var events = doc.DocumentNode
                .Descendants("div")
                .Where(d =>
                    d.Attributes.Contains("class")
                    &&
                    d.Attributes["class"].Value.Contains("event")
                    );
            foreach(var e in events)
            {
                var title = e.Descendants("h3")
                    .Where(t =>
                        t.Attributes.Contains("class")
                        &&
                        t.Attributes["class"].Value.Contains("referer")
                        ).ElementAt(0);
                var course = e.Descendants("div")
                    .Where(c =>
                        c.Attributes.Contains("class")
                        &&
                        c.Attributes["class"].Value.Contains("course")
                    ).ElementAt(0);
                var d = e.Descendants("span")
                    .Where(date =>
                        date.Attributes.Contains("class")
                        &&
                        date.Attributes["class"].Value.Contains("date")
                        ).ElementAt(0);
                if (!dates.ContainsKey(course.InnerText + title.InnerText))
                {
                    dates.Add(course.InnerText + title.InnerText, d.InnerText);
                }
                if (!assignments.ContainsKey(course.InnerText))
                {
                    assignments.Add(course.InnerText, new List<String> { title.InnerText });
                }
                else if (!assignments[course.InnerText].Contains(title.InnerText))
                {
                    assignments[course.InnerText].Add(title.InnerText);
                }
            }
            Console.WriteLine(format_assignments(assignments, dates));
            return format_assignments(assignments, dates);
        }

        public static void Init()
        {
            Console.WriteLine("Checking texts every minute!");
            while (running)
            {
                Console.WriteLine("Checking...");
                check();
                Thread.Sleep(1000*60);
            }
            Console.WriteLine("Thread Ended");
        }

        private void btnSendText_Click(object sender, EventArgs e)
        {
            if (USERNAME == String.Empty || PASSWORD == String.Empty)
            {
                MessageBox.Show("Username and Password must not be empty.");
                return;
            }
            if (AUTH_TOKEN == String.Empty || ACCOUNT_SID == String.Empty || CELL_NUMBER == String.Empty || TWILIO_NUMBER == String.Empty)
            {
                if (MessageBox.Show("Settings are missing, please fill out all the fields!") == DialogResult.OK)
                {
                    Settings window = new Settings();
                    window.ShowDialog();
                    ACCOUNT_SID = Properties.Settings.Default.Account_SID;
                    AUTH_TOKEN = Properties.Settings.Default.Auth_Token;
                    TWILIO_NUMBER = Properties.Settings.Default.Twilio_Number;
                    CELL_NUMBER = Properties.Settings.Default.Cell_Number;
                    TwilioClient.Init(ACCOUNT_SID, AUTH_TOKEN);
                    return;
                }
                
            }

            // Save last session Username and Pass
            Properties.Settings.Default.Username = textUsername.Text;
            Properties.Settings.Default.Password = textPassword.Text;
            Properties.Settings.Default.Save();
            USERNAME = Properties.Settings.Default.Username;
            PASSWORD = Properties.Settings.Default.Password;

            if (!running)
            {
                checkTexts = new Thread(new ThreadStart(Init));
                checkTexts.Start();
                btnSendText.Text = "Stop";
            }
            else
            {
                btnSendText.Text = "Start";
                checkTexts.Abort();
            }
            running = !running;

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings window = new Settings();
            window.ShowDialog();
            ACCOUNT_SID = Properties.Settings.Default.Account_SID;
            AUTH_TOKEN = Properties.Settings.Default.Auth_Token;
            TWILIO_NUMBER = Properties.Settings.Default.Twilio_Number;
            CELL_NUMBER = Properties.Settings.Default.Cell_Number;
        }
    }
}
