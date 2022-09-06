namespace MauiApp1;

public partial class MainPage : ContentPage
{
    List<int> numberList = new();

    int seconds = 0;
    int minutes = 0;

    int setSeconds = 0;
    int setMinutes = 0;

    int repCount = 0;

    bool isRunning;
    bool isClicked = false;

    public MainPage()
    {
        InitializeComponent();
        PutNumbersInPicker();
    }

    public void PutNumbersInPicker()
    {
        for (int i = 0; i < 61; i++)
        {
            numberList.Add(i);
        }

        MinPicker.ItemsSource = numberList;
        SecPicker.ItemsSource = numberList;
        RepPicker.ItemsSource = numberList;
    }

    private async void ActivateTimerButton(object sender, EventArgs e)
    {
        if (!isClicked)
        {
            if (!String.IsNullOrEmpty(MinPicker.SelectedItem.ToString()) && !String.IsNullOrEmpty(SecPicker.SelectedItem.ToString()) &&
                !String.IsNullOrEmpty(RepPicker.SelectedItem.ToString()))
            {
                isClicked = true;
                isRunning = true;

                minutes = int.Parse(MinPicker.SelectedItem.ToString());
                seconds = int.Parse(SecPicker.SelectedItem.ToString());
                repCount = int.Parse(RepPicker.SelectedItem.ToString());

                setSeconds = seconds;
                setMinutes = minutes;

                while (isRunning)
                {
                    SetTime();
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
        }
    }

    public void SetTime()
    {

        if (repCount == 0 && minutes == 0 && seconds == 0)
        {
            isRunning = false;
            isClicked = false;
        }
        else if (seconds > 0)
        {
            seconds--;
        }
        else if (minutes > 0 && seconds == 0)
        {
            minutes--;
            seconds = 59;
        }
        else if (minutes == 0 && seconds == 0)
        {
            repCount--;
            seconds = setSeconds;
            minutes = setMinutes;
        }

        if (seconds < 10)
        {
            TimeResultLabel.Text = "00 : " + $"0{minutes} : 0{seconds}";
        }
        else if (minutes < 10)
        {
            TimeResultLabel.Text = "00 : " + $"0{minutes} : {seconds}";
        }
        else if (seconds < 10 && minutes < 10)
        {
            TimeResultLabel.Text = "00 : " + $"0{minutes} : 0{seconds}";
        }
        else
        {
            TimeResultLabel.Text = "00 : " + $"{minutes} : {seconds}";
        }

        RepCountLabel.Text = "Reps: " + repCount.ToString();

    }

    private void StopTimerButton_Clicked(object sender, EventArgs e)
    {
        isRunning = false;
        isClicked = false;

        TimeResultLabel.Text = "00 : " + "00 : 00";

        RepCountLabel.Text = "Reps: ";

        minutes = 0;
        seconds = 0;
    }
}
