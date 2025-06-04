

namespace CattelSalasarMAUI.CustomComponents;

public partial class HeaderContent : ContentView
{
	public HeaderContent()
	{
		InitializeComponent();
        Pageload();
    }
    public void Pageload()
	{
        UserName.Text = Preferences.Get("userName", "");
        Email.Text = Preferences.Get("emaiId", "");
        Mobile.Text = Preferences.Get("mobileNo", "");
    }
}