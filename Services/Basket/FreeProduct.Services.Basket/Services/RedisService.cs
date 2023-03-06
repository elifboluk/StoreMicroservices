using StackExchange.Redis;

namespace FreeProduct.Services.Basket.Services
{
    public class RedisService
    {
        private readonly string _host;

        private readonly int _port;

        private ConnectionMultiplexer _ConnectionMultiplexer; // Redis ile bağlantı kurabilmek için gerekli sınıf.

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public void Connect() => _ConnectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}"); // Bağlantı kuruldu.

        public IDatabase GetDb(int db = 1) => _ConnectionMultiplexer.GetDatabase(db); // Redis veritabanlarından 1. veritabanına kaydet.
    }
}
