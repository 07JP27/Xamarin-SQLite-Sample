using Xamarin.Forms;
using System;

namespace XamarinSQLiteSample
{
    public partial class Xamarin_SQLite_SamplePage : ContentPage
    {
        readonly TodoRepository _db = new TodoRepository();

        public Xamarin_SQLite_SamplePage()
        {
            InitializeComponent();
            list.ItemsSource = _db.GetItems();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(entry.Text))
            {//Entryに文字列が入力されている場合に処理する
                var item = new TodoItemModel { Text = entry.Text, CreatedAt = DateTime.Now, Delete = false };
                _db.SaveItem(item);
                list.ItemsSource = _db.GetItems();//リスト更新
                entry.Text = "";
            }
        }

        private void DropClicked(object sender, EventArgs e)
        {
            _db.DeleteAll();
            list.ItemsSource = _db.GetItems();//リスト更新
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (TodoItemModel)e.Item;
            if (await DisplayAlert("削除してい宜しいですか", item.Text, "OK", "キャンセル"))
            {
                item.Delete = true;//削除フラグを有効にして
                _db.DeleteItem(item);//DB更新
                list.ItemsSource = _db.GetItems();//リスト更新
            }
        }
    }
}
