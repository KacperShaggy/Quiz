using Newtonsoft.Json;
using Quiz;
namespace Quiz
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        async private void Start_quiz(object sender, EventArgs e)
        {
            string player1 = user1_name.Text;
            string player2 = user2_name.Text;
            Game NewGame =new Game(player1, player2, 0, 0, 1);
            string jsonData = JsonConvert.SerializeObject(NewGame);
            await Shell.Current.GoToAsync($"//quiz_page?gameData={Uri.EscapeDataString(jsonData)}");
        }
    }
}
