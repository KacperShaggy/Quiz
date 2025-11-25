using Newtonsoft.Json;
using Quiz;
namespace Quiz;

public partial class quiz_page : ContentPage, IQueryAttributable
{
    private Game currentGame;
    private Question currentQuestion;
    private List<Question> questions;

    public quiz_page()
    {
        InitializeComponent();
    }

    public class Question
    {
        public string? Text { get; set; }
        public string[]? Answers { get; set; } 
        public int CorrectIndex { get; set; } 
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("gameData", out var data))
        {
            string encoded = data.ToString();
            string json = Uri.UnescapeDataString(encoded);
            currentGame = JsonConvert.DeserializeObject<Game>(json);

            LoadAllQuestions();      
            PrepareGameQuestions(); 
            ShowQuestion();           
        }
    }


    private List<Question> allQuestions;
    private List<Question> gameQuestions; 

    private void LoadAllQuestions()
    {
        allQuestions = new List<Question>
    {
        new Question { Text = "Kto jest obecnym Prezydentem Polski?", Answers = new[] { "Andrzej Duda", "Donald Tusk", "Mateusz Morawiecki", "Jaros³aw Kaczyñski" }, CorrectIndex = 0 },
        new Question { Text = "Jak nazywa siê polski parlament?", Answers = new[] { "Sejm i Senat", "Kongres i Senat", "Rada Narodowa", "Zgromadzenie Narodowe" }, CorrectIndex = 0 },
        new Question { Text = "Kto pe³ni funkcjê premiera Polski?", Answers = new[] { "Mateusz Morawiecki", "Andrzej Duda", "Donald Tusk", "Beata Szyd³o" }, CorrectIndex = 0 },
        new Question { Text = "W którym roku Polska wst¹pi³a do Unii Europejskiej?", Answers = new[] { "2004", "1999", "2007", "2010" }, CorrectIndex = 0 },
        new Question { Text = "Jakie s¹ g³ówne partie polityczne w Polsce?", Answers = new[] { "PiS, PO, Lewica, PSL", "PiS, PO, SLD, KOR", "PSL, PO, PiS, KOR", "PO, PiS, Ruch Palikota, SLD" }, CorrectIndex = 0 },
        new Question { Text = "Kto by³ pierwszym prezydentem III RP?", Answers = new[] { "Lech Wa³êsa", "Aleksander Kwaœniewski", "Bronis³aw Komorowski", "Ryszard Kaczorowski" }, CorrectIndex = 0 },
        new Question { Text = "Jak nazywa siê polski Trybuna³ Konstytucyjny?", Answers = new[] { "Trybuna³ Konstytucyjny", "S¹d Najwy¿szy", "Rada Ministrów", "Senat" }, CorrectIndex = 0 },
        new Question { Text = "Kto jest liderem PiS?", Answers = new[] { "Mateusz Morawiecki", "Jaros³aw Kaczyñski", "Donald Tusk", "Andrzej Duda" }, CorrectIndex = 1 },
        new Question { Text = "Kiedy odby³y siê pierwsze wolne wybory w Polsce po 1989 roku?", Answers = new[] { "2004", "1991", "1993", "1989" }, CorrectIndex = 3 },
        new Question { Text = "Jak nazywa siê polski urz¹d premiera?", Answers = new[] { "Kancelaria Prezesa Rady Ministrów", "Pa³ac Prezydencki", "Sejm", "Senat" }, CorrectIndex = 0 },
        new Question { Text = "Kto by³ pierwszym przewodnicz¹cym Unii Europejskiej z Polski?", Answers = new[] { "Donald Tusk", "Jerzy Buzek", "Rados³aw Sikorski", "Lech Wa³êsa" }, CorrectIndex = 1 },
        new Question { Text = "Jak nazywa siê polska konstytucja uchwalona w 1997 roku?", Answers = new[] { "Konstytucja Rzeczypospolitej Polskiej", "Ustawa Zasadnicza", "Konstytucja III RP", "Ustawa Konstytucyjna" }, CorrectIndex = 0 },
        new Question { Text = "Kto by³ liderem Solidarnoœci w latach 80.?", Answers = new[] { "Lech Wa³êsa", "Tadeusz Mazowiecki", "Wojciech Jaruzelski", "Bronis³aw Geremek" }, CorrectIndex = 0 },
        new Question { Text = "Kto w Polsce uchwala bud¿et pañstwa?", Answers = new[] { "Sejm", "Senat", "Prezydent", "Rada Ministrów" }, CorrectIndex = 0 },
        new Question { Text = "Jaki jest obowi¹zuj¹cy system polityczny w Polsce?", Answers = new[] { "Republika parlamentarna", "Monarchia", "Republika prezydencka", "Demokracja bezpoœrednia" }, CorrectIndex = 0 }
    };
    }

    private void PrepareGameQuestions()
    {
        var rnd = new Random();
        gameQuestions = allQuestions.OrderBy(x => rnd.Next()).Take(10).ToList(); // losowe 10 pytañ
    }




    private void ShowQuestion()
    {
        int round = currentGame.RoundNumber;

        if (round >= gameQuestions.Count)
        {
            string json = JsonConvert.SerializeObject(currentGame);
            Shell.Current.GoToAsync($"//leaderboard?gameData={Uri.EscapeDataString(json)}");
            return;
        }

        currentQuestion = gameQuestions[round];
        ShuffleAnswers(currentQuestion);

        question.Text = currentQuestion.Text;
        ansA.Text = currentQuestion.Answers[0];
        ansB.Text = currentQuestion.Answers[1];
        ansC.Text = currentQuestion.Answers[2];
        ansD.Text = currentQuestion.Answers[3];

        UpdateCurrentPlayer();
    }




    private void UpdateCurrentPlayer()
    {
        string currentPlayer = currentGame.RoundNumber % 2 == 1 ? currentGame.UserName1 : currentGame.UserName2;
        currentPlayerLabel.Text = $"Teraz odpowiada: {currentPlayer}";
    }

    private void ShuffleAnswers(Question q)
    {
        var rnd = new Random();
        string correctAnswer = q.Answers[q.CorrectIndex];
        q.Answers = q.Answers.OrderBy(a => rnd.Next()).ToArray();
        q.CorrectIndex = Array.IndexOf(q.Answers, correctAnswer);
    }




    private void AnswerSelected(int selectedIndex)
    {
        string currentPlayer = currentGame.RoundNumber % 2 == 1
            ? currentGame.UserName1
            : currentGame.UserName2;
        ;

        if (selectedIndex == currentQuestion.CorrectIndex)
        {
            if (currentGame.RoundNumber % 2 == 1)
                currentGame.PointsUser1 += 1;
            else
                currentGame.PointsUser2 += 1;

            DisplayAlert("Poprawnie!", $"{currentPlayer} zdobywa punkt!", "OK");
        }
        else
        {
            string correctAnswer = currentQuestion.Answers[currentQuestion.CorrectIndex];
            DisplayAlert("le!", $"{currentPlayer} odpowiedzia³ Ÿle. Poprawna odpowiedŸ to: {correctAnswer}", "OK");
        }

        currentGame.RoundNumber += 1;
        ShowQuestion();
    }


    private void AnswerA(object sender, EventArgs e) => AnswerSelected(0);
    private void AnswerB(object sender, EventArgs e) => AnswerSelected(1);
    private void AnswerC(object sender, EventArgs e) => AnswerSelected(2);
    private void AnswerD(object sender, EventArgs e) => AnswerSelected(3);
}