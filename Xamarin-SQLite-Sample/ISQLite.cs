using SQLite.Net;

namespace XamarinSQLiteSample
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
