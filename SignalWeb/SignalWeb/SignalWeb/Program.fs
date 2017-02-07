open System
open Owin
open Microsoft.Owin
open SignalXLib.Lib
open Microsoft.Owin.Hosting

type public Startup() =
    member x.Configuration (app:IAppBuilder) = app.UseSignalX( new SignalX()) |> ignore
let  index = @"<!DOCTYPE html>
							<html>
							<body>
							<input id='message' type='text'/>
							<button id='send'>Send Message To Server</button>
							<button id='clear'>Clear Console</button>
							<div id='myconsole'></div>
							<script src='https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.9.0.min.js'></script>     
							<script src='https://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.2.0.js'></script>
							<script src='https://unpkg.com/signalx'></script>
							<script>
							    var writeToMyConsole = function(m) {
							        $('#myconsole').append('<BR /><BR />'+m);
							    };
							    signalx.debug(function (o) { writeToMyConsole(o); });
							    signalx.error(function (o) { writeToMyConsole(o); });
							    signalx.ready( function(server) {
                                signalx.server('Sample', function (request) {
                                     request.respond('hello');
		                          });
	                            });
							    $('#clear').on('click', function () {
							        $('#myconsole').html('--cleared--');
							    });
							    $('#send').on('click', function () {
							        var message = $('#message').val();
							        signalx.server.sample('MY MESSAGE',  function (m) {
							            writeToMyConsole(m);
							        });
							    });
							</script>
							</body>
							</html>"
[<EntryPoint>]
let main argv = 
  let url="http://localhost:44111"
  let filePath=AppDomain.CurrentDomain.BaseDirectory+"\\index.html"
  System.IO.File.WriteAllText(filePath,index)
  use server=WebApp.Start<Startup>(url)
  SignalX.Server("Sample",fun request -> request.RespondToAll(request.ReplyTo))
  System.Diagnostics.Process.Start(url) |> ignore
  Console.ReadLine() |>ignore
  0