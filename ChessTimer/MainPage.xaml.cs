namespace ChessTimer;

public partial class MainPage : ContentPage
{
    private TimeOnly timerBlack;
    private TimeOnly timerWhite;
    private bool IsWhiteRunning = false;
    private bool IsBlackRunning = false;

	public MainPage()
    {
        InitializeComponent();
        startStopBtn.IsVisible = false;
        resetBtn.IsVisible= false;
    }
    //When clicking the start game, white timer starts
    public async void StartGame(object sender, EventArgs e)
    {
        startStopBtn.IsVisible = false;
        IsWhiteRunning = !IsWhiteRunning;

        while (IsWhiteRunning)
        {
            timerWhite = timerWhite.Add(-TimeSpan.FromSeconds(1));
            SetTimeWhite();
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }

    //Resets timers
    public void ResetGame(object sender, EventArgs e)
    {
        time.IsVisible = true;
        IsWhiteRunning = false;
        IsBlackRunning = false;
        TimerBlackBtn.Text = "00:00";
        TimerWhiteBtn.Text = "00:00";
        startStopBtn.IsVisible = true;
    }

    public async void OnStartStop(object sender, EventArgs e)
    {
        if (IsBlackRunning)
        {
            IsBlackRunning = !IsBlackRunning;
            IsWhiteRunning = !IsWhiteRunning;
            while (IsWhiteRunning)
            {
                timerWhite = timerWhite.Add(-TimeSpan.FromSeconds(1));
                SetTimeWhite();
                await Task.Delay(TimeSpan.FromSeconds(1));

                if(timerWhite.Minute == 0 && timerWhite.Second == 0)
                {
                    await DisplayAlert("GAME OVER!", "Black Wins!", "Close.");
                    resetGame();
                    return;
                }
            }
        }
        else if(IsWhiteRunning)
        {
            IsBlackRunning = !IsBlackRunning;
            IsWhiteRunning = !IsWhiteRunning;
            while (IsBlackRunning)
            {
                timerBlack = timerBlack.Add(-TimeSpan.FromSeconds(1));
                SetTimeBlack();
                await Task.Delay(TimeSpan.FromSeconds(1));

                if (timerBlack.Minute == 0 && timerBlack.Second == 0)
                {
                    await DisplayAlert("GAME OVER!", "White Wins!", "Close.");
                    resetGame();
                    return;
                }
            }
        }
    }

    private void SetTimeWhite()
    {
        TimerWhiteBtn.Text = $"{timerWhite.Minute:00}:{timerWhite.Second:00}";
    }
    private void SetTimeBlack()
    {
        TimerBlackBtn.Text = $"{timerBlack.Minute:00}:{timerBlack.Second:00}";
    }
    void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        string myText = time.Text;
    }
    public async void OnEntryCompleted(object sender, EventArgs e)
    {
        if (int.Parse(time.Text)<=0 || int.Parse(time.Text) > 59)
        {
            await DisplayAlert("Alert!", "Insert number between 0 and 59", "Close");
            time.Text = "";
            return;
        }
        timerBlack = new(00, int.Parse(time.Text), 00);
        timerWhite = new(00, int.Parse(time.Text), 00);
        SetTimeBlack();
        SetTimeWhite();
        startStopBtn.IsVisible = true;
        resetBtn.IsVisible = true;
        time.IsVisible = false;
        time.Text = "";
    }

    public void resetGame()
    {
        time.IsVisible = true;
        IsWhiteRunning = false;
        IsBlackRunning = false;
        TimerBlackBtn.Text = "00:00";
        TimerWhiteBtn.Text = "00:00";
        startStopBtn.IsVisible = true;
    }
}

