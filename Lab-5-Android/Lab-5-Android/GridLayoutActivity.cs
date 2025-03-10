using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Reflection;

namespace Lab_5_Android
{
    [Activity(Label = "GridLayout")]
    public class GridLayoutActivity : Activity
    {
        GridLayout gridLayout;
        Button backButton;

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Використовуємо activity_gridlayout.xml
            SetContentView(Resource.Layout.activity_gridlayout);

            // Отримуємо посилання на GridLayout та кнопку
            gridLayout = FindViewById<GridLayout>(Resource.Id.gridLayout);
            backButton = FindViewById<Button>(Resource.Id.backButton);

            // При кліку на кнопку повертаємося назад
            backButton.Click += (sender, e) => Finish();

            // Чекаємо, коли макет буде виміряний, щоб встановити ширину для всіх елементів
            gridLayout.ViewTreeObserver.GlobalLayout += GridLayout_GlobalLayout;
        }

        private void GridLayout_GlobalLayout(object sender, EventArgs e)
        {
            // Видаляємо обробник, щоб виконати код лише один раз
            gridLayout.ViewTreeObserver.GlobalLayout -= GridLayout_GlobalLayout;

            // Розраховуємо доступну ширину (без padding)
            int totalWidth = gridLayout.Width - gridLayout.PaddingLeft - gridLayout.PaddingRight;
            int colCount = gridLayout.ColumnCount;
            int cellWidth = totalWidth / colCount;

            // Проходимо по всіх дочірніх елементах GridLayout
            for (int i = 0; i < gridLayout.ChildCount; i++)
            {
                var child = gridLayout.GetChildAt(i);
                if (child.LayoutParameters is GridLayout.LayoutParams lp)
                {
                    // Отримуємо кількість стовпців, яку займає елемент.
                    //int span = GetColumnSpan(lp);
                    // Встановлюємо ширину для елемента: cellWidth * span.
                    //lp.Width = cellWidth * span;
                    lp.Width = cellWidth;
                    child.LayoutParameters = lp;
                }
            }
        }
    }
}