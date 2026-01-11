using Proiect_CabinetVeterinar.Mob.Data;
using System.IO;
namespace Proiect_CabinetVeterinar.Mob;
public partial class App : Application
{
    static PetDatabase database;

    public static PetDatabase Database
    {
        get
        {
            if (database == null)
            {
                database = new PetDatabase(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Pets.db3"));
            }
            return database;
        }
    }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}
