var target = Argument("target", "Default");


Task("Default")
    .Does(() =>{
            Information("Hallo from cake!!!");
    });

    

    RunTarget(target);