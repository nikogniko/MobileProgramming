using Android.Views;
using AndroidX.ConstraintLayout.Widget;
using ConstraintSet = AndroidX.ConstraintLayout.Widget.ConstraintSet;

namespace Lab_4_Android
{
    [Activity(Label = "ConstraintLayout Example")]
    public class ConstraintSetActivity : Activity
    {
        ConstraintLayout constraintLayout;
        Button applyButton, backButton;
        TextView textView1, textView2, textView3, defaultText, textView4, textView5;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Використовуємо activity_constraint.xml
            SetContentView(Resource.Layout.activity_constraint);

            // Отримуємо посилання на ConstraintLayout та кнопки
            constraintLayout = FindViewById<ConstraintLayout>(Resource.Id.constraintLayout);
            applyButton = FindViewById<Button>(Resource.Id.button);
            backButton = FindViewById<Button>(Resource.Id.backButton);

            // Отримуємо елементи
            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            textView2 = FindViewById<TextView>(Resource.Id.textView2);
            textView3 = FindViewById<TextView>(Resource.Id.textView3);
            defaultText = FindViewById<TextView>(Resource.Id.defaultText);

            // При кліку на кнопку "Click me" застосовуємо нові обмеження
            applyButton.Click += (sender, e) => ApplyNewConstraints();

            // При кліку на кнопку "Switch Layout" повертаємося назад
            backButton.Click += (sender, e) => Finish();
        }

        private void ApplyNewConstraints()
        {
            // Створюємо новий ConstraintSet і копіюємо поточні обмеження з макету
            ConstraintSet constraintSet = new ConstraintSet();
            constraintSet.Clone(constraintLayout);

            // 1. Ланцюжок (Chain):
            // створюємо вертикальний ланцюжок для textView1, textView2 та textView3

            //int[] chainIds = new int[] { Resource.Id.textView1, Resource.Id.textView2, Resource.Id.textView3 };
            //constraintSet.CreateVerticalChain(ConstraintSet.ParentId, ConstraintSet.Top,
            //                                    ConstraintSet.ParentId, ConstraintSet.Bottom,
            //                                    chainIds, null, ConstraintSet.ChainPacked);

            // 2. Бар’єр (Barrier):
            // створюємо бар’єр, який визначатиме кінець (End) textView3
            // Створюємо новий об'єкт Barrier програмно та додаємо його до ConstraintLayout

            var barrier = new AndroidX.ConstraintLayout.Widget.Barrier(this);
            barrier.Id = View.GenerateViewId();

            // Додаємо бар’єр із "порожніми" параметрами (не відображається)
            constraintLayout.AddView(barrier, new ConstraintLayout.LayoutParams(0, 0));

            // Створюємо бар’єр 
            constraintSet.CreateBarrier(barrier.Id, ConstraintSet.Start, Resource.Id.textView3);

            // Прикріплюємо кнопку (button) до цього бар’єра: її початок (Start) буде прив'язаний до кінця бар’єра з відступом 16 пікселів
            constraintSet.Connect(Resource.Id.button, ConstraintSet.End, barrier.Id, ConstraintSet.Start, 16);


            // 3. Група: симулюємо групу, задаючи однакове вирівнювання для textView1 та textView2
            // Наприклад, встановлюємо для обох однаковий верхній відступ від батьківського контейнера

            constraintSet.Connect(Resource.Id.textView1, ConstraintSet.Top, ConstraintSet.ParentId, ConstraintSet.Top, 16);
            constraintSet.Connect(Resource.Id.textView2, ConstraintSet.Top, ConstraintSet.ParentId, ConstraintSet.Top, 16);

            // 4. constraintCircle: розташовуємо defaultText по колу відносно textView1

            // Додаємо два нових TextView
            textView4 = new TextView(this);
            textView5 = new TextView(this);

            // Встановлюємо текст
            textView4.Text = "Extra 1";
            textView5.Text = "Extra 2";

            // Призначаємо унікальні ідентифікатори
            textView4.Id = View.GenerateViewId();
            textView5.Id = View.GenerateViewId();

            // Додаємо їх у ConstraintLayout
            constraintLayout.AddView(textView4, new ConstraintLayout.LayoutParams(ConstraintLayout.LayoutParams.WrapContent, ConstraintLayout.LayoutParams.WrapContent));
            constraintLayout.AddView(textView5, new ConstraintLayout.LayoutParams(ConstraintLayout.LayoutParams.WrapContent, ConstraintLayout.LayoutParams.WrapContent));

            // Масив текстових елементів, які потрібно розмістити по колу
            var circleTexts = new TextView[] { textView4, textView5 };

            // КОЛОВА ПРИВ’ЯЗКА (імітація `SetCircleConstraint`)

            // Радіус та початковий кут
            int radius = 200;
            float startAngle = 45f;
            float angleStep = 360f / circleTexts.Length; // Рівномірний розподіл по колу

            foreach (int i in Enumerable.Range(0, circleTexts.Length))
            {
                float angle = startAngle + (i * angleStep);

                constraintSet.ConstrainWidth(circleTexts[i].Id, ConstraintSet.WrapContent);
                constraintSet.ConstrainHeight(circleTexts[i].Id, ConstraintSet.WrapContent);

                // Прив’язуємо елементи до defaultText
                constraintSet.Connect(circleTexts[i].Id, ConstraintSet.Start, defaultText.Id, ConstraintSet.Start);
                constraintSet.Connect(circleTexts[i].Id, ConstraintSet.Top, defaultText.Id, ConstraintSet.Top);

                // Обчислюємо зміщення по колу навколо defaultText
                constraintSet.SetTranslationX(circleTexts[i].Id, (float)(radius * System.Math.Cos(angle * System.Math.PI / 180)));
                constraintSet.SetTranslationY(circleTexts[i].Id, (float)(radius * System.Math.Sin(angle * System.Math.PI / 180)));
            }

            // Застосовуємо всі зміни до ConstraintLayout
            constraintSet.ApplyTo(constraintLayout);
        }
    }
}