using Quiz;
namespace Quiz;

public partial class quiz_page : ContentPage
{
	public quiz_page()
	{
		InitializeComponent();
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
        await Shell.Current.GoToAsync("//leaderboard");
    }
}