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
            await Shell.Current.GoToAsync("//quiz_page");
        }
    }
}
