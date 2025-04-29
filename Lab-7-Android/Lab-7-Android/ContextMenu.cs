using Android.Views;
using Android.Graphics;
using AndroidX.AppCompat.App;

namespace Lab_7_Android
{
    // ³������ �� ���������� ���� ���� ������� TextView.
    public class ContextMenu
    {
        private readonly AppCompatActivity _activity;
        private readonly TextView _targetView;
        private readonly int _menuResId;

        public ContextMenu(AppCompatActivity activity, TextView targetView, int menuResId)
        {
            _activity = activity;
            _targetView = targetView;
            _menuResId = menuResId;

            // �������� TextView ��� ������������ ����
            _activity.RegisterForContextMenu(_targetView);
        }

        // ����������� � MainActivity.OnCreateContextMenu
        public void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id != _targetView.Id)
                return;

            _activity.MenuInflater.Inflate(_menuResId, menu);
            menu.SetHeaderTitle("������� ���� ������");
        }

        // ����������� � MainActivity.OnContextItemSelected
        public bool OnContextItemSelected(IMenuItem item)
        {
            Color androidColor;
            string toastText;

            switch (item.ItemId)
            {
                case Resource.Id.menu_red:
                    androidColor = Color.Red;
                    toastText = "��������";
                    break;
                case Resource.Id.menu_green:
                    androidColor = Color.Green;
                    toastText = "�������";
                    break;
                case Resource.Id.menu_blue:
                    androidColor = Color.Blue;
                    toastText = "����";
                    break;
                case Resource.Id.menu_purple:
                    androidColor = Color.Purple;
                    toastText = "Գ��������";
                    break;
                case Resource.Id.menu_default:
                    androidColor = Color.Black;
                    toastText = "�� �������������";
                    break;
                default:
                    return false;
            }

            _targetView.SetTextColor(androidColor);
            Toast.MakeText(_activity, toastText, ToastLength.Short).Show();
            return true;
        }
    }
}