using Android.Views;
using Android.Content;

namespace Lab_4_Android
{
    [Activity(Label = "RelativeLayout Example", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RelativeLayout relativeLayout;
        TextView textView3;
        Button button;
        Button switchButton;
  
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);  // RelativeLayout

            // Знаходимо компоненти за id
            relativeLayout = FindViewById<RelativeLayout>(Resource.Id.relativeLayout);
            textView3 = FindViewById<TextView>(Resource.Id.textView3);
            button = FindViewById<Button>(Resource.Id.button);

            // Призначаємо обробник кліку на кнопку, який викликає метод переміщення
            button.Click += (sender, e) => MoveElement();


            // У activity_main.xml є кнопка з id "switchButton"
            switchButton = FindViewById<Button>(Resource.Id.switchButton);
            switchButton.Click += (sender, e) =>
            {
                // Запускаємо ConstraintSetActivity
                StartActivity(new Intent(this, typeof(ConstraintSetActivity)));
            };
        }

        // Метод, який відповідає за переміщення textView3
        private void MoveElement()
        {
            // Видаляємо textView3 з RelativeLayout
            relativeLayout.RemoveView(textView3);

            // Створюємо нові параметри розташування для textView3
            RelativeLayout.LayoutParams newParams = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);

            // Розташовуємо textView3 під кнопкою
            newParams.AddRule(LayoutRules.Below, Resource.Id.button);
            // Центруємо по горизонталі
            newParams.AddRule(LayoutRules.CenterHorizontal);
            // Встановлюємо відступ згори
            newParams.SetMargins(0, 20, 0, 0);

            // Застосовуємо нові параметри до textView3
            textView3.LayoutParameters = newParams;

            // Додаємо textView3 назад до RelativeLayout
            relativeLayout.AddView(textView3);
        }
    }
}