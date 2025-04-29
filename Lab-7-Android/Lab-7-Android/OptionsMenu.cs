using Android.Views;
using AndroidX.AppCompat.App;

namespace Lab_7_Android
{
    public class OptionsMenu
    {
        readonly AppCompatActivity _activity;

        public OptionsMenu(AppCompatActivity activity)
        {
            _activity = activity;
        }

        // ���� ���������� � XML
        public void Populate(IMenu menu)
        {
            menu.Clear();
            _activity.MenuInflater.Inflate(Resource.Menu.options_menu, menu);
            // ��������� �������� ������ � overflow
            EnableIconsInOverflow(menu);
        }

        // ������� ����
        public bool HandleSelection(IMenuItem item, System.Action<string> onReorder)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_profile:
                case Resource.Id.menu_settings:
                case Resource.Id.menu_messages:
                case Resource.Id.menu_help:
                case Resource.Id.menu_logout:
                    // �������� toast � ������
                    Toast.MakeText(_activity, item.TitleFormatted, ToastLength.Short).Show();
                    return true;

                case Resource.Id.menu_reorder:
                    // ��������� popup-���� ����������
                    onReorder?.Invoke("");// callback ������ ���������� � InvalidateOptionsMenu
                    return true;
            }
            return false;
        }

        // ��������, ��� ������ � XML ������������ � ������
        void EnableIconsInOverflow(IMenu menu)
        {
            try
            {
                // �������� IMenu �� Java-��'����
                var menuJavaObj = menu as Java.Lang.Object;
                if (menuJavaObj == null)
                    return;

                // �������� ���� � ����� �������� �������� ����������� ������
                var menuClass = menuJavaObj.Class;
                if (menuClass.SimpleName.Equals("MenuBuilder"))
                {
                    var setIconsMethod = menuClass.GetDeclaredMethod(
                        "setOptionalIconsVisible",
                        Java.Lang.Boolean.Type
                    );
                    setIconsMethod.Accessible = true;
                    setIconsMethod.Invoke(menuJavaObj, true);
                }
            }
            catch
            {
                // ���� ���� ���� �� ��� � ������ ���������
            }
        }
    }
}