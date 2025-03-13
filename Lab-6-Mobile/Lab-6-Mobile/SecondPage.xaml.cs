using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Lab_6_Mobile;

public partial class SecondPage : ContentPage
{
	public SecondPage()
	{
		InitializeComponent();

        var screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

        calcBtn.WidthRequest = screenWidth / 2 - 30;
        exitBtn.WidthRequest = screenWidth / 2 - 30;
    }

    private async void OnCalculateClicked(object sender, EventArgs e)
    {
        // �������� ��������� ����������
        ResultGrid.Clear();

        // ������ ���������
        ResultGrid.Add(new Label { Text = "X", FontAttributes = FontAttributes.Bold, BackgroundColor = Colors.Indigo, TextColor = Colors.White, Padding = new Thickness(75, 10) }, 0, 0);
        ResultGrid.Add(new Label { Text = "Y", FontAttributes = FontAttributes.Bold, BackgroundColor = Colors.Indigo, TextColor = Colors.White, Padding = new Thickness(75, 10) }, 1, 0);

        // ��������� �������� �������
        if (!double.TryParse(EntryXStart.Text, out double xStart) ||
            !double.TryParse(EntryXEnd.Text, out double xEnd) ||
            !double.TryParse(EntryStep.Text, out double step) ||
            step <= 0 || xEnd <= xStart)
        {
            await Toast.Make("��������� ����� ���!", ToastDuration.Short).Show();
            return;
        }

        int rowIndex = 1;
        for (double x = xStart; x <= xEnd; x += step)
        {
            double y = Math.Sin(x); // ���������� �������

            // ��������� � �������
            ResultGrid.Add(new Label { Text = x.ToString("F2"), HorizontalTextAlignment = TextAlignment.Center }, 0, rowIndex);
            ResultGrid.Add(new Label { Text = y.ToString("F3"), HorizontalTextAlignment = TextAlignment.Center }, 1, rowIndex);
            rowIndex++;
        }

        if (rowIndex > 1) // ���� � ���, �������� �������
            ResultGrid.IsVisible = true;
    }

    private void OnExitClicked(object sender, EventArgs e)
    {
        Environment.Exit(0); // ������� �������
    }
}