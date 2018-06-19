namespace FunctionalSystem

open System

module Car =
    let getCar() = 
        let res1 = Automobile.init()
        let res2 = res1.AllowedEvents.[0].RaiseEvent()

        Console.WriteLine "Result 1"
        Console.WriteLine res1
        Console.WriteLine "Result 2"
        Console.WriteLine res2

    let hello name =
        printfn "Hello %s" name
