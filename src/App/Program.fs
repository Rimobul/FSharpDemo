namespace DemoApp

open DemoLib
open FunctionalSystem

module App = 
    [<EntryPoint>]
    let main argv =
        printfn "Nice command-line arguments! Here's what JSON.NET has to say about them:"

        argv
        |> Array.map Library.getJsonNetJson
        |> Array.iter (printfn "%s")

        printfn "Calling a car"
        Car.getCar()


        0 // return an integer exit code
    