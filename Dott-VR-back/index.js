var app = require('express')();
var server = require('http').createServer(app);
// Socket.io
var io = require('socket.io')(server);
//DataBase
var mysql = require('mysql');
const {TIME} = require("mysql/lib/protocol/constants/types");
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

var disponibleEras = [];

sqlCon.query("SELECT * FROM era", function (err, result, fields) {
    if(err) throw err;
    result.forEach(era => {
        era.isFree = true;
        disponibleEras.push(era);
    });
});



io.on('connection', function(socket) {
    
    if(disponibleEras.length == 0 ){
        setTimeout(() => {
            console.log("Delayed for 3 second.");
        }, "3000");
    }
    var roomName = null;
    var activeEra = null;
    
    console.log("new connection Socket: " + socket.id);
    console.log("");

    socket.on('getGames', function () {
        var games = [];
        sqlCon.query("SELECT * FROM game", function (err, result, fields) {
            if (err) throw err;

            result.forEach(row => {
                games.push(row);
            });
            
            socket.emit('gamesListReceived', {games: games});
        });
    });

    socket.on('saveEra', function (data) {

        console.log("Save received on socket" + socket.id + " for era " + data.id);
        JSON.stringify(data);
        console.log("  └─► " + data.grapableObjects.length + "objects to save");
        console.log("  └─► " + data.npcs.length + "npcs to save");
        console.log("");
        let eraId = data.id;

        //Retire de la base de données tout les objets qui ne sont pas dans la liste d'obets reçus
        removeUnnecessaryObjects(data);
        
        //Mise à jour de tous les objets reçus en base de donnée
        data.grapableObjects.forEach(g => {
            updateGrapableObject(eraId, g);
        });

        //Mise à jour de tous les npcs reçus en base de donnée
        data.npcs.forEach(npc => {
            updateNpc(eraId, npc);
        });
        
    });
    
    socket.on('getErasOf', function (data){
        console.log("Eras required on socket " + socket.id + " for the game '" + data.name + "'");
        sqlCon.query("SELECT * FROM era WHERE `game_id` = " + data.id, function (err, result, fields) {
            if(err) throw err;
            result.forEach(era => {
                if (disponibleEras.find(dEra => dEra.id == era.id) == undefined)
                    era.isFree =false;
                else 
                    era.isFree = true;
            });
            
            socket.emit('erasReceived', {eras: result});
            
            
            roomName= "Game" + data.id;
            
            socket.join(roomName);

            console.log("  └─► Subsribed to chanel '" + roomName + "' and list of " + result.length + " Eras sent.");
            console.log("");
        });
    });
    
    socket.on('getGrapableObjects', function(data){
        console.log("Objects required on socket" + socket.id + " for era " + data.id);
        
        var objects=[];
        
        sqlCon.query("SELECT * FROM grapable_object WHERE `era_id` = " + data.eraId, function (err, result, fields) {
            if (err) throw err;

            result.forEach(row => {
                var position =  JSON.parse(row.position);
                var rotation =  JSON.parse(row.rotation);
                var res = row;
                res.position = position;
                res.rotation = rotation;
                objects.push(res);
            });
            
            var objectResponse = {
                objects: objects
            };
            
            socket.emit('GrapableObjectList', objectResponse);

            console.log("  └─► " + objects.length + "objects sent.");
            console.log("");
        });
        
    });

    socket.on('getNpcs', function(data){
        console.log("Npcs required on socket" + socket.id + " for era " + data.id);

        var npcs=[];

        sqlCon.query("SELECT * FROM npc WHERE `era_id` = " + data.eraId, function (err, result, fields) {
            if (err) throw err;

            result.forEach(row => {
                var position =  JSON.parse(row.position);
                var rotation =  JSON.parse(row.rotation);
                var res = row;
                res.position = position;
                res.rotation = rotation;
                npcs.push(res);
            });

            var npcResponse = {
                npcs: npcs
            };

            socket.emit('NpcsList', npcResponse);

            console.log("  └─► " + npcs.length + "Npcs sent.");
            console.log("");
        });

    });
 
    socket.on('connectToEra', function (data){
        
        console.log("Player connection required to era " + data.id + "with socket " + socket.id);
        console.log("  └─► " + disponibleEras.length + " disponibles Eras" );
        
        activeEra = disponibleEras.find(e => e.id == data.id);
        
        console.log("  └─► Era n°" + activeEra.id + " active: " + activeEra.name );

        //  retrait de l'Era de la liste des Eras disponibles 
            var index = disponibleEras.findIndex(e => e.id == data.id);
            disponibleEras.splice(index, 1);

        console.log("  └─► " + disponibleEras.length + " disponibles Eras left" );
        console.log("");

        activeEra.isFree =false;
        io.to(roomName).emit("PlayerJoin", activeEra);
    });
    
    socket.on('objectToSend', function (data){
        console.log("Object to send received for era " + data.era_id);

        sqlCon.query("DELETE FROM grapable_object WHERE `era_id` = " + activeEra.id +" " +
            "AND `name` = '" + data.name + "'", function (err, result, fields) {
            if (err) throw err;
        });
        updateGrapableObject(data.era_id, data);
        io.to(roomName).emit("GrapableObjectReceived", data);
    });
    
    socket.on('UnsetIsNewForGame', function(data){
        sqlCon.query("UPDATE `game` SET `isNew`= false WHERE id = "+ data.id , function (err, result, fields) {
            if (err) throw err;
        });
    });

    socket.on('DeleteGame', function(data){
        console.log("Delete received for Game " + data.id);
        sqlCon.query("DELETE FROM `game` WHERE id = "+ data.id , function (err, result, fields) {
            if (err) throw err;
        });
    });
    
    socket.on('disconnect', function () {
        console.log( "socket "+ socket.id + " Disconected!!!");
        console.log("");
        if(activeEra !=null){
            disponibleEras.push(activeEra);
            //activeEra =null;            
        }

        socket.broadcast.emit('Other player disconnected');
    });
    
    socket.on('ExitGame', function(){
        console.log("Player leave a game");
        console.log("");
        var roomName= "Game" + activeEra.game_id;
        activeEra.isFree = true;
        disponibleEras.push(activeEra);
        io.to(roomName).emit("PlayerLeave", activeEra);
        socket.leave(roomName);
        activeEra=null;
        
    });

    socket.on('createGameRequired', function() {
        console.log("Game creation required");
        console.log("");

        var query1 = "INSERT INTO `game` (`name`,`isNew`) VALUES ('game', true )";

        sqlCon.query(query1, function (err1, result1, fields) {
            if (err1) throw err1;

            sqlCon.query("SELECT * FROM `game` WHERE `name` = 'game'", function (err2, result2, fields) {
                if (err2) throw err2;
                
                console.log("Partie trouvée : '" + JSON.stringify(result2) +"'")
                var newGame = result2[0];
                newGame.name = "Partie " + newGame.id;

                sqlCon.query("UPDATE `game` SET `name`='"+ newGame.name +"' WHERE `id` =" + newGame.id , 
                    function (err3, result3, fields ) {
                    
                    if (err3) throw err3;
                    CreateErasForGame(newGame.id);
                });
                
                socket.emit("gameCreated", newGame);
            });

        });

    });
    

    function CreateErasForGame(gameId) {
        sqlCon.query("INSERT INTO `era`(`name`, `game_id`) VALUES ('Present'," + gameId + ")" , function (err, result, fields ) {
            if (err) throw err;
        });

        sqlCon.query("SELECT * FROM `era` WHERE `name` = 'Present' AND `game_id` = " + gameId , function (err, result, fields ) {
            if (err) throw err;
            var newEra= result[0];
            disponibleEras.push(newEra);
            
        });
        
        sqlCon.query("INSERT INTO `era`(`name`, `game_id`) VALUES ('Past'," + gameId + ")" , function (err, result, fields ) {
            if (err) throw err;
        });

        sqlCon.query("SELECT * FROM `era` WHERE `name` = 'Past' AND `game_id` = " + gameId , function (err, result, fields ) {
            if (err) throw err;
            var newEra= result[0];
            disponibleEras.push(newEra);

        });
        
        
        sqlCon.query("INSERT INTO `era`(`name`, `game_id`) VALUES ('Futur'," + gameId + ")" , function (err, result, fields ) {
            if (err) throw err;
        });
        sqlCon.query("SELECT * FROM `era` WHERE `name` = 'Futur' AND `game_id` = " + gameId , function (err, result, fields ) {
            if (err) throw err;
            var newEra= result[0];
            disponibleEras.push(newEra);

        });
    }
    
    function updateGrapableObject(eraId, g) {

        var alreadyExist = true;
        
        sqlCon.query("SELECT * FROM `grapable_object` WHERE `era_id` = " + eraId +" AND `name` = '"+ g.name + "'", function (err, result, fields) {
            if (err) throw err;
            
            
            if (result.length == 0)
                alreadyExist = false;

            if (alreadyExist) {
                
                
                var query = "UPDATE `grapable_object` SET `position` = '" + JSON.stringify(g.position) + "', `rotation` = '" + JSON.stringify(g.rotation) + "' WHERE era_id ="+ eraId +" AND name ='" + g.name +"'" ;
                
                
                sqlCon.query( query , function (err, result, fields) {
                    if (err) throw err;
                });
            }

            else {
                var query = "INSERT INTO `grapable_object` (`name`, `era_id`, `position`, `rotation`) VALUES ('" + g.name + "','" + eraId + "','"+ JSON.stringify(g.position) +"' , '" + JSON.stringify(g.rotation) +"')";

                sqlCon.query(query, function (err, result, fields) {
                    if (err) throw err;
                });
            }
        });
    }

    function updateNpc(eraId, npc) {

        var alreadyExist = true;

        sqlCon.query("SELECT * FROM `npc` WHERE `era_id` = " + eraId +" AND `name` = '"+ npc.name + "'", function (err, result, fields) {
            if (err) throw err;


            if (result.length == 0)
                alreadyExist = false;

            if (alreadyExist) {
                var query = "UPDATE `npc` SET `position` = '" + JSON.stringify(npc.position) + "', `rotation` = '" + JSON.stringify(npc.rotation) + "' WHERE era_id ="+ eraId +" AND name ='" + npc.name +"'" ;

                sqlCon.query( query , function (err, result, fields) {
                    if (err) throw err;
                });
            }

            else {
                var query = "INSERT INTO `npc` (`name`, `era_id`, `position`, `rotation`) VALUES ('" + npc.name + "','" + eraId + "','"+ JSON.stringify(npc.position) +"' , '" + JSON.stringify(npc.rotation) +"')";

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
        console.log("Objs in the game: " + objsInTheGame.length);
        
        var objsInDB = [];
        sqlCon.query("SELECT * FROM `grapable_object` WHERE `era_id` = " + era.id , function (err, result, fields) {
            if (err) throw err;
            objsInDB=result;
            console.log("Objs en base de donnée: " + objsInDB.length);

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



console.log('=== SERVER IS RUNNING ===');
console.log(""); 