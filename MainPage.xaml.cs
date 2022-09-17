namespace Drag;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private void Button_OnPressed(object sender, EventArgs e)
    {
        MauiProgram.Drag();
    }
}
