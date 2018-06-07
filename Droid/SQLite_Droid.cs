using System.IO;
using XamarinSQLiteSample.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace XamarinSQLiteSample.Droid
{
    public class SQLite_Android : ISQLite
    {
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            const string sqliteFilename = "TodoSQLite.db3"; //データベース名
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);//Documentsフォルダ
            var path = Path.Combine(documentsPath, sqliteFilename);//DBファイルのパス
            var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);
            return conn;
        }
    }
}
