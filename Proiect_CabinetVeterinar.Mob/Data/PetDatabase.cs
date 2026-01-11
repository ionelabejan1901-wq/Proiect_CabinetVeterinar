using Proiect_CabinetVeterinar.Mob.Models;

using SQLite;

namespace Proiect_CabinetVeterinar.Mob.Data;

public class PetDatabase
{
    readonly SQLiteAsyncConnection _database;

    public PetDatabase(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<PetModel>().Wait();
    }

    public Task<List<PetModel>> GetPetsAsync() =>
        _database.Table<PetModel>().ToListAsync();

    public Task<int> SavePetAsync(PetModel pet) =>
        pet.Id != 0 ? _database.UpdateAsync(pet) : _database.InsertAsync(pet);

    public Task<int> DeletePetAsync(PetModel pet) =>
        _database.DeleteAsync(pet);
}
