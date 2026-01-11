using Proiect_CabinetVeterinar.Mob.Models;
using System.Collections.ObjectModel;

namespace Proiect_CabinetVeterinar.Mob;

public partial class PetListPage : ContentPage
{
    ObservableCollection<PetModel> Pets = new();

    public PetListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var petsFromDb = await App.Database.GetPetsAsync();
        Pets = new ObservableCollection<PetModel>(petsFromDb);
        petListView.ItemsSource = Pets;
    }

    private async void OnAddPetClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PetEntryPage());
    }

    private async void OnEditClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var pet = button?.BindingContext as PetModel;
        if (pet != null)
        {
            await Navigation.PushAsync(new PetEntryPage(pet));
        }
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var pet = button?.BindingContext as PetModel;
        if (pet != null)
        {
            var confirm = await DisplayAlert("Confirmare", $"Ștergi {pet.Name}?", "Da", "Nu");
            if (confirm)
            {
                await App.Database.DeletePetAsync(pet);
                Pets.Remove(pet);
            }
        }
    }
}
