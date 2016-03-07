// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System

[<EntryPoint>]
let main argv = 

    let readSomeInput () = System.Console.ReadLine() |> Some

    let words = 
        [
            1, "One";
            2, "Two";
            3, "Three";
            4, "Four";
            5, "Five";
            6, "Six";
            7, "Seven";
            8, "Eight";
            9, "Nine";
            0, "Ten";
        ] 
        |> Map.ofList

    let alternateWorder number =
        match words.TryFind(number) with
            | Some number -> number
            | None -> ""

    let worder number = 
        match number with
            | 1 -> "One"
            | 2 -> "Two"
            | 3 -> "Three"
            | 4 -> "Four"
            | 5 -> "Five"
            | 6 -> "Six"
            | 7 -> "Seven"
            | 8 -> "Eight"
            | 9 -> "Nine"
            | 0 -> "Zero"
            | _ -> "Bajillion"

    let builder numbers = 
        let accumulator = fun acc elem -> acc + (alternateWorder elem)
        let result = numbers |> List.fold accumulator ""
        printfn "%s" result

    let splitter number = 
        List.ofSeq(number.ToString().ToCharArray()) |> List.map (fun x -> System.Int32.Parse (x.ToString())) 

    let rec inputReader input = 
        match input with
            | None -> readSomeInput () |> inputReader
            | Some "exit" -> 0
            | Some candidate -> 
                let success, value = System.Int32.TryParse candidate 
                match success with
                    | true -> builder (splitter value)
                    | false -> printfn "Please enter a number eg 1234 and try again"
                readSomeInput () |> inputReader
    
    printfn "Please enter a number eg 1234"

    let result = readSomeInput () |> inputReader

    0 // return an integer exit code
