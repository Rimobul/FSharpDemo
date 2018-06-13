namespace FunctionalSystem

// 1.
module DependencyInjection = 
    type Application = Application
    type Ids = Ids
    type Deposit = Deposit
    type CheckingAccount = CheckingAccount

    let validateApplication (application: Application) =
        true

    let openCheckingAccount
        validateApplication
        validateIds
        validateDeposit
        (application: Application)
        (ids: Ids)
        (deposit: Deposit) = 

        if not (validateApplication application) then None
        elif not (validateIds ids) then None
        elif not (validateDeposit deposit) then None
        else Some CheckingAccount
    