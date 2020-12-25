using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSaver
{
    public partial class Form1 : Form
    {
        //public string rabbitIP { get; set; }
        //public int rabbitPort { get; set; }
        //public string rabbitQueueName { get; set; }
        //public string rabbitExchangeName { get; set; }
        //public string rabbitPattern { get; set; }
        //public string rabbitUserName { get; set; }
        //public string rabbitPassword { get; set; }

        private string databaseIP;
        private int databasePort;
        private string databaseName;
        private string databaseUserName;
        private string databasePassword;
        private string databaseTable;

        private ConnectionFactory factory;
        private List<BridgeSaver> dataSaverCollection;
        private Dictionary<string, IConnection> rabbitConnectionCollection;
        public Form1()
        {
            InitializeComponent();
            dataSaverCollection = new List<BridgeSaver>();
            rabbitConnectionCollection = new Dictionary<string, IConnection>();

            databaseIP = ConfigurationManager.AppSettings["DatabaseIP"];
            databaseName = ConfigurationManager.AppSettings["DataBase"];
            databaseUserName = ConfigurationManager.AppSettings["UserName"];
            databasePassword = ConfigurationManager.AppSettings["Password"];
            databaseTable = ConfigurationManager.AppSettings["Table"];

            LoadConfigs();
        }

        private void LoadConfigs()
        {
            string connectionString = "Data Source = " + databaseIP + ";Network Library = DBMSSOCN;Initial Catalog = " + databaseName + ";User ID = " + databaseUserName + ";Password = " + databasePassword;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    LoadBridgeSaverConfig(connection);
                }
                catch (Exception ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.Message);
                    return;
                }
                connection.Close();
            }
        }

        public void AppendLog(string content)
        {
            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.BeginInvoke(new MethodInvoker(() =>
                {
                    textBoxLog.AppendText(content + "\r\n");
                }));
            }
            else
            {
                textBoxLog.AppendText(content + "\r\n");
            }
        }

        private void LoadBridgeSaverConfig(SqlConnection sqlConnection)
        {
            string strainStatement = "select RabbitIP,RabbitPort,RabbitQueueName,RabbitUserName,RabbitPassword,DatabaseIP,DatabasePort,DatabaseName,DatabaseUserName,DatabasePassword from "+ databaseTable;
            SqlCommand strainCommand = new SqlCommand(strainStatement, sqlConnection);
            using (SqlDataReader reader = strainCommand.ExecuteReader())
            {
                Dictionary<string, IConnection> rabbitConnectionCollection = new Dictionary<string, IConnection>();

                while (reader.Read())
                {
                    string rabbitIP = reader.GetString(0);
                    int rabbitPort = reader.GetInt32(1);
                    string rabbitQueueName = reader.GetString(2);
                    string rabbitUserName = reader.GetString(3) ;
                    string rabbitPassword = reader.GetString(4);

                    string databaseIP = reader.GetString(5);
                    int databasePort = reader.GetInt32(6);
                    string databaseName = reader.GetString(7);
                    string databaseUserName = reader.GetString(8);
                    string databasePassword = reader.GetString(9);

                    IConnection connection = null;
                    if (!rabbitConnectionCollection.ContainsKey(rabbitIP))
                    {
                        ConnectionFactory factory = new ConnectionFactory();
                        factory.Password = rabbitPassword;
                        factory.UserName = rabbitUserName;
                        factory.Port = rabbitPort;
                        factory.HostName = rabbitIP;
                        factory.VirtualHost = "/";
                        connection = factory.CreateConnection();
                        
                        rabbitConnectionCollection.Add(rabbitIP, connection);
                    }
                    else
                    {
                        connection = rabbitConnectionCollection[rabbitIP];
                    }

                    BridgeSaver ds = new BridgeSaver(rabbitQueueName,connection, databaseIP, databasePort, databaseName, databaseUserName, databasePassword,this);
                    dataSaverCollection.Add(ds);

                    string[] viewItem = { rabbitIP, rabbitPort.ToString(), rabbitQueueName,databaseIP,databasePort.ToString(),databaseName };
                    ListViewItem listItem = new ListViewItem(viewItem);
                    this.listViewBridgeItem.Items.Add(listItem);
                }
            }
        }

        private bool InitialConnect(ConnectionFactory factory, ref IConnection connection)
        {
            bool result = false;
            try
            {
                connection = factory.CreateConnection();
                result = true;
            }
            catch (Exception e)
            {//RabbitMQ.Client.Exceptions.BrokerUnreachableException e
                textBoxLog.AppendText(e.Message + "\r\n");
                result = false;
            }
            return result;
        }

        private void ToolStripMenuItemStart_Click(object sender, EventArgs e)
        {
            foreach( var item in dataSaverCollection)
            {
                item.Start();
            }
        }

        private void ToolStripMenuItemStop_Click(object sender, EventArgs e)
        {
            foreach (var item in dataSaverCollection)
            {
                item.Stop();
            }
        }
    }
}
