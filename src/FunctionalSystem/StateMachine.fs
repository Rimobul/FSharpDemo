namespace FunctionalSystem

module Automobile = 
    type AutomobileState = 
        | NotRunning
        | Idling
        | Moving

    type AutomobileEvent = 
        | Ignition
        | PutInDrive
        | TurnLeft
        | TurnRight
        | Stop
        | TurnOff

    // this works, but can cause an exception in case
    // of invalid state and event combination
    let stateMachine (state, event) = 
        match state, event with
        | NotRunning, Ignition
            -> Idling
        | Idling, PutInDrive
            -> Moving
        | Moving, TurnLeft
            -> Moving
        | Moving, TurnRight
            -> Moving
        | Moving, Stop
            -> Idling
        | Idling, TurnOff
            -> NotRunning
        | _
            -> failwith "Invalid state transition"

    // to ensure only valid combinations, we need some helper functions
    let private stateTransitions event = 
        match event with
        | Ignition
            -> Idling
        | PutInDrive
            -> Moving
        | TurnLeft
            -> Moving
        | TurnRight
            -> Moving
        | Stop
            -> Idling
        | TurnOff
            -> NotRunning

    let private getEventsForState state = 
        match state with
        | NotRunning
            -> [| Ignition |]
        | Idling
            -> [| PutInDrive; TurnOff |]        
        | Moving
            -> [| TurnLeft; TurnRight; Stop |]    

    // circular reference is allowed in F# with the and keyword
    // AllowedEvent is a record containing the Event itself and a functions
    // which tells us what happens after the event is invoked
    type AllowedEvent = 
        {
            EventInfo : AutomobileEvent
            RaiseEvent : unit -> EventResult
        }
    // EventResult is a record which contains the new state resulting from 
    // an event and all the possible events the new state can react to
    and EventResult = 
        {
            CurrentState: AutomobileState
            AllowedEvents: AllowedEvent array
        }

    // a private recursive function
    // takes an AutomobileEvent as input
    // returns a record of type EventResult, which contains new state 
    //      and allowed events for the new state
    let rec private constrainedStateMachine event = 
        // 1. we get the new state from the current event
        let newState = stateTransitions event
        // 2. we generate all possible new events for the new state
        let newEvents = getEventsForState newState
        // 3. we create the result record
        {
            // 4. in the result, the CurrentState will be the new state from the current event
            CurrentState = newState
            // 5. and the AllowedEvents will be an array of AllowedEvent records from the newEvents array
            AllowedEvents = 
                newEvents
                |> Array.map (fun newEvent ->
                    // 6. for each new event we get the transition 
                    //      determined by the recursive constrainedStateMachine method
                    let innerRaiseEvent() = constrainedStateMachine newEvent
                    // 6A. we could write also the following code, which would
                    //      return the new state, not the transition. However,
                    //      this would result in a never-ending recursion.
                    // let innerNewState = constrainedStateMachine newEvent

                    // 7. then for each new event we create a new AllowedEvent record
                    {
                        // 8. the AllowedEvent record will contain information about the
                        //      event to which it belongs
                        EventInfo = newEvent
                        // 9. and also the transition function
                        RaiseEvent = innerRaiseEvent
                    }
                )
        }

    let init() = constrainedStateMachine TurnOff    
