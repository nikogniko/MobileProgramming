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

        // Будує тулбар–меню з XML
        public void Populate(IMenu menu)
        {
            menu.Clear();
            _activity.MenuInflater.Inflate(Resource.Menu.options_menu, menu);
            // Примусово увімкнути іконки в overflow
            EnableIconsInOverflow(menu);
        }

        // Обробка кліку
        public bool HandleSelection(IMenuItem item, System.Action<string> onReorder)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_profile:
                case Resource.Id.menu_settings:
                case Resource.Id.menu_messages:
                case Resource.Id.menu_help:
                case Resource.Id.menu_logout:
                    // Виводимо toast з назвою
                    Toast.MakeText(_activity, item.TitleFormatted, ToastLength.Short).Show();
                    return true;

                case Resource.Id.menu_reorder:
                    // Викликаємо popup-меню сортування
                    onReorder?.Invoke("");// callback виконає сортування і InvalidateOptionsMenu
                    return true;
            }
            return false;
        }

        // Рефлексія, щоб іконки з XML відображались у списку
        void EnableIconsInOverflow(IMenu menu)
        {
            try
            {
                // Приводим IMenu до Java-об'єкту
                var menuJavaObj = menu as Java.Lang.Object;
                if (menuJavaObj == null)
                    return;

                // Отримуємо клас і через рефлексію включаємо відображення іконок
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
                // якщо щось пішло не так — просто пропускаєм
            }
        }
    }
}