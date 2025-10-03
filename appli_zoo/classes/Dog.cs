namespace appli_zoo.classes
{
    public class Dog : Animal
    {
        // Utilisation de l'héritage d'une classe abstraite
        public Dog(string name, string specie, string race)
            : base(name, specie, race)
        {
        }

        public string aboyer()
        {
            return "ouaf !";
        }
    }
}