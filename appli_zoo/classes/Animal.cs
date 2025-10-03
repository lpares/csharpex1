namespace appli_zoo.classes
{
    // Classe abstraite
    abstract public class Animal
    {
        // Utilisation de l'encapsulation
        private string name;
        private string specie;
        private string race;
        private int age;

        public Animal(string name, string specie, string race)
        {
            this.name = name;
            this.specie = specie;
            this.race = race;
            this.age = 0;
        }

        public Animal(string name, string specie, string race, int age)
        {
            this.name = name;
            this.specie = specie;
            this.race = race;
            this.age = age;
        }


        public string getName()
        {
            return name;
        }

        public string getSpecie()
        {
            return specie;
        }

        public string getRace()
        {
            return race;
        }

        public int getAge()
        {
            return age;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setSpecie(string specie)
        {
            this.specie = specie;
        }

        public void setRace(string race)
        {
            this.race = race;
        }

        public void setAge(int age)
        {
            this.age = age;
        }
    }
}
