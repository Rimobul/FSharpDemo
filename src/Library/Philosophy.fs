namespace DemoLib

module Philosophy = 

    // C# - mutable, nullable, comparison by reference, non-explicit exceptions, 
    //      private by default, statement-oriented, more imperative
    // F# - immutable, non-nullable, comparison by value, explicit exceptions, 
    //      public by default, expression-oriented, more declarative
    // https://fsharpforfunandprofit.com/posts/key-concepts/

    let doSomething f x = 
        let y = f(x + 1)
        "hello" + y

    // Function composition
    let add1 x = 
        x + 1

    let double x = 
        x + x 

    let square x =
        x * x

    let doubleAndAdd1 = 
        double >> add1

    let add1AndDouble = 
        add1 >> double

    let intToString (x: int) = 
        string x

    let pipingTest x = 
        // classic C-sharp code
        let res1 = square(double(add1(x)))
        // piping
        let res2 = x |> add1 |> double |> square
        // composition
        let composedFn = square << double << add1
        let res3 = composedFn x
        // values in an array are separated by semicolon ;
        // comma represents a vector
        [res1; res2; res3]