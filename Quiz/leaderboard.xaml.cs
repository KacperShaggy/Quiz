using Newtonsoft.Json;
using Quiz;
namespace Quiz;

public partial class leaderboard : ContentPage, IQueryAttributable
{
    private Game currentGame;

    public leaderboard()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("gameData", out var data))
        {
            string encoded = data.ToString();
            string json = Uri.UnescapeDataString(encoded);

            currentGame = JsonConvert.DeserializeObject<Game>(json);

        }
    }
    private void Grid_Loaded(object sender, EventArgs e)
    {
        nickname1.Text = currentGame.UserName1;
        nickname2.Text = currentGame.UserName2;
        point1.Text = (currentGame.PointsUser1).ToString();
        point2.Text = (currentGame.PointsUser2).ToString();
        results.Text = "Wyniki | Runda "+(currentGame.RoundNumber).ToString()+"/10";
        if (currentGame.RoundNumber==9)
        {
            results.Text = "Wyniki koñcowe";
        }
    }

    private async void Next_Round(object sender, EventArgs e)
    {
        currentGame.RoundNumber += 1;
        string json = JsonConvert.SerializeObject(currentGame);
        if (currentGame.RoundNumber < 10)
        {
            await Shell.Current.GoToAsync($"//quiz_page?gameData={Uri.EscapeDataString(json)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//MainPage?gameData={Uri.EscapeDataString(json)}");
        }
    }
}
