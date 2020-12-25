using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataSaver
{
    class BridgeSaver
    {
        private string rabbitQueueName;
        private string rabbitExchangeName;
        private string rabbitPattern;

        private string databaseIP;
        private int databasePort;
        private string databaseName;
        private string databaseUserName;
        private string databasePassword;

        private IConnection connection;
        private IModel channel;
        private string consumerTag;

        private Form1 form;
        private SqlConnection sqlConnection;

        public BridgeSaver(string queueName,IConnection _connection,string _databaseIP,int _databasePort,string _databaseName,string _databaseUserName,string _databasePassword, Form1 fm)
        {
            //rabbitExchangeName = exchangeName;
            rabbitQueueName = queueName;
            //rabbitPattern = keyPattern;
            connection = _connection;
            databaseIP = _databaseIP;
            databasePort = _databasePort;
            databaseName = _databaseName;
            databaseUserName = _databaseUserName;
            databasePassword = _databasePassword;
            form = fm;
            string connectionString = "Data Source = " + databaseIP + "," + databasePort.ToString() + ";Network Library = DBMSSOCN;Initial Catalog = " + databaseName + ";User ID = " + databaseUserName + ";Password = " + databasePassword;
            sqlConnection = new SqlConnection(connectionString);
        }

        public void Start()
        {
            if(sqlConnection.State  == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            if (connection.IsOpen)
            {
                channel = connection.CreateModel();

                if (channel.IsOpen)
                {
                    channel.ConfirmSelect();
                    channel.BasicQos(0, 1, false);

                    //string[] keys = rabbitPattern.Split(',');

                    //foreach (var bindingKey in keys)
                    //{
                    //    channel.QueueBind(queue: rabbitQueueName, exchange: rabbitExchangeName, routingKey: bindingKey);
                    //}

                    channel.ModelShutdown += Channel_ModelShutdown;
                    
                    var consumer = new EventingBasicConsumer(channel);

                    
                    consumer.Received += (model, ea) =>
                    {
                        try
                        {
                            //consumer.
                            var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                            var routingKey = ea.RoutingKey;
                            //Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
                            form.AppendLog(DateTime.Now.ToString()+" "+ routingKey + " " + message);
                            //Thread.Sleep(1000);
                            // Note: it is possible to access the channel via
                            //       ((EventingBasicConsumer)sender).Model here
                            EventingBasicConsumer _consumer = (EventingBasicConsumer)model;
                            if (_consumer.Model.IsOpen)
                            {
                                //form.AppendLog(routingKey + " " + message);

                                bool result = HandleMessage(routingKey, message);
                                if (result)
                                {
                                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                                }
                            }
                            else
                            {
                                form.AppendLog("_consumer Model Is not Open");
                            }
                        }
                        catch (Exception ex)
                        {
                            form.AppendLog(ex.StackTrace.ToString());
                        }
                        
                        
                    };
                    
                    consumerTag = channel.BasicConsume(queue: rabbitQueueName, autoAck: false, consumer: consumer);
                    
                    form.AppendLog(consumerTag);
                }
                else
                {

                }
            }
            else
            {
                //AppendLog("rabbit connection is closed,auto reconnect.....");
            }
            
        }

        private void Channel_ModelShutdown(object sender, ShutdownEventArgs e)
        {
            form.AppendLog(e.ClassId.ToString()+" Channel shutdown");
        }
            

        private bool HandleMessage(string routingKey,string msg)
        {
            string tableName = "t_" + routingKey.Replace('.', '_');
            string deviceType = routingKey.Split('.')[1];
            bool result = false;
            switch (deviceType)
            {
                case "02":
                    Temperature_Data td = JsonConvert.DeserializeObject<Temperature_Data>(msg);
                    result = InsertTemperatureData(tableName, td);
                    break;
                case "08":
                    Strain_Data sd = JsonConvert.DeserializeObject<Strain_Data>(msg);
                    result = InsertStrainData(tableName, sd);
                    break;
                case "09":
                    Settlement_Data obj = JsonConvert.DeserializeObject<Settlement_Data>(msg);
                    result = InsertSettlementData(tableName, obj);
                    break;
                case "11":
                    Inclination_Data id = JsonConvert.DeserializeObject<Inclination_Data>(msg);
                    result = InsertInclinationData(tableName, id);
                    break;
                case "13":
                    TemperatureHumidity_Data thd = JsonConvert.DeserializeObject<TemperatureHumidity_Data>(msg);
                    result = InsertTemperatureHumidityData(tableName, thd);
                    break;
                case "19":
                    LaserRange_Data lrd = JsonConvert.DeserializeObject<LaserRange_Data>(msg);
                    result = InsertLaserRangeData(tableName, lrd);
                    break;
                default:break;
            }
            return result;
        }

        private bool InsertLaserRangeData(string tableName, LaserRange_Data data)
        {
            string sqlStatement = "INSERT INTO " + tableName + "(Stamp,Distance,DeltaDistance) VALUES(@stamp,@distance,@deltaDistance)";

            bool result = true;
            try
            {
                if(sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
                using (SqlCommand cmd = new SqlCommand(sqlStatement, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@stamp", data.TimeStamp);
                    cmd.Parameters.AddWithValue("@distance", data.Distance);
                    cmd.Parameters.AddWithValue("@deltaDistance", data.DeltaDistance);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result = false;
                form.AppendLog(ex.StackTrace.ToString() + "\r\n");
            }
            return result;
        }

        private bool InsertInclinationData(string tableName, Inclination_Data data)
        {
            string sqlStatement = "INSERT INTO " + tableName + "(Stamp,X,Y,DeltaX,DeltaY) VALUES(@stamp,@x,@y,@deltaX,@deltaY)";
            

            bool result = true;
            try
            {
                string stamp = data.TimeStamp;
                string x = data.X.ToString();
                string y = data.Y.ToString();
                string deltaX = data.DeltaX.ToString();
                string deltaY = data.DeltaY.ToString();
                //string sqlStatement = $"INSERT INTO {tableName} VALUES('{stamp}',{x},{y},{deltaX},{deltaY})";

                using (SqlCommand cmd = new SqlCommand(sqlStatement, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@stamp", data.TimeStamp);
                    cmd.Parameters.AddWithValue("@x", data.X);
                    cmd.Parameters.AddWithValue("@y", data.Y);
                    cmd.Parameters.AddWithValue("@deltaX", data.DeltaX);
                    cmd.Parameters.AddWithValue("@deltaY", data.DeltaY);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result = false;
                form.AppendLog(ex.StackTrace.ToString() + "\r\n");
            }
            return result;
        }

        private bool InsertSettlementData(string tableName, Settlement_Data data)
        {
            string sqlStatement = "INSERT INTO " + tableName + "(Stamp,Innage,DeltaInnage,Deflection) VALUES(@stamp,@innage,@deltaInnage,@deflection)";

            bool result = true;
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@stamp", data.TimeStamp);
                    cmd.Parameters.AddWithValue("@innage", data.Innage);
                    cmd.Parameters.AddWithValue("@deltaInnage", data.DeltaInnage);
                    cmd.Parameters.AddWithValue("@deflection", data.Deflection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result = false;
                form.AppendLog(ex.StackTrace.ToString() + "\r\n");
            }
            return result;
        }

        public void Stop()
        {
            channel.BasicCancel(consumerTag);
            channel.Close();
            sqlConnection.Close();
        }

        private bool InsertTemperatureData(string tableName,Temperature_Data data)
        {   
            string sqlStatement = "INSERT INTO "+tableName+"(Stamp,Temperature) VALUES(@stamp,@temperature)";

            bool result = true;
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@stamp", data.TimeStamp);
                    cmd.Parameters.AddWithValue("@temperature", data.Temperature);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result = false;
                form.AppendLog(ex.StackTrace.ToString() + "\r\n");
            }
            return result;
        }

        private bool InsertTemperatureHumidityData(string tableName, TemperatureHumidity_Data data)
        {
            string sqlStatement = "INSERT INTO " + tableName + "(Stamp,Temperature,Humidity) VALUES(@stamp,@temperature,@humidity)";

            bool result = true;
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@stamp", data.TimeStamp);
                    cmd.Parameters.AddWithValue("@temperature", data.Temperature);
                    cmd.Parameters.AddWithValue("@humidity", data.Humidity);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result = false;
                form.AppendLog(ex.StackTrace.ToString() + "\r\n");
            }
            return result;
        }

        private bool InsertStrainData(string tableName, Strain_Data data)
        {
            string sqlStatement = "INSERT INTO " + tableName + "(Stamp,Temperature,Strain,Frequency) VALUES(@stamp,@temperature,@strain,@frequency)";

            bool result = true;
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlStatement, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@stamp", data.TimeStamp);
                    cmd.Parameters.AddWithValue("@temperature", data.Temperature);
                    cmd.Parameters.AddWithValue("@strain", data.Strain);
                    cmd.Parameters.AddWithValue("@frequency", data.Frequency);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                result = false;
                form.AppendLog(ex.StackTrace.ToString() + "\r\n");
            }
            return result;
        }
    }
}
