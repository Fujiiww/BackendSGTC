using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Text.Json;
using TreinamentosCorp.API.Domain.Entities;

namespace TreinamentosCorp.API.Infra.Database
{
    public class JsonDatabase
    {
        private readonly string _filePath;
        private DatabaseModel _cache;

        public JsonDatabase()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Infra", "Data", "db.json");
            Load();
        }

        public DatabaseModel Data => _cache;

        private void Load()
        {
            if (!File.Exists(_filePath))
                throw new Exception("Arquivo db.json não encontrado.");

            var json = File.ReadAllText(_filePath);
            _cache = JsonSerializer.Deserialize<DatabaseModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_cache, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
