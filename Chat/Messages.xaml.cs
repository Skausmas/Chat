using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using WebSocketSharp;

namespace Chat
{

    public partial class Messages : Page
    {
        WebSocket ws = new WebSocket("ws://127.0.0.1:8888");

        public Messages(string ud)
        {
            InitializeComponent();

            ws.Connect();

            ws.OnMessage += (sender1, e1) =>
            {

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    UserData m = JsonConvert.DeserializeObject<UserData>(e1.Data);
                    if (m.UserWant == "mesAll")
                    {
                        TextBlockChat.Text = m.Message + "\n";
                    }

                    if (m.Name != ((MainWindow)System.Windows.Application.Current.MainWindow).lb1.Content.ToString() && m.UserWant == "auth")
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).listId.Items.Add(m.WSID);

                    }
                });



            };



        }

        private void Send_Click(object sender, RoutedEventArgs e) // Отправить всем
        {
            UserData user = new UserData();
            user.Name = ((MainWindow)System.Windows.Application.Current.MainWindow).lb1.Content.ToString();
            user.Password = "";
            user.Message = msgBox.Text;
            user.UserWant = "mesAll";
            user.WSID = "";

            var jsonchik = JsonConvert.SerializeObject(user);

            ws.Send(jsonchik);
        }

        private void Button_Click(object sender, RoutedEventArgs e) //  Отправить в личку
        {
            UserData user = new UserData();
            user.Name = ((MainWindow)System.Windows.Application.Current.MainWindow).lb1.Content.ToString();
            user.Password = "";
            user.Message = msgBox.Text;
            user.UserWant = "whisper";
            user.WSID = ((MainWindow)System.Windows.Application.Current.MainWindow).toId.Content.ToString();

            var jsonchik = JsonConvert.SerializeObject(user);

            ws.Send(jsonchik);
        }
    }
}
