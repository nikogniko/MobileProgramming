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

            // ������������� activity_gridlayout.xml
            SetContentView(Resource.Layout.activity_gridlayout);

            // �������� ��������� �� GridLayout �� ������
            gridLayout = FindViewById<GridLayout>(Resource.Id.gridLayout);
            backButton = FindViewById<Button>(Resource.Id.backButton);

            // ��� ���� �� ������ ����������� �����
            backButton.Click += (sender, e) => Finish();

            // ������, ���� ����� ���� ��������, ��� ���������� ������ ��� ��� ��������
            gridLayout.ViewTreeObserver.GlobalLayout += GridLayout_GlobalLayout;
        }

        private void GridLayout_GlobalLayout(object sender, EventArgs e)
        {
            // ��������� ��������, ��� �������� ��� ���� ���� ���
            gridLayout.ViewTreeObserver.GlobalLayout -= GridLayout_GlobalLayout;

            // ����������� �������� ������ (��� padding)
            int totalWidth = gridLayout.Width - gridLayout.PaddingLeft - gridLayout.PaddingRight;
            int colCount = gridLayout.ColumnCount;
            int cellWidth = totalWidth / colCount;

            // ��������� �� ��� ������� ��������� GridLayout
            for (int i = 0; i < gridLayout.ChildCount; i++)
            {
                var child = gridLayout.GetChildAt(i);
                if (child.LayoutParameters is GridLayout.LayoutParams lp)
                {
                    // �������� ������� ��������, ��� ����� �������.
                    //int span = GetColumnSpan(lp);
                    // ������������ ������ ��� ��������: cellWidth * span.
                    //lp.Width = cellWidth * span;
                    lp.Width = cellWidth;
                    child.LayoutParameters = lp;
                }
            }
        }
    }
}