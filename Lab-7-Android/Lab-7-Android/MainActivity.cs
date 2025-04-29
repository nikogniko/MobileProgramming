using Android.Views;
using AndroidX.AppCompat.App;
using _Microsoft.Android.Resource.Designer;
using Android.Graphics;

namespace Lab_7_Android
{
    [Activity(Label = "Lab_7_Android", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ContextMenu _contextMenu;

        OptionsMenu _opts;
        List<(int id, string title)> _defaultOrder, _currentOrder;

        private ListView listView;
        private List<string> items;
        private ArrayAdapter<string> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            _opts = new OptionsMenu(this);

            _defaultOrder = new()
            {
                (Resource.Id.menu_profile,   "Профіль"),
                (Resource.Id.menu_settings,  "Налаштування"),
                (Resource.Id.menu_messages,  "Повідомлення"),
                (Resource.Id.menu_help,      "Допомога"),
                (Resource.Id.menu_logout,    "Вихід")
            };
            _currentOrder = new(_defaultOrder);

            // Знаходимо TextView в макеті
            var tv = FindViewById<TextView>(Resource.Id.textViewColor);

            // Ініціалізуємо наш ContextMenu
            _contextMenu = new ContextMenu(this, tv, Resource.Menu.context_menu);

            listView = FindViewById<ListView>(Resource.Id.listViewItems);
            items = ["Елемент 1", "Елемент 2", "Елемент 3", "Елемент 4","Елемент 5"];
            adapter = new ArrayAdapter<string>(this,
            Android.Resource.Layout.SimpleListItem1, items);
            listView.Adapter = adapter;

            listView.ChoiceMode = ChoiceMode.Single;
            listView.ItemLongClick += (s, e) => StartActionMode(new
            ListActionModeCallback(this, e.Position));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            _opts.Populate(menu);

            // Якщо у нас уже є відмінний порядок — перерозміщуємо пункти перед overflow
            if (!AreEqual(_defaultOrder, _currentOrder))
            {
                // Забираємо всі, окрім reorder
                menu.Clear();
                foreach (var (id, title) in _currentOrder)
                {
                    var mi = menu.Add(0, id, 0, title);
                    mi.SetIcon(GetDrawableForId(id));
                    mi.SetShowAsAction(ShowAsAction.Never);
                }
                // Додаємо reorder справа
                menu.Add(0, Resource.Id.menu_reorder, 0, "")
                    .SetIcon(Resource.Drawable.reorder)
                    .SetShowAsAction(ShowAsAction.Always);
            }

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // Delegate to OptionsMenu to show Toast
            if (_opts.HandleSelection(item, null))
            {
                // Для будь-якого пункту (включно з profile…logout) відкриваємо Popup
                var anchor = FindViewById(Resource.Id.menu_reorder);
                ReorderPopupMenu.Show(this, anchor, sortType =>
                {
                    SortCurrentOrder(sortType);
                    InvalidateOptionsMenu();  // щоб знову викликати OnCreateOptionsMenu
                });
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        void SortCurrentOrder(string sortType)
        {
            _currentOrder = sortType switch
            {
                "asc" => _currentOrder.OrderBy(x => x.title).ToList(),
                "desc" => _currentOrder.OrderByDescending(x => x.title).ToList(),
                _ => new List<(int, string)>(_defaultOrder),
            };
        }

        bool AreEqual(
            List<(int, string)> a,
            List<(int, string)> b) =>
            a.Count == b.Count && !a.Where((t, i) => t!=b[i]).Any();

        int GetDrawableForId(int id) => id switch
        {
            Resource.Id.menu_profile => Resource.Drawable.profile,
            Resource.Id.menu_settings => Resource.Drawable.settings,
            Resource.Id.menu_messages => Resource.Drawable.messages,
            Resource.Id.menu_help => Resource.Drawable.help,
            Resource.Id.menu_logout => Resource.Drawable.logout,
            _ => 0
        };

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            _contextMenu.OnCreateContextMenu(menu, v, menuInfo);
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (_contextMenu.OnContextItemSelected(item))
                return true;
            return base.OnContextItemSelected(item);
        }

        private sealed class ListActionModeCallback : Java.Lang.Object, ActionMode.ICallback
        {
            private readonly MainActivity _activity;
            private readonly int _position;
            public ListActionModeCallback(MainActivity activity, int position)
            {
                _position = position;
                _activity = activity;
            }
            public bool OnActionItemClicked(ActionMode? mode, IMenuItem? item)
            {
                if (item!.ItemId != ResourceConstant.Id.menu_bold)
                {
                    return false;
                }
                _activity.listView.GetChildAt(_position)!.SetBackgroundColor(Color.Yellow);
                mode!.Finish();
                return true;
            }
            public bool OnCreateActionMode(ActionMode? mode, IMenu? menu)
            {
                mode!.MenuInflater!.Inflate(ResourceConstant.Menu.cab_menu,
                menu);
                return true;
            }
            public void OnDestroyActionMode(ActionMode? mode) { }
            public bool OnPrepareActionMode(ActionMode? mode, IMenu? menu) =>
            false;
        }
    }
}