namespace FunctionalSystem

// 3.
module FunctionComposition = 
    // compose only pure functions
    let subtract x y = x - y

    subtract 9 3 |> ignore

    3
    |> subtract 9
    |> subtract 88
    |> ignore

    // works also the other way
    subtract 88 <| (subtract 9 <| 3) 
    |> ignore

    let square x = x * x
    let triple x = x * 3.0
    let negate x = -x

    let toFloat x = (float)x
    let toInt (x:float) = (int)x

    let squareAndTripleAndNegate = square >> toFloat >> triple >> toInt >> negate

    squareAndTripleAndNegate 2
    |> ignore

    let add x y = x + y
    
    // this will not work because of different number of params
    // let addAndSquare = add >> square

    // to make it work, we need to add an adapter function
    let splitPair f (x:int, y:int) = f x y

    let addAndSquare = splitPair add >> square