using System.Collections.Generic;
using SQLite.Net;
using Xamarin.Forms;

namespace XamarinSQLiteSample
{
    public class TodoRepository
    {
        static readonly object Locker = new object();
        readonly SQLiteConnection _db;

        public TodoRepository()
        {
            _db = DependencyService.Get<ISQLite>().GetConnection();//データベース接続
            _db.CreateTable<TodoItemModel>();//テーブル作成
        }
        //一覧
        public IEnumerable<TodoItemModel> GetItems()
        {
            lock (Locker)
            {
                //Delete==falseの一覧を取得する(削除フラグが立っているものは対象外)
                return _db.Table<TodoItemModel>().Where(m => m.Delete == false);
            }
        }
        //更新・追加
        public int SaveItem(TodoItemModel item)
        {
            lock (Locker)
            {
                if (item.ID != 0)
                {//IDが0で無い場合は、更新
                    _db.Update(item);
                    return item.ID;
                }
                return _db.Insert(item);//追加
            }
        }

        public int DeleteItem(TodoItemModel item)
        {
            lock (Locker)
            {
                return _db.Delete(item);//追加
            }
        }

        public int DeleteAll()
        {
            lock (Locker)
            {
                return _db.DeleteAll<TodoItemModel>();
            }
        }
    }
}
