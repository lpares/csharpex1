using System;
using appli_zoo.classes;

// Exemple avec Dog
Dog monChien = new Dog("Rex", "Canis lupus", "Labrador");
Console.WriteLine("=== Chien ===");
Console.WriteLine($"Nom : {monChien.getName()}");
Console.WriteLine($"Espèce : {monChien.getSpecie()}");
Console.WriteLine($"Race : {monChien.getRace()}");
Console.WriteLine($"Âge : {monChien.getAge()}");

monChien.setAge(3);
monChien.setName("Max");
Console.WriteLine($"\nNouveau nom : {monChien.getName()}");
Console.WriteLine($"Nouvel âge : {monChien.getAge()}");
Console.WriteLine($"{monChien.getName()} dit : {monChien.aboyer()}");

// Exemple avec Monkey
Monkey monSinge = new Monkey("Kong", "Pan troglodytes", "Chimpanzé");
Console.WriteLine("\n=== Singe ===");
Console.WriteLine($"Nom : {monSinge.getName()}");
Console.WriteLine($"Espèce : {monSinge.getSpecie()}");
Console.WriteLine($"Race : {monSinge.getRace()}");
Console.WriteLine($"Âge : {monSinge.getAge()}");

monSinge.setAge(5);
Console.WriteLine($"\n{monSinge.getName()} dit : {monSinge.talk()}");
Console.WriteLine($"\n{monSinge.getName()} répète : {monSinge.talk("Bonjour !")}");
