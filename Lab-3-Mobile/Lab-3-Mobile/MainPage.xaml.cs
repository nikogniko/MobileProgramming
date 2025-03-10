namespace Lab_3_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var screenWidth = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;

            clearButton.WidthRequest = screenWidth / 2 - 25;
            exitButton.WidthRequest = screenWidth / 2 - 25;
        }

        private void OnOperatorClicked(object sender, EventArgs e)
        {
            if (!double.TryParse(Number1.Text, out double num1) || !double.TryParse(Number2.Text, out double num2))
            {
                ResultOutput.Text = "Помилка";
                return;
            }

            Button? button = sender as Button;
            if (button == null) return;

            double result = 0;
            switch (button.Text)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = Math.Round(num1 * num2, 10);
                    break;
                case "/":
                    if (num2 == 0)
                    {
                        ResultOutput.Text = "Ділення на 0!";
                        return;
                    }

                    result = num1 / num2;
                    break;

                case "%":
                    if (num2 == 0)
                    {
                        ResultOutput.Text = "Ділення на 0!";
                        return;
                    }

                    result = num1 % num2;
                    break;

                case "^":
                    if (num1 == 0 && num2 < 0)
                    {
                        ResultOutput.Text = "0 у від’ємному степені!";
                        return;
                    }
                    else
                    {
                        result = Math.Pow(num1, num2);

                        if (double.IsInfinity(result))
                        {
                            ResultOutput.Text = "Результат завеликий!";
                            return;
                        }
                    }
                    break;
            }

            if (result == -0)
                result = 0;
            else
                result = Math.Round(result, 10);

            ResultOutput.Text = result.ToString();
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            Number1.Text = string.Empty;
            Number2.Text = string.Empty;
            ResultOutput.Text = string.Empty;
        }

        private static void OnExitClicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}