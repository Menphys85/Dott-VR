var app = require('express')();
var server = require('http').createServer(app);
// Socket.io
var io = require('socket.io')(server);
//DataBase
var mysql = require('mysql');
var sqlCon = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "",
    database: "dott-db"
});

server.listen(3000);


app.get('/', function(req, res){
    res.send('Hello, you have got a back get "/" ')
});


io.on('connection', function(socket) {

    // var currenPlayer = {};
    // currenPlayer.name = 'unknow';
    console.log("new connection Socket: " + socket.address);
    
    

    socket.emit('message', {hello: 'world'});

    socket.on('getGames', function () {
        console.log("Games list required by the game!");
        var games = [];

        sqlCon.query("SELECT * FROM game", function (err, result, fields) {
            if (err) throw err;

            result.forEach(row => {
                console.log(row);
                games.push(row);
            });

            var gamesJson = JSON.stringify(games);
            console.log("Stringify:")
            console.log(games);
            socket.emit('gamesListReceived', {games: games});
        });
    });

    socket.on('saveEra', function (data) {

        console.log("SaveObjects received from the game: ");
        JSON.stringify(data);
        console.log(data.grapableObjects.length);
        let eraId = data.id;

        removeUnnecessaryObjects(data);
        
        data.grapableObjects.forEach(g => {

            updateGrapableObject(eraId, g);
        });
    });
    
    socket.on('getErasOf', function (data){
        console.log("eras required for " + data.name);
        sqlCon.query("SELECT * FROM era WHERE `game_id` = " + data.id, function (err, result, fields) {
            if(err) throw err;
            
            socket.emit('erasReceived', {eras: result});
            
            
        });
    });
    
    socket.on('getGrapableObjects', function(data){
        console.log("Objects Required revived from the game");
        
        var objects=[];
        
        sqlCon.query("SELECT * FROM grapable_object WHERE `era_id` = " + data.eraId, function (err, result, fields) {
            if (err) throw err;

            result.forEach(row => {
                console.log("variable objects: " + objects);
                
                var position =  JSON.parse(row.position);
                var rotation =  JSON.parse(row.rotation);
                var res = row;
                res.position = position;
                res.rotation = rotation;
                objects.push(res);
            });

            //console.log("tableau : ");

            //console.log(objects);
            var objectResponse = {
                objects: objects
            };
            
            console.log("réponse à envoyer : ");
            console.log(objectResponse);
            
            socket.emit('GrapableObjectList', objectResponse);

        });
        
    });
    
    // socket.on('getObjects', function(data){
    //     sqlCon.query("SELECT * FROM grapable_object WHERE `era_id` = " + data.id, function (err, result, fields) {
    //         if(err) throw err;
    //
    //         socket.emit('objectsReceived', {eras: result});
    //
    //
    //     });
    // });

    socket.on('disconnect', function () {
        console.log('Disconected!!!');
        socket.broadcast.emit('other player disconnected');
        // console.log(currentPlayer.name+' bcst: other player disconnected '+JSON.stringify(currentPlayer));
        // for(var i=0; i<clients.length; i++) {
        //     if(clients[i].name === currentPlayer.name) {
        //         clients.splice(i,1);
        //     }
        // }
    });

    function updateGrapableObject(eraId, g) {

        var alreadyExist = true;

        
        sqlCon.query("SELECT * FROM `grapable_object` WHERE `era_id` = " + eraId +" AND `name` = '"+ g.name + "'", function (err, result, fields) {
            if (err) throw err;
            
            
            if (result.length == 0)
                alreadyExist = false;
            
            
            console.log("AlreadyExiste =" + alreadyExist);

            if (alreadyExist) {
                console.log ("Faire un update:");
                console.log(" Position en x: " + g.position.x);
                
                var query = "UPDATE `grapable_object` SET `position` = '" + JSON.stringify(g.position) + "', `rotation` = '" + JSON.stringify(g.rotation) + "' WHERE era_id ="+ eraId +" AND name ='" + g.name +"'" ;
                console.log(" requète envoyée pour l'update: " + query);
                
                sqlCon.query( query , function (err, result, fields) {
                    if (err) throw err;
                });
            }

            else {

                console.log(eraId);

                var query = "INSERT INTO `grapable_object` (`name`, `era_id`, `position`, `rotation`) VALUES ('" + g.name + "','" + eraId + "','"+ JSON.stringify(g.position) +"' , '" + JSON.stringify(g.rotation) +"')";
                console.log(query);

                sqlCon.query(query, function (err, result, fields) {
                    if (err) throw err;
                });


            }
                
        });

        

    }
    
    function removeUnnecessaryObjects(era){

        var objsInTheGame = [];
        era.grapableObjects.forEach(obj => {
            objsInTheGame.push(obj);
        });
        console.log("Objs en base de game:" + objsInTheGame);
        
        var objsInDB = [];
        sqlCon.query("SELECT * FROM `grapable_object` WHERE `era_id` = " + era.id , function (err, result, fields) {
            if (err) throw err;
            objsInDB=result;
            console.log("Objs en base de donnée:" + objsInDB);

            objsInDB.forEach( obj =>{
                if(objsInTheGame.find( o=> o.name == obj.name) == undefined ){
                    sqlCon.query("DELETE FROM `grapable_object` WHERE id =" + obj.id , function (err, result, fields) {
                        if (err) throw err;
                    });
                }
            });
            
        });
        
    }
});



console.log('--- Server is running ---')