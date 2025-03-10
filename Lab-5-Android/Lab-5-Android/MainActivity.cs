using Android.Content;

namespace Lab_5_Android
{
    [Activity(Label = "TableLayout", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button switchButton;
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);  //TableLayout

            // У activity_main.xml є кнопка з id "switchButton"
            switchButton = FindViewById<Button>(Resource.Id.switchButton);
            switchButton.Click += (sender, e) =>
            {
                // Запускаємо ConstraintSetActivity
                StartActivity(new Intent(this, typeof(GridLayoutActivity)));
            };
        }
    }
}