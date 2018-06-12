namespace DemoLib
open System

module Person = 
// By default, everything is public
    // And nothing is nullable
    [<AllowNullLiteral>]
    type OldPerson(name: string, birthday: DateTime) = 

        // default return, expression-oriented language, not statement oriented
        member this.Name = 
            name 

        member this.Birthday = 
             birthday

        member this.Age() = 
            let daysDiff = DateTime.Today.Subtract(birthday).Days
            daysDiff / 365

    // A record type (like DTO)
    type NewPerson = {
        Name: string
        Birthday: DateTime
    }

    // A separate function
    let age person = 
        let daysDiff = DateTime.Today.Subtract(person.Birthday).Days
        daysDiff / 365

    let mutateOne number = 
        let mutable one = 1
        one <- number
        one

    let assignNull = 
        let oldPerson: OldPerson = null
        // This does not work
        // let newPerson: NewPerson = null
        oldPerson

    let compareByStructureNotReference name birthday = 
        // This does not work
        // let alice: NewPerson
        // let bob = { Name = "Bob" }
        // ...when I add a new property, all usages brake

        let firstPerson = { Name = name; Birthday = birthday } 
        let secondPerson = { Name = name; Birthday = birthday }
        firstPerson = secondPerson