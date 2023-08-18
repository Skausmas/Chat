using System.Windows;
using System.Windows.Controls;
using WebSocketSharp;


namespace Chat
{

    public partial class MainWindow : Window

    {
        static public string idshka = "";

        WebSocket ws = new WebSocket("ws://127.0.0.1:8888");

        Auth a1 = new Auth();

        public string name { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            Frame1.Content = a1;

            ws.Connect();


        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {


        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void listId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            toId.Content = listId.SelectedItem.ToString();
        }
    }
}
