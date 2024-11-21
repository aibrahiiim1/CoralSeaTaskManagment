namespace CoralSeaTaskManagment.Mobile
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
        private async void OnGetDataButtonClicked(object sender, EventArgs e)
        {
            var apiService = new ApiService();
            var data = await apiService.GetModelsAsync();
            // Update UI with the retrieved data
        }

    }

}
