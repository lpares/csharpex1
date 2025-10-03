namespace appli_zoo.classes
{
    public class Monkey : Animal
    {
        public Monkey(string name, string specie, string race)
            : base(name, specie, race)
        {
        }

        public string talk()
        {
            return "ouga bouga";
        }

        // Utilisation du polymorphisme
        public string talk(string text)
        {
            return text;
        }
    }
}
