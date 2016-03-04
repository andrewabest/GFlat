// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open System
open System.IO
open Suave
open Suave.Http
open Suave.Web
open Suave.Filters
open Suave.Operators
open Suave.Successful
open Suave.Writers
open Suave.Response
open Suave.Files

[<EntryPoint>]
let main argv = 

    let html = "This is my html!"

    let getTodos = fun x -> "TODO"

    let add input = input |> ignore

    let remove id = id |> ignore

    let index = GET >=> choose [ path "/" >=> OK html ]
    let staticContent = GET >=> choose [ pathRegex "\/static\/(.*)\.(css|js)" >=> Files.browseHome ]
    let actions = [ 
        GET >=> choose
            [ path "/todos" >=> request (fun req -> OK (getTodos ())) ]   
        POST >=> choose
            [ path "/todos" >=> request (fun req -> add (req.formData "text") ; OK "Added!") ]
        DELETE >=> choose
            [ pathScan "/todos/%d" (fun (id) -> remove id ; OK "Removed!") ]       
    ]

    let app = choose (List.append [index;staticContent] actions)

    startWebServer defaultConfig app
    
    System.Console.Read() |> (fun x -> 0)
