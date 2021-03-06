using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ChatClient.ServiceChat;


namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback //IServiceChatCallback is from ServiceChat from wcf_chat.csproj
    {
        bool isConnected = false;
        ServiceChatClient client;
        int ID;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        void Connect_User() 
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this)); //MainWindow realise IServiceChatCallback, so we can use it by the keyword "this".
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                tbMessage.IsEnabled = true;
                btnConnDiscon.Content = "Disconnect";
                isConnected = true;

            }
        }

        void Disconnect_User() 
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;

                tbUserName.IsEnabled = true;
                tbMessage.IsEnabled = false;

                btnConnDiscon.Content = "Connect";

                isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                Disconnect_User();
            }
            else
            {
                Connect_User();
            }
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);  // Scrolls down, when messages goes to the bottom
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Disconnect_User();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Enter)
            {
                if (client!=null)
                {
                    client.SendMsg(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }
            }
        }

        private void btnSndMsg_Click(object sender, RoutedEventArgs e)
        {
            if (client != null)
            {
                client.SendMsg(tbMessage.Text, ID);
                tbMessage.Text = string.Empty;
            }
        }

    }
}
