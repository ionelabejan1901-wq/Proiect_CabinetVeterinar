using Proiect_CabinetVeterinar.Mob.Models;

namespace Proiect_CabinetVeterinar.Mob;

public partial class PetEntryPage : ContentPage
{
    PetModel pet;

    public PetEntryPage(PetModel petToEdit = null)
    {
        InitializeComponent();
        pet = petToEdit ?? new PetModel();

        nameEntry.Text = pet.Name;
        speciesEntry.Text = pet.Species;
        breedEntry.Text = pet.Breed;
        birthDatePicker.Date = pet.BirthDate == DateTime.MinValue ? DateTime.Today : pet.BirthDate;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        pet.Name = nameEntry.Text;
        pet.Species = speciesEntry.Text;
        pet.Breed = breedEntry.Text;
        pet.BirthDate = birthDatePicker.Date;

        await App.Database.SavePetAsync(pet);
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
