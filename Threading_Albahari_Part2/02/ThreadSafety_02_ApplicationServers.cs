using System.Collections.Generic;
using System.Threading;

namespace Threading_Albahari_Part2
{
    /// <summary>
    /// Безопасность потоков на серверах приложений
    ///
    /// Серверы приложений должны быть многопоточными для обработки одновременных клиентских запросов.
    /// Приложения WCF, ASP.NET и веб-служб являются неявно многопоточными;
    /// то же самое верно и для серверных приложений удаленного взаимодействия, которые используют сетевой канал, такой как TCP или HTTP.
    /// Это означает, что при написании кода на стороне сервера вы должны учитывать безопасность потоков,
    /// если есть какая-либо возможность взаимодействия между потоками, обрабатывающими клиентские запросы.
    /// К счастью, такая возможность редка;
    /// типичный серверный класс либо не имеет состояния (без полей),
    /// либо имеет модель активации, которая создает отдельный экземпляр объекта для каждого клиента или каждого запроса.
    /// Взаимодействие обычно возникает только через статические поля, иногда используемые для кэширования в частях памяти базы данных для повышения производительности.
    /// </summary>
    public class ThreadSafety_02_ApplicationServers
    {
        public static void Start()
        {
            new Thread(() =>
            {
                var user1 = UserCache.GetUser(1);
            }).Start();
            
            new Thread(() =>
            {
                var user2 = UserCache.GetUser(1);
            }).Start();
        }
    }

    class User
    {
        public int Id { get; set; }
    }

    class Database
    {
        private static readonly User[] Users = { new() { Id = 1 }, new() { Id = 2 } };
        
        /// <summary>
        /// Получить юзера из БД
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Пользователь</returns>
        public static User RetriveUser(int id)
        {
            return (User)Users.GetValue(id);
        }
    }

    /// <summary>
    /// Пример потокобезопасного кэша пользователей
    /// </summary>
    static class UserCache
    {
        private static Dictionary<int, User> _users = new Dictionary<int, User>();

        public static User GetUser(int id)
        {
            User user = null;

            // Блокируем и получаем пользователя из кэша
            lock(_users)
                if (_users.TryGetValue(id, out user))
                    return user;

            user = Database.RetriveUser(id); // Получаем пользователя из БД
            lock (_users) // блокируем кэш и сохраняем пользователя
                _users[id] = user; 
            
            return user;
        }
    }
}