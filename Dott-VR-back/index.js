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

io.on('connection', function(socket){
    
    var currenPlayer ={};
    currenPlayer.name = 'unknow';
    console.log("new connection Socket: " + socket.address);
    
    JSON.stringify()
    
    socket.emit('message', {hello: 'world'});
    socket.on('getGames', function(){
        console.log("Games list required by the game!");
        var games = [];

        sqlCon.query("SELECT * FROM game", function (err, result, fields) {
                if (err) throw err;
                
                result.forEach( row =>{
                    console.log(row);
                    games.push(row);
                });

                var gamesJson= JSON.stringify(games);
                console.log("Stringify:")
                console.log(games);
                socket.emit('gamesListReceived', {games:games});
        });
    });
} );

console.log('--- Server is running ---')