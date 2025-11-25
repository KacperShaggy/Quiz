using Newtonsoft.Json;
using Quiz;
namespace Quiz;

public partial class quiz_page : ContentPage, IQueryAttributable
{
    private Game currentGame;

    public quiz_page()
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

    private void AnswerA(object sender, EventArgs e)
    {
        checkAnswer(1);
    }
    private void AnswerB(object sender, EventArgs e)
    {
        checkAnswer(2);
    }
    private void AnswerC(object sender, EventArgs e)
    {
        checkAnswer(3);
    }
    private void AnswerD(object sender, EventArgs e)
    {
        checkAnswer(4);
    }
    async private void checkAnswer(int x)
    {
        int correctAnswer = 3;
        if (correctAnswer==x && currentGame.RoundNumber%2==1)
        {
            currentGame.PointsUser1 += 1;
        }
        if (correctAnswer==x && currentGame.RoundNumber%2==0)
        {
            currentGame.PointsUser2 += 1;
        }
        string json = JsonConvert.SerializeObject(currentGame);
        await Shell.Current.GoToAsync($"//leaderboard?gameData={Uri.EscapeDataString(json)}");
    }
}