using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WebSocketSharp;

namespace Chat
{

    public partial class Auth : Page
    {
        WebSocket ws = new WebSocket("ws://127.0.0.1:8888");

        public Auth()
        {
            InitializeComponent();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserData user = new UserData();
            user.Name = login.Text;
            user.Password = password.Text;
            user.Message = "";
            user.UserWant = "auth";

            var json = JsonConvert.SerializeObject(user);

            ws.Connect();

            ws.Send(json);

            ws.OnMessage += (sender1, e1) =>
            {

                UserData m = JsonConvert.DeserializeObject<UserData>(e1.Data);

                if (m.Name == $"{user.Name}" && m.UserWant == "auth")
                {

                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {

                        var window = new Messages(e1.Data.ToString());
                        NavigationService.Navigate(window);

                        ((MainWindow)System.Windows.Application.Current.MainWindow).lb1.Content = m.Name;
                        ((MainWindow)System.Windows.Application.Current.MainWindow).lbMyId.Content = m.WSID;

                    });

                }


            };


        }
    }
}
