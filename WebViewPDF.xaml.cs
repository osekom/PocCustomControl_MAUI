using static System.Net.Mime.MediaTypeNames;

namespace PocCustomControlMAUI;

public partial class WebViewPDF : ContentPage
{

    string url = "";


	public WebViewPDF()
	{
		InitializeComponent();

		WebView.Source = "";
        WebView.Navigating += WebView_Navigating;
        InitView();
    }

    private void WebView_Navigating(object? sender, WebNavigatingEventArgs e)
    {
        if (string.IsNullOrEmpty(url))
        {
            url = e.Url;
        }
        else
        {
            e.Cancel = true;
        }
    }

    public async void InitView()
    {
        
        try
        {
            Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("pdfs.html");
            StreamReader reader = new StreamReader(fileStream);

            var html = reader.ReadToEnd();

            WebView.Source = new HtmlWebViewSource { Html=html };
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    async void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        string result = await WebView.EvaluateJavaScriptAsync("GetPDF()"); //obtiene el PDF Base64 Binario.
        Console.WriteLine(result);
    }
}
