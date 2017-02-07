// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open System.Drawing
open System.Windows.Forms

[<EntryPoint>]
let main argv =
    let mutable counter=0; 
    while counter < 45 do  
        counter <- counter+1       
        Cursor.Position <- new Point(Cursor.Position.X+10,Cursor.Position.Y+1)
        System.Threading.Thread.Sleep(60000)             
    0
