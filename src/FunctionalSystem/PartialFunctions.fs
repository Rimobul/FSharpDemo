namespace FunctionalSystem

open System
open System.Globalization

// 2.
module PartialFunctions = 
    let add x y =
        x + y

    let add2 = add 2

    let addTuple (x, y) = 
        x + y

    let adapterFx x y =
        addTuple (x, y)

    let add2adapter = adapterFx 2    

    let doOperation f x y = 
        f x y

    let doAdd = doOperation add

    doAdd 8 9 |> ignore

    let doMultiply = doOperation (*)

    doMultiply 6 8 |> ignore

    // Do not use partial functions for interoperability!!!

    // this would be done in  C#
    let stringCompareC s1 s2 = 
        String.Compare(s1, s2, true, CultureInfo.InvariantCulture)

    // this way it is better for F#
    // first, do an adapter function
    let stringCompareAdapter
        (ignoreCase: bool)
        (cultureInfo: CultureInfo)
        s1 s2 = 
            String.Compare(s1, s2, ignoreCase, cultureInfo)

    // then create a partial function
    let stringCompareF = 
        stringCompareAdapter true CultureInfo.InvariantCulture