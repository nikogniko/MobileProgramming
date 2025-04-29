using Android.Views;

namespace Lab_7_Android
{
    public static class ReorderPopupMenu
    {
        // Показує PopupMenu, інфлейтить reorder_popup_menu.xml
        public static void Show(Activity activity, View anchor, System.Action<string> onSort)
        {
            var popup = new PopupMenu(activity, anchor);
            popup.MenuInflater.Inflate(Resource.Menu.reorder_popup_menu, popup.Menu);

            popup.MenuItemClick += (s, e) =>
            {
                string sortType = e.Item.ItemId switch
                {
                    Resource.Id.popup_sort_asc => "asc",
                    Resource.Id.popup_sort_desc => "desc",
                    Resource.Id.popup_sort_default => "default",
                    _ => "default"
                };
                onSort(sortType);
            };

            popup.Show();
        }
    }
}