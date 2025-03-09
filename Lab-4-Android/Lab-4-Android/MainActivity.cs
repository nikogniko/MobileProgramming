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

            // ��������� ���������� �� id
            relativeLayout = FindViewById<RelativeLayout>(Resource.Id.relativeLayout);
            textView3 = FindViewById<TextView>(Resource.Id.textView3);
            button = FindViewById<Button>(Resource.Id.button);

            // ���������� �������� ���� �� ������, ���� ������� ����� ����������
            button.Click += (sender, e) => MoveElement();


            // � activity_main.xml � ������ � id "switchButton"
            switchButton = FindViewById<Button>(Resource.Id.switchButton);
            switchButton.Click += (sender, e) =>
            {
                // ��������� ConstraintSetActivity
                StartActivity(new Intent(this, typeof(ConstraintSetActivity)));
            };
        }

        // �����, ���� ������� �� ���������� textView3
        private void MoveElement()
        {
            // ��������� textView3 � RelativeLayout
            relativeLayout.RemoveView(textView3);

            // ��������� ��� ��������� ������������ ��� textView3
            RelativeLayout.LayoutParams newParams = new RelativeLayout.LayoutParams(
                ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);

            // ����������� textView3 �� �������
            newParams.AddRule(LayoutRules.Below, Resource.Id.button);
            // �������� �� ����������
            newParams.AddRule(LayoutRules.CenterHorizontal);
            // ������������ ������ �����
            newParams.SetMargins(0, 20, 0, 0);

            // ����������� ��� ��������� �� textView3
            textView3.LayoutParameters = newParams;

            // ������ textView3 ����� �� RelativeLayout
            relativeLayout.AddView(textView3);
        }
    }
}